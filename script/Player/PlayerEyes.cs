using UnityEngine;

public class PlayerEyes : MonoBehaviour
{
    public Vector3 eyesWatchingSize;
    public bool enemyAtkDetected = false;
    public static PlayerEyes instance;
    private void Awake() {
        if(instance != null){
            return;
        }
        instance = this;
    }
    void Update()
    {
        enemyAtkDetected = checkEnemyAtkDetected();
    }

    private bool checkEnemyAtkDetected()
    {
        Collider2D[] hitColliders = null;
        if(PlayerMove.instance.facingRight){
            hitColliders =  Physics2D.OverlapBoxAll(transform.position+ new Vector3(eyesWatchingSize.x/2,0,0),eyesWatchingSize,0.0f,LayerMask.GetMask("enemies")); 
            
        }else{
            hitColliders = Physics2D.OverlapBoxAll(transform.position+ new Vector3(-eyesWatchingSize.x/2,0,0),eyesWatchingSize,180f,LayerMask.GetMask("enemies")); 
        }  
        foreach(Collider2D collider in hitColliders){
            if(collider.gameObject.GetComponent<EnemyShooter>() != null || 
            collider.gameObject.GetComponent<EnemyChaser>() != null ||
            collider.gameObject.GetComponent<EnemyChallenge>() != null
            ){
                GameObject warninkAtk = collider.gameObject.transform.Find("warningAtk").gameObject;
                if(warninkAtk.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("warningAtk")){                        
                    return true;
                }
            }
                  
        }
        return false;    
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position+ new Vector3(eyesWatchingSize.x/2,0,0), eyesWatchingSize);        
    }
}
