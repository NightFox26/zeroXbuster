using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float detectionRange; 
    public bool isTrackingPlayer = true;
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
        resetTimeToReach();
    }

    void Update()
    {    
        Collider2D detect = Physics2D.OverlapCircle(transform.position, detectionRange,playerLayerMask);
        if(detect || playerDetected){
            playerRaycastDetected();  
            tempDeltaTimeToReach -= 0.001f; 

            if(isTrackingPlayer){
                enemyPatrol.allwaysWatchingPlayer = true;
                transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity,tempDeltaTimeToReach);

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
