using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    public string sceneName;

    [HideInInspector]
    public GameObject loadingScreen;

    [HideInInspector]
    public Animator animatorFondu;     
    private CameraFollow cam;
    public static TeleportScript instance;

    private void Awake() {
        if(instance != null){
            // Debug.LogWarning("il y a deja une instance de teleportScript");
            return;
        }
        instance = this;

        try{
            animatorFondu =  GameObject.FindGameObjectWithTag("fondu").GetComponent<Animator>();        
            loadingScreen =  GameObject.FindGameObjectWithTag("loadingScreen");
        }
        catch (System.Exception){
            Debug.LogWarning("Impossible de charger l'objet blackscreen...il est surement desactivé... ");
            loadingScreen = null;
            animatorFondu = null;
            Dev_params.instance.enableLoading = false;
        }   
    }

    private void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }
    
    //pour la teleport vers une autre scene
    public void teleportPlayer(string scName, bool needAnimTp = true, bool needLoading = false)
    {                    
        StartCoroutine(waitFondu(scName,needAnimTp,needLoading));        
    } 

    IEnumerator waitFondu(string scName, bool needAnimTp, bool needLoading){
       if(Dev_params.instance.enableLoading){
            if(needAnimTp){            
                PlayerMove.instance.playTeleportationAnimation();
                yield return new WaitForSeconds(0.5f);
            }   
            animatorFondu.SetTrigger("startFonduOn");
            yield return new WaitForSeconds(1f);         

            if(needLoading){            
                loadingScreen.transform.Find("Panel").gameObject.SetActive(true);
                loadingScreen.GetComponent<LoadingScreenManager>().showLoading();
                yield return new WaitForSeconds(1f);
            }else{
                loadingScreen.transform.Find("Panel").gameObject.SetActive(false);
            }
        }
        SceneManager.LoadScene(scName); 
    }  

    private void OnTriggerEnter2D(Collider2D other) {        
        if(other.CompareTag("Player")){                   
            teleportPlayer(sceneName);          
        }
    }
    /***********************************************************/


    // teleportation vers un autre point mais de la meme scene en cours
    public void teleportOnaSpot(Vector3 destinationPoint){
        PlayerMove.instance.moveDisable();
        PlayerMove.instance.rb2d.bodyType = RigidbodyType2D.Static;
        animatorFondu.SetTrigger("startFonduOn");                 
        StartCoroutine(delayTp(destinationPoint));      
    }    
    IEnumerator delayTp(Vector3 destinationPoint){
        yield return new WaitForSeconds(0.5f);
        PlayerMove.instance.gameObject.transform.position = destinationPoint;
        cam.goFindPlayer();
        PlayerMove.instance.moveEnable();
        PlayerMove.instance.rb2d.bodyType = RigidbodyType2D.Dynamic;
    }


}
