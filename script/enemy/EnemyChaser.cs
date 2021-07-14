using System;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float detectionRange; 
    public bool isTrackingPlayer = true;

    [Header("Keep distance")]
    public bool keepDistanceToPlayer = false;
    private Vector3 distanceToPlayer;
    public Vector3 distanceToPlayerMin;
    public Vector3 distanceToPlayerMax;

    [Header("time to reach")]
    public float deltaTimeToReach = 1;
    private float tempDeltaTimeToReach;
    private GameObject player;

    [HideInInspector]
    public bool playerDetected = false;  
    private Vector3 velocity;    
    private LayerMask playerLayerMask; 
    private EnemyPatrol enemyPatrol;    
    private GameObject warningAtk;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLayerMask = LayerMask.GetMask("player");
        enemyPatrol = GetComponent<EnemyPatrol>(); 
        warningAtk = transform.Find("warningAtk").gameObject; 
        getDistanceToPlayer();
        resetTimeToReach();
    }

    private void getDistanceToPlayer(){
        if(keepDistanceToPlayer)
            distanceToPlayer = new Vector3(UnityEngine.Random.Range(distanceToPlayerMin.x, distanceToPlayerMax.x + 1),UnityEngine.Random.Range(distanceToPlayerMin.y, distanceToPlayerMax.y + 1),UnityEngine.Random.Range(distanceToPlayerMin.z, distanceToPlayerMax.z + 1));
    }

    void Update()
    {    
        Collider2D detect = Physics2D.OverlapCircle(transform.position, detectionRange,playerLayerMask);
        if(detect || playerDetected){
            playerRaycastDetected();  
            tempDeltaTimeToReach -= 0.001f; 

            if(isTrackingPlayer){
                enemyPatrol.allwaysWatchingPlayer = true;

                if(keepDistanceToPlayer){                    
                    Vector3 tempdistanceToPlayer;                 
                    if(enemyPatrol.isPlayerOnRight){
                        tempdistanceToPlayer = new Vector3(-distanceToPlayer.x, distanceToPlayer.y,distanceToPlayer.z);
                    }else{                        
                        tempdistanceToPlayer = new Vector3(distanceToPlayer.x, distanceToPlayer.y,distanceToPlayer.z);
                    }
                    //transform.position = Vector3.MoveTowards(transform.position, player.transform.position + tempdistanceToPlayer, tempDeltaTimeToReach * Time.deltaTime);

                    Vector3 posToReach = transform.position;
                    if(tempDeltaTimeToReach > 0){
                        posToReach = player.transform.position + tempdistanceToPlayer;
                    }
                    transform.position = Vector3.SmoothDamp(transform.position,posToReach , ref velocity,tempDeltaTimeToReach*Time.deltaTime*200);

                    if(tempDeltaTimeToReach < -0.2f){
                        enemyPatrol.goToTheOtherSideX();
                        getDistanceToPlayer();
                        resetTimeToReach();
                    }
                }else{
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity,tempDeltaTimeToReach);
                }

                if (Vector3.Distance(transform.position, player.transform.position) < 0.001f){        
                    resetTimeToReach();
                    playerDetected = false;
                }
            }       
        }else{
            resetTimeToReach();
            playerDetected = false;
        }        
    }

    public void playerRaycastDetected(){
        if(!playerDetected){
            warningAtk.GetComponent<Animator>().SetTrigger("warningAtk"); 
        }
        playerDetected = true;
        PlayerHealth.instance.isNotStealth();
    }

    private void resetTimeToReach(){
        tempDeltaTimeToReach = deltaTimeToReach;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectionRange);
    }
}
