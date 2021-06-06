using UnityEngine;
using System;

public class EnemyShooter : MonoBehaviour
{
    public GameObject[] bulletsPrefab;    
    public bool[] isStoppingForShooting;
    public bool[] isTargetShooting;
    public float[] bulletsFireRate;    
    private float[] bulletsFireRateTemp;    
    public float[] detectionsRange;
    [HideInInspector]
    public bool playerDetected = false;
    [HideInInspector]
    public bool isShooting = false;
    private LayerMask playerLayerMask;
    [HideInInspector]
    public Animator animator;     
    private GameObject warningAtk;
    private EnemyPatrol enemyPatrol;

    private void Start() {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        playerLayerMask = LayerMask.GetMask("player");
        bulletsFireRateTemp = new float[bulletsFireRate.Length];
        Array.Copy(bulletsFireRate,bulletsFireRateTemp,bulletsFireRate.Length);  
        warningAtk = transform.Find("warningAtk").gameObject;      
    }

    private void Update() {        
        int i = 0;        
        foreach(float detectionRange in detectionsRange){  
            Collider2D detect = Physics2D.OverlapCircle(transform.position, detectionRange,playerLayerMask);
            if(detect && enemyPatrol.isPlayerDetectable){   
                playerRaycastDetected();
                dontMoveWhenDetect(i);
                if(bulletsFireRateTemp[i]<=0){   
                    if(!isShooting){
                        bulletsFireRateTemp[i] = bulletsFireRate[i];
                        isShooting = true;
                        animator.Play("atk"+i); 
                        warningAtk.GetComponent<Animator>().SetTrigger("warningAtk"); 
                    } 
                }else{
                    bulletsFireRateTemp[i]-=Time.deltaTime;
                }
                return;                    
            }else{
                GetComponent<EnemyPatrol>().enableMovements();
            }
            i++;
        }        
    }

    private void dontMoveWhenDetect(int i){
        if(isStoppingForShooting[i] == true){
            GetComponent<EnemyPatrol>().disableMovements();
        }else{
            GetComponent<EnemyPatrol>().enableMovements();
        }
    }

    public void playerRaycastDetected(){
        playerDetected = true;
        PlayerHealth.instance.isNotStealth();
    }

    public void launchBullet(int bulletId){
        Transform bulletsPos = null;
        try
        {
            bulletsPos = transform.Find("bullet"+bulletId+"Pos").gameObject.transform;
        }
        catch (System.Exception)
        {
            if( transform.Find("bullet"+bulletId+"Pos")== null){            
                Debug.LogWarning("ATTENTION l'enemy ne peut pas tirer car le gameobject 'bullet"+bulletId+"Pos' n'existe pas !!!!!!!");
            }
            return;
        }  

        GameObject bullet = (GameObject)Instantiate(bulletsPrefab[bulletId],bulletsPos.position,Quaternion.Euler(0,180,0));
        float speedBullet = bullet.GetComponent<Bullet>().speed;

        if(enemyPatrol.isFacingLeft){                
            bullet.transform.eulerAngles = new Vector3(0,0,0);            
            bullet.GetComponent<Bullet>().speed = -speedBullet;
        }
        bullet.GetComponent<Bullet>().isPlayerBullet = false;


        if(isTargetShooting[bulletId]){
            Vector3 direction = enemyPatrol.player.transform.position - gameObject.transform.position;          
            bullet.GetComponent<Bullet>().setShootingDirection(direction.x*Mathf.Abs(speedBullet/4),direction.y*Mathf.Abs(speedBullet/4));
        }

        isShooting = false;
        animator.Play("idle");
    }

    private void OnDrawGizmos() {        
        Gizmos.color = Color.red;
        foreach(float detectionRange in detectionsRange){
            Gizmos.DrawWireSphere(transform.position, detectionRange);            
        }        
    }
}
