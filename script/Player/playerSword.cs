using UnityEngine;

public class playerSword : MonoBehaviour
{
    public CircleCollider2D swordBox;

    public Transform swordPosition;
    public float swordRadius;
    public GameObject hitAnimation;

    public AudioSource[] sounds;
    
    public GameObject thirdHitAnimation;

    public static playerSword instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une isntance de playersword dans la scene");
            return;
        }

        instance = this;
    }

    private void Update() {       
        if(PlayerActions.instance.isAttacking){
            swordBox.enabled = true;
        }else{
            swordBox.enabled  = false;
        } 
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(swordPosition.position,swordRadius);        
    }

    public void playSaberSong(int i){
        sounds[i].Play();
    }

    public void thirdStrikeAnimation(){
        PlayerActions.instance.isThirdHit = true; 
        GameObject thirdHitAnim = null;
        if(PlayerMove.instance.facingRight){
            thirdHitAnim = Instantiate(thirdHitAnimation,transform.position,Quaternion.identity);
        }else{
            thirdHitAnim = Instantiate(thirdHitAnimation,transform.position,Quaternion.Euler(0,180,0));
        }        
        thirdHitAnim.transform.parent = transform;
    }
}
