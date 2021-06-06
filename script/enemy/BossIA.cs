using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIA : Enemy
{
    private GameObject player;
    public float speedMovement = 5;
    public float eyesDetectionPlayerRange;
    public int runRate = 100;
    public int tpRate = 30;
    public int idleRate = 15;
    private int moveActionChoice = 99;
    public int iaRateAtk = 50;
    private bool iaWantToAtk;
    private bool isRunning;
    private bool isIdling;
    private bool isTeleporting;
    private bool isAttackingShort;
    private bool isIdleAttackingDistance;
    private bool isRunningAttackingDistance;
    public float timerChangeAction = 4;
    private float timerElapsed = 3;
    private GameObject eyes;
    public GameObject bulletsPref;
    private GameObject gunPos;
    private bool playerPosRight;
    public LayerMask playerLayerMask;

    new void  Start()
    {        
        base.Start();
        eyes = transform.Find("eyes").gameObject;
        gunPos = transform.Find("bulletPos").gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {        
        if(detectPlayer()){
            checkPlayerDirection();
            flipSprite();
            isCloseToPlayer();

            if(isRunning){
                animator.Play("run");
            }else if(isIdling){
                playIdleAnim();
            }else if(isTeleporting){                
                animator.Play("spawn");
            }else if(isAttackingShort){                
                animator.Play("saber");
            }else if(isIdleAttackingDistance){                
                animator.Play("shooting");
            }else if(isRunningAttackingDistance){                
                animator.Play("shootRun");
            }
        }        
    }

    private void FixedUpdate() {
        if(detectPlayer()){                    
            if(isCloseToPlayer()){
                shortAtk();
            }else{
                timerElapsed -= Time.deltaTime;
                if(timerElapsed <=0){
                    timerElapsed = Random.Range(1.5f,timerChangeAction);
                    moveActionChoice = Random.Range(0,100);
                    iaWantToAtk = checkWantToAtk();
                }
               
                if(moveActionChoice <= idleRate){
                    if(iaWantToAtk){
                        distanceIdleAtk();
                    }else{                        
                        idleBoss();
                    }
                }else if(moveActionChoice < tpRate){
                    teleportRandom();
                }else{
                    if(iaWantToAtk){
                        distanceRunAtk();
                    }else{
                        runningToPlayer();
                    }
                }                
            }
        }else{
            idleBoss();
        }
    }

    private bool detectPlayer()
    {
        bool detected = Physics2D.OverlapCircle(eyes.transform.position, eyesDetectionPlayerRange,playerLayerMask);
        if(detected) return true;

        return false;
    }

    private bool checkWantToAtk(){        
        if(Random.Range(0,100)<=iaRateAtk) return true;
        
        return false;
    }

    void playIdleAnim(){
        if(currentHealth <= maxHealth/2){
            animator.Play("idleWeak");
        }else{
            animator.Play("idle");
        }
    }    

    private void stopAllStates(){
        isIdling = false;  
        isRunning = false;  
        isTeleporting = false;
        isAttackingShort = false;
        isRunningAttackingDistance = false;
        isIdleAttackingDistance = false;
    }

    void idleBoss(){
        stopAllStates();
        isIdling = true;    
        rb.velocity = new Vector2(0,0);        
    }

    void runningToPlayer(){
        stopAllStates();
        isRunning = true;
        if(playerPosRight){
            rb.velocity = new Vector2(speedMovement,0);
        }else{
            rb.velocity = new Vector2(-speedMovement,0);
        }        
    }

    void teleportRandom(){
        if(!isTeleporting){
            stopAllStates();
            isTeleporting = true;
            rb.velocity = new Vector2(0,0);                        
        }
    }
    public void doTeleportation(){
        float xPosBack = Random.Range(player.transform.position.x - 5, player.transform.position.x - 2);
        float xPosFront = Random.Range(player.transform.position.x + 2, player.transform.position.x + 5);

        int isFrontTp = Random.Range(0,2);
        float xPos = 0;
        if(isFrontTp == 1){
            xPos = xPosFront;
        }else{
            xPos = xPosBack;
        }

        transform.position = new Vector3(xPos, player.transform.position.y, transform.position.z);
    }
    public void endTeleporting(){
        isTeleporting = false;
        timerElapsed = -0.2f;
    }

    void shortAtk(){         
        stopAllStates();
        rb.velocity = new Vector2(0,0);    
        isAttackingShort = true;
    }

    public void stopShortAtk(){ 
        playIdleAnim();
    }

    void distanceIdleAtk(){         
        stopAllStates();
        rb.velocity = new Vector2(0,0);    
        isIdleAttackingDistance = true;
    }

    void distanceRunAtk(){         
        stopAllStates();
        if(playerPosRight){
            rb.velocity = new Vector2(speedMovement,0);
        }else{
            rb.velocity = new Vector2(-speedMovement,0);
        }    
        isRunningAttackingDistance = true;        
    }

    public void shootBullet(){
        Bullet bullet =  Instantiate(bulletsPref,gunPos.transform.position,Quaternion.identity).GetComponent<Bullet>();
        if(!playerPosRight){
            bullet.speed = -bullet.speed;
        }
    }

    void flipSprite(){
        if(playerPosRight){
            transform.rotation = Quaternion.Euler(0,-180,0);
        }else{
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    void checkPlayerDirection(){
        if(player.transform.position.x >= transform.position.x){
            playerPosRight = true;
        }else{
            playerPosRight = false;
        }
    }

    bool isCloseToPlayer(){
        if(transform.position.x <= player.transform.position.x + 2 &&
           transform.position.x >= player.transform.position.x - 2 &&
           transform.position.y <= player.transform.position.y + 1.5 &&
           transform.position.y >= player.transform.position.y - 1.5){
               return true;
        }
        return false;
    }

    // IEnumerator delayToChangeAction(){
    //     yield return new WaitForSeconds(2);
    //     canChangeState = true;
    // }

    void OnDrawGizmos() {
        eyes = transform.Find("eyes").gameObject;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(eyes.transform.position,eyesDetectionPlayerRange);
    }
}
