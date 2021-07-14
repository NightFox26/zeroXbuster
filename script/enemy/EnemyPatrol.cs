using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    const float TIME_TO_WAIT_UPDATE = 0.1f;
    const float TIME_TO_WAIT_FOR_RANDOM_Y_VELOCITY = 2f;
    public bool isFlying = false;
    public bool isFreeToMove;
    public Vector2 limitsMove;
    private Vector3 initEnemyPos;

    public float speed;

    [HideInInspector]
    public float velocityX;
    [HideInInspector]
    public float velocityY;

    public bool moveWhenDetect = false;

    public bool allwaysWatchingPlayer = false;

    [HideInInspector]
    public bool isPlayerDetectable = false;


    [HideInInspector]
    public bool isFacingLeft = true;
    [HideInInspector]
    public bool isPlayerOnRight = false;

    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public Rigidbody2D rb;
    private Vector3 lastPosition;

    public Transform groundCheckPos;
    private bool isGrounded = false;
    private float groundCheckRadius = 0.5f;
    private float timeDelayToCheckIfBlocked = 0;
    private float timeDelayToRandomVelocityY = 0;


    public Transform eyes;

    [Header( "ground detection")]
    public Vector3 checkHoleDirection;
    public float lengthVision;

    private Enemy enemy;
    private EnemyShooter enemyShooter;
    private EnemyChaser enemyChaser;

    [HideInInspector]
    public GameObject player;

    private void Start() {
        velocityX = speed;
        velocityY = speed;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        initEnemyPos = transform.position;
        lastPosition = transform.position;

        if(GetComponent<EnemyShooter>() != null)
            enemyShooter = GetComponent<EnemyShooter>();

        if(GetComponent<EnemyChaser>() != null)
            enemyChaser = GetComponent<EnemyChaser>();
    }
    private void Update() {
        checkIfGrounded();
        if(velocityX != 0){
            if(velocityX>0){
                isFacingLeft = false;
                transform.eulerAngles = new Vector3(0,180,0);
            }else if (velocityX<0){
                isFacingLeft = true;
                transform.eulerAngles = new Vector3(0,0,0);
            }      
        }

        if(allwaysWatchingPlayer){
            if(player.transform.position.x > enemy.transform.position.x){
                isFacingLeft = false;
                isPlayerOnRight = true;
                transform.eulerAngles = new Vector3(0, 180, 0);                
            }else{
                isFacingLeft = true;
                isPlayerOnRight = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        checkIfPlayerDetectable();
    }
    
    private void FixedUpdate() {            
        if(enemy.currentHealth <= 0){
           rb.velocity =  new Vector2(0, 0);
           enemy.animator.enabled = false;
           return; 
        }
        
        if(enemyShooter != null){
            if(enemyShooter.isShooting || (moveWhenDetect && !enemyShooter.playerDetected))
                return;         
        }

        if(enemyChaser != null){
            if(enemyChaser.playerDetected) {                
                return;            
            }             
        }

        if(enemyChaser != null){
            if(moveWhenDetect && !enemyChaser.playerDetected)              
                return;            
        }

        if(rb.bodyType == RigidbodyType2D.Static)
            return;        
        
        if(isFlying){
            rb.gravityScale = 0;
            timeDelayToRandomVelocityY += Time.deltaTime;
            if(timeDelayToRandomVelocityY >= TIME_TO_WAIT_FOR_RANDOM_Y_VELOCITY){                
                ranomYvelocity();
            }else{
                rb.velocity = new Vector2(velocityX, velocityY);
            }
        }

        if(velocityX !=0){ 
            rb.velocity =  new Vector2(velocityX, rb.velocity.y);
            checkIfGoingToFall();
            playMovingAnim();
            if(timeDelayToCheckIfBlocked > TIME_TO_WAIT_UPDATE){
                if(!isFreeToMove){
                    checkIfOverMoveLimits();
                }else{
                    if(enemyChaser != null){
                        print("here");
                        checkIfBlocked();
                    }
                }
            }            
            lastPosition = transform.position; 
            timeDelayToCheckIfBlocked += Time.deltaTime;                     
        }        
    } 

    private void playMovingAnim(){
        if(isFlying){
            enemy.animator.Play("moving");
        }else if(!isFlying && isGrounded){
            enemy.animator.Play("moving");
        }
    }

    private void checkIfGrounded(){
        isGrounded = false;
        if(!isFlying){
            isGrounded = Physics2D.OverlapCircle(groundCheckPos.position,groundCheckRadius,LayerMask.GetMask("ground"));
        }
    }

    private void checkIfPlayerDetectable(){
        float playerXpos = player.transform.position.x;
        if((isFacingLeft && playerXpos < transform.position.x) ||
           (!isFacingLeft && playerXpos > transform.position.x)){            
            isPlayerDetectable = true;
        }else{
            isPlayerDetectable = false;
        }
    }

    private void checkIfGoingToFall(){
        if(!isFlying && isGrounded ){ 
            checkHoleDirection.x = Mathf.Abs(checkHoleDirection.x);
            if(isFacingLeft){
                checkHoleDirection.x = -checkHoleDirection.x;
            }     

            RaycastHit2D ray = Physics2D.Raycast(eyes.position,checkHoleDirection,lengthVision,LayerMask.GetMask("ground"));
            //Debug.DrawRay(eyes.position,checkHoleDirection*lengthVision,Color.red,0.1f);            
            if (!ray.collider){
                goToTheOtherSideX();          
            }
        }
    }

    public void disableMovements(){
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void enableMovements(){
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void checkIfOverMoveLimits(){
        if(transform.position.x >= initEnemyPos.x + limitsMove.x/2) goToLeft();
        if(transform.position.x <= initEnemyPos.x - limitsMove.x/2) goToRight();
        if(transform.position.y >= initEnemyPos.y + limitsMove.y/2) goDown();
        if(transform.position.y <= initEnemyPos.y - limitsMove.y/2) goUp();
    }

    private void checkIfBlocked(){ 
        if((transform.position.x - lastPosition.x == 0) ){
            goToTheOtherSideX();                
        }

        if((transform.position.y - lastPosition.y == 0) ){ 
            goToTheOtherSideY();                
        }
    }

    private void ranomYvelocity(){
        velocityY = Random.Range(-speed,speed);
        rb.velocity = new Vector2(velocityX, velocityY);
        timeDelayToRandomVelocityY = 0;
    }

    private void goToLeft(){
        velocityX = -speed;  
        timeDelayToCheckIfBlocked = 0;   
    }

    private void goToRight(){
        velocityX = speed;  
        timeDelayToCheckIfBlocked = 0;   
    }
    private void goDown(){
        velocityY = -speed;  
        timeDelayToCheckIfBlocked = 0;   
    }
    private void goUp(){
        velocityY = speed;  
        timeDelayToCheckIfBlocked = 0;   
    }

    public void goToTheOtherSideX(){
        velocityX = -velocityX;  
        timeDelayToCheckIfBlocked = 0;      
    }

    public void goToTheOtherSideY(){
        velocityY = -velocityY;  
        timeDelayToCheckIfBlocked = 0;      
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        if(groundCheckPos != null)
            Gizmos.DrawWireSphere(groundCheckPos.position,groundCheckRadius);

        if(!isFreeToMove)
            Gizmos.DrawWireCube(initEnemyPos,new Vector3(limitsMove.x,limitsMove.y,1));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(eyes.position,checkHoleDirection*lengthVision);
    }

}
