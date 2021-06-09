using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{      
    public Transform groundCheck;
    public Transform wallCheck;    
    public GameObject spawnElectricAnimation;
    private PlayerActions actions;
    private PlayerHealth health;    
    
    [HideInInspector]
    public bool isTeleporting = false;
    [HideInInspector]
    public bool facingRight = true;
    public float grounCheckRadius;
    public float wallCheckRadius;   
    public LayerMask collisionLayers;
    public LayerMask collisionRollingLayers;
    public static PlayerMove instance;
    public Rigidbody2D rb2d;
    public Animator animator;    
    private float rollingTimer = 1f;
    private float gravityScaleInit;    
    public Animator dashDustAnim;
    
    private bool isRunning;
    [HideInInspector]
    public bool isJumping;
    [HideInInspector]
    public bool isJumpAllowed;
    private bool isWallJumping;
    [HideInInspector]
    public bool isFalling;
    public bool isTouchingWall;
    public bool isGrounded;

    [HideInInspector]
    public bool isDashing;
    private bool isCriticalDashing;
    private bool isRollingAllowed;

    [HideInInspector]
    public bool isRolling;

    [HideInInspector]
    public float horizontalMovement;
    [HideInInspector]
    public float verticalMovement;
    

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("Il y a plusieurs instance de playerMove dans la scene");
            return;
        }
        instance = this;
        gravityScaleInit = GetComponent<Rigidbody2D>().gravityScale;
        rb2d          = GetComponent<Rigidbody2D>();
        animator      = GetComponent<Animator>();  
        rb2d.bodyType = RigidbodyType2D.Dynamic;   
        isTeleporting = false;  
        isRollingAllowed = true;
        actions = GetComponent<PlayerActions>();      
        health = GetComponent<PlayerHealth>();
    }
    

    private void Update() {       

        checkIfTouchingWall();
        checkIfGrounded();
        if(!isGrounded && !health.isHurted && !health.isDying){
            if(!isTouchingWall){
                if(actions.isQuickTp){
                    animator.Play("playerQuickTp");  
                }else if(actions.isJumpSwordRolling){
                    animator.Play("playerJumpSwordRolling");                           
                }else if(isRolling){
                    animator.Play("playerRoulade");
                }else if(actions.isShooting){
                    animator.Play("playerShooting");
                }else if(actions.isDragonPunchSword){
                    animator.Play("playerDragonPunch");            
                }else if(isCriticalDashing){
                    animator.Play("playerCriticalDash");
                }else if(isDashing){
                    animator.Play("playerDash");
                }else if(actions.isAttacking){
                    animator.Play("playerJumpSword");
                }else if(isJumping){
                    animator.Play("playerJump"); 
                }else if(isFalling){
                    animator.Play("playerFalling");
                }
            }else{
                animator.Play("playerWallClimb");
            }
        }

        if(isGrounded && !health.isHurted && !health.isDying && !isTeleporting){
            if(actions.isEarthQuake){                
                animator.Play("playerEarthQuake");
            }else if(isRolling){
                animator.Play("playerRoulade");            
            }else if(isCriticalDashing){
                animator.Play("playerCriticalDash");  
            }else if(isDashing){
                animator.Play("playerDash");  
            }else if(actions.isDragonPunchSword){
                animator.Play("playerDragonPunch");
            }else if(actions.isAttacking && actions.nbOfAttackingClick == 1){
                animator.Play("standSword");
            }else if(actions.isAttacking && actions.nbOfAttackingClick == 2){
                animator.Play("standSword2");
            }else if(actions.isAttacking && actions.nbOfAttackingClick == 3){
                animator.Play("standSword3");
            }else if(actions.isShooting){
                animator.Play("playerShooting");
            }else if(actions.chargingShot){
                animator.Play("playerChargedShot");
            }else if(isRunning){
                animator.Play("playerRun");
            }else{
               animator.Play("playerIdle"); 
            }
        }

        if(health.isDying){
            animator.Play("playerDying"); 
        }else if(health.isHurted){
            animator.Play("playerHurt"); 
        }
        
        //sert a retourner le perso (le flipX ne marche pas car il ne retourne pas les objets a l'interieur de zero)
        if(facingRight == true){
            transform.eulerAngles = new Vector3(0, 0, 0);
        }else{
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
        //gestion du saut
        if(Input.GetButtonDown("Jump") && (isGrounded || isTouchingWall || isJumpAllowed)){ 
           if(isTouchingWall && !isGrounded){
               isWallJumping = true;              
           }              
           rb2d.velocity = new Vector2(transform.position.x, getJumpForce());
        } 

        //gestion de la roulade
        if(Input.GetButtonDown("Fire2") && isRollingAllowed){ 
            AudioManager.Instance.Play(PlayerSounds.instance.rouladeSound);
            isRollingAllowed = false;       
            actions.isShootingAllowed = false;
            isRolling = true;
            passingThroughtEnemy();
        }

        //gestion du dash
        if(Input.GetButtonDown("L1") && isDashing == false && isCriticalDashing == false && !isTouchingWall){ 
            isDashing = true;
            actions.isShootingAllowed = false;
            dashDustAnim.SetTrigger("isDashing");
            if(PlayerEyes.instance.enemyAtkDetected && PlayerNewMovements.instance.obtain_counterDash){
                actions.isAttacking = true;
                isCriticalDashing = true;
            }
        }
    }

    void FixedUpdate()
    {         
        horizontalMovement = Input.GetAxis("Horizontal"); 
        verticalMovement = Input.GetAxis("Vertical"); 
        
        if(!isTeleporting && !PlayerHealth.instance.isDying){ 

            //QuickTp
            if(actions.isQuickTp){
                rb2d.velocity = new Vector2(0,0);               
                rb2d.AddForce(new Vector2(actions.impulseForceQuickTp * horizontalMovement,actions.impulseForceQuickTp * verticalMovement),ForceMode2D.Impulse);                
                return;
            }

            //verification de l'etat du saut
            if(rb2d.velocity.y < -1 && !isGrounded){                
                isFalling = true;                
            }else if( rb2d.velocity.y > 3){
                isJumping = true;
                isFalling = false;
            }else{
                isJumping = false;
                isFalling = false;
            }            

            //deplacement normal
            if(horizontalMovement>0.3f && isWallJumping == false && !actions.isEarthQuake){
                rb2d.velocity = new Vector2(horizontalMovement*getVelocityForce(),rb2d.velocity.y);
                facingRight = true;  
                isRunning = true;
            }else if(horizontalMovement<-0.3f && isWallJumping == false && !actions.isEarthQuake){
                rb2d.velocity = new Vector2(horizontalMovement*getVelocityForce(),rb2d.velocity.y);
                facingRight = false;   
                isRunning = true;
            }else{
                rb2d.velocity = new Vector2(0,rb2d.velocity.y);
                isRunning = false; 
            }

            //wall jump repulsion
            if(isWallJumping){                
                if(facingRight){
                    rb2d.velocity = new Vector2(getVelocityForce()*-10, getJumpForce()*0.5f); 
                    rb2d.AddForce(new Vector2(-200,500));
                }else{
                    rb2d.velocity = new Vector2(getVelocityForce()*10, getJumpForce()*0.5f); 
                    rb2d.AddForce(new Vector2(200,500));
                }
                isWallJumping = false;
                return;
            }  

            //glissade sur les murs
            if(isTouchingWall){
                if(rb2d.velocity.y < -1 && !isGrounded){
                   rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y + 0.8f);             
                }                
            }          

            //acceleration du dash
            if((isDashing == true || isCriticalDashing == true) && !actions.isEarthQuake){
                rb2d.velocity = new Vector2(horizontalMovement*getVelocityForce()*getDashForce(),rb2d.velocity.y);
                if(isCriticalDashing){
                    passingThroughtEnemy();
                    if(facingRight){
                        rb2d.velocity = new Vector2(rb2d.velocity.x + 4.5f, 0);
                    }else{
                        rb2d.velocity = new Vector2(rb2d.velocity.x - 4.5f, 0);
                    }
                }
            }
        }

    }

    public void stopGravity(){
        rb2d.gravityScale = 0;
    }

    public void reGravity(){
        rb2d.gravityScale = gravityScaleInit;
    }

    private float getJumpForce(){
        return PlayerStats.instance.jumpPower;
    }
    private float getVelocityForce(){
        return PlayerStats.instance.velocity;
    }

    private float getDashForce(){
        return PlayerStats.instance.dashVelocity;
    }

    //impulse du dragon punch
    public void makeImpulseDragonPunch(){
        rb2d.AddForce(new Vector2(0,800));
    }    

    // systeme de teleportation
    public void playTeleportationAnimation(){
        health.isInvincible = true;
        isTeleporting = true;
        animator.Play("playerTeleport");
        rb2d.bodyType = RigidbodyType2D.Static;
    }

    public void teleportToPosition(Vector3 destination){
        Animator animatorFondu =  GameObject.FindGameObjectsWithTag("fondu")[0].GetComponent<Animator>();
        PlayerMove.instance.moveDisable();
        animatorFondu.SetTrigger("startFonduOn");                 
        StartCoroutine(delayTp(destination));
    }

    IEnumerator delayTp(Vector3 destination){
        CameraFollow cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        yield return new WaitForSeconds(0.5f);
        facingRight = true;
        gameObject.transform.position = destination;        
        cam.goFindPlayer();
        PlayerMove.instance.moveEnable();
    }
    /********************************************/

    //Quand zero spawn sur la map (teleport vers le bas)
    public void playerSpawning(){
        facingRight = true;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        animator.Play("playerSpawn");        
        isTeleporting = false;        
    }
    public void invokeSpawnLightnings(){
        Instantiate(spawnElectricAnimation,transform.position,Quaternion.identity);
        health.isInvincible = false;
    }   
    /*****************************************/
    
    public void stopRolling(){          
        StartCoroutine(rollingTimerControl());
    }  
    public void passingThroughtEnemy(){
        GetComponent<SurfaceEffector2D>().colliderMask = collisionRollingLayers;
        PlayerHealth.instance.isInvincible = true;
    }
    public void stopPassingThroughtEnemy(){
        GetComponent<SurfaceEffector2D>().colliderMask = collisionLayers;
        PlayerHealth.instance.isInvincible = false;
    }    
    
    IEnumerator rollingTimerControl(){
        yield return new WaitForSeconds(0.5f);
        isRolling = false;
        actions.isShootingAllowed = true;
        stopPassingThroughtEnemy();
        yield return new WaitForSeconds(rollingTimer - 0.5f);
        isRollingAllowed = true;
    }

    public void stopDashing(){
        isDashing = false;
        actions.isShootingAllowed = true;
        actions.isAttacking = false;
        isCriticalDashing = false;
        stopPassingThroughtEnemy();
    }

    public void disableJump(){
        isJumpAllowed = false;
    }

    private void checkIfGrounded(){          
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,grounCheckRadius,collisionLayers);
        if(isGrounded){
            actions.canQuickTpAgain = true;
            actions.isJumpSwordRolling = false;
            isJumpAllowed = true;
        }            
    }

    private void checkIfTouchingWall(){  
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position,grounCheckRadius,collisionRollingLayers);        
        if(isTouchingWall && !isGrounded){
            actions.canQuickTpAgain = true;
            actions.isJumpSwordRolling = false;
            actions.isAttacking = false;
            actions.isDragonPunchSword = false;
            isJumpAllowed = true;
        }  
    } 

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position,grounCheckRadius);        
        Gizmos.DrawWireSphere(wallCheck.position,wallCheckRadius);        
    }

    public void moveDisable(){
        if(!isTeleporting){
            animator.Play("playerIdle");
        }
        PlayerMove.instance.enabled = false;
        PlayerActions.instance.enabled = false;
    }

    public void moveEnable(){
        PlayerMove.instance.enabled = true;
        PlayerActions.instance.enabled = true;
    }


}
