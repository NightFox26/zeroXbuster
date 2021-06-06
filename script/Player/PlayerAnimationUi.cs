using UnityEngine;

public class PlayerAnimationUi : MonoBehaviour
{
    public GameObject levelUpTxt;
    public GameObject getBoosterTxt;
    public GameObject levelUp_expulsionAnim;
    public static PlayerAnimationUi instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de PlayerAnimationUi");
            return;
        }
        instance = this;
    }

    public void playAnimLevelUp(){  
        levelUpTxt.transform.position = new Vector3 (transform.position.x, transform.position.y + 1);  
        if(PlayerMove.instance.facingRight){
            transform.Find("Canvas").gameObject.transform.localScale = new Vector3(0.08f,0.08f,1);
        }else{            
            transform.Find("Canvas").gameObject.transform.localScale = new Vector3(-0.08f,0.08f,1);
        }
        Instantiate(levelUp_expulsionAnim,transform.position,Quaternion.identity);     
        levelUpTxt.GetComponent<Animator>().SetTrigger("levelUp");
    }

    public void playAnimGetBooster(){  
        getBoosterTxt.transform.position = new Vector3 (transform.position.x, transform.position.y + 1);  
        if(PlayerMove.instance.facingRight){
            transform.Find("Canvas").gameObject.transform.localScale = new Vector3(0.08f,0.08f,1);
        }else{            
            transform.Find("Canvas").gameObject.transform.localScale = new Vector3(-0.08f,0.08f,1);
        }     
        getBoosterTxt.GetComponent<Animator>().SetTrigger("getBooster");
    }
}
