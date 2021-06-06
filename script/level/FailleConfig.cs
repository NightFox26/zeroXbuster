using UnityEngine;
using UnityEngine.Video;

public class FailleConfig : MonoBehaviour
{
    private VideoPlayer bgVideo;
    [HideInInspector]
    public GameObject darkPortal;

    [HideInInspector]
    public bool playerIsInFaille;
    public static FailleConfig instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja un FailleConfig");
            return;
        }
        instance = this;
    }
    private void Start() {
        bgVideo = transform.Find("bgVideo").GetComponent<VideoPlayer>();
        bgVideo.gameObject.SetActive(false);
    }

    public void initFaille(){
        bgVideo.targetCamera = Camera.main;
        bgVideo.gameObject.SetActive(true);
        LevelConfig.instance.stopBgmStage();
        playerIsInFaille = true;
    }
    public void exitFaille(bool isDestroyed = false){
        playerIsInFaille = false;

        if(isDestroyed){
            darkPortal.GetComponent<DarkPortalTp>().destroyPortal();
        }else{
            darkPortal.GetComponent<DarkPortalTp>().exitedFaille();
            PlayerHealth.instance.healing(10);                  
        }
    
        TeleportScript.instance.teleportOnaSpot(FailleConfig.instance.darkPortal.transform.position);
        PlayerMove.instance.moveEnable();        
        Invoke("resetFailleConfig",1f);
    }

    private void resetFailleConfig(){
        bgVideo.targetCamera = null;
        bgVideo.gameObject.SetActive(false);
        LevelConfig.instance.playBgmStage(); 
        PlayerMove.instance.playerSpawning();
    }
}
