using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    public Sprite backgroundImage;
    public bool isNightLevel = false;
    public Transform failleSpawnPos;
    private GameObject mainCamera;
    public Material nightMaterial;
    public Material dayMaterial;

    private GameObject loadingScreen;
    private Animator animatorFondu;

    public bool isLevelWithRanking = true;
    public bool isLevelNeuroHack = false;
    public float timeToRunMinutes = 5.0f;
    private AudioSource bgmStage;
    public Transform exitPos;

    public static LevelConfig instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de LevelConfig");
            return;
        }
        instance = this;

        
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.Find("bg").GetComponent<SpriteRenderer>().sprite = backgroundImage;
        if(isNightLevel){
            mainCamera.transform.Find("bg").GetComponent<SpriteRenderer>().material = nightMaterial;
        }else{
            mainCamera.transform.Find("bg").GetComponent<SpriteRenderer>().material = dayMaterial;
        }
    }

    private void Start() {        
        PlayerMove.instance.moveDisable();
        animatorFondu = TeleportScript.instance.animatorFondu; 
        loadingScreen = TeleportScript.instance.loadingScreen;
        if(Dev_params.instance.enableLoading){
            loadingScreen.transform.Find("Panel").gameObject.SetActive(true);
            loadingScreen.GetComponent<LoadingScreenManager>().showLoading();
            StartCoroutine(waitForFondu());  
        }else{
            if(loadingScreen)
                loadingScreen.transform.Find("Panel").gameObject.SetActive(false);
                
            PlayerMove.instance.playerSpawning();
        }  
        makeSpawnRecoveryShards();    
        spawnPlayerAtPoint();
        StageParameters.instance.resetStageParameters();
        findFailleSpawnPos();
        bgmStage = transform.Find("Audio/stage musque").GetComponent<AudioSource>();  
        GameoverManager.instance.armorToDestroy = getArmorToDestroy();  
    }

    private Dictionary<string, object> getArmorToDestroy(){
        List<Dictionary<string, object>> armors = new List<Dictionary<string, object>>();
        if(PlayerEquipments.instance.slotSatelite1.Count > 0)
            armors.Add(PlayerEquipments.instance.slotSatelite1);
        if(PlayerEquipments.instance.slotSatelite2.Count > 0)
            armors.Add(PlayerEquipments.instance.slotSatelite2);
        if(PlayerEquipments.instance.slotHead.Count > 0)
            armors.Add(PlayerEquipments.instance.slotHead);
        if(PlayerEquipments.instance.slotBody.Count > 0)
            armors.Add(PlayerEquipments.instance.slotBody);
        if(PlayerEquipments.instance.slotGun.Count > 0)
            armors.Add(PlayerEquipments.instance.slotGun);
        if(PlayerEquipments.instance.slotArm.Count > 0)
            armors.Add(PlayerEquipments.instance.slotArm);
        if(PlayerEquipments.instance.slotLeg.Count > 0)
            armors.Add(PlayerEquipments.instance.slotLeg);
        if(PlayerEquipments.instance.slotBooster.Count > 0)
            armors.Add(PlayerEquipments.instance.slotBooster);
        if(PlayerEquipments.instance.slotSword.Count > 0)
            armors.Add(PlayerEquipments.instance.slotSword);

        if(armors.Count>0){
            int i = UnityEngine.Random.Range(0,armors.Count);
            return armors[i];
        }
        return new Dictionary<string, object>();
    }

    IEnumerator waitForFondu(){
        yield return new WaitForSeconds(1);
        animatorFondu.SetTrigger("startFonduOff");        
        loadingScreen.GetComponent<LoadingScreenManager>().stopLoading();        
        PlayerMove.instance.playerSpawning();
        StageParameters.instance.showReadyAnimation();
    }

    private void spawnPlayerAtPoint(){ 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawnPoint =  GameObject.FindGameObjectWithTag("Respawn"); 
        player.transform.position = spawnPoint.transform.position; 
        mainCamera.GetComponent<CameraFollow>().goFindPlayer();
    }

    private void makeSpawnRecoveryShards(){
        GameoverManager gom = GameoverManager.instance;
        if(gom.stageNameLostCrystal == SceneManager.GetActiveScene().name && gom.qtLostCrystal > 0){            
            GameObject shardsRecovering = Resources.Load("PREFABS/items/crystalsRecovering") as GameObject;
            GameObject shards = Instantiate(shardsRecovering,gom.lostCrystalPos,Quaternion.identity);
            shards.GetComponent<CrystalRecover>().crystalQt = gom.qtLostCrystal;
        }
    }

    private void findFailleSpawnPos(){
        if(isLevelNeuroHack){
            try
            {
                failleSpawnPos = GameObject.Find("FAILLE").transform;                
            }
            catch (System.Exception)
            {
                Debug.LogWarning("il manque le gameobjetc 'FAILLE' !!!!!");
            }       
        }
    }

    public void playBgmStage(){
        bgmStage.Play();
    }

    public void stopBgmStage(){
        bgmStage.Stop();
    }

    public void makeSpawnExitTp(){
        GameObject tpPref = (GameObject)Resources.Load("PREFABS/decorsElements/teleporterExit");
        Instantiate(tpPref,exitPos.position,Quaternion.identity);
    }
    public void makeSpawnJenovaTp(){
        GameObject tpPref = (GameObject)Resources.Load("PREFABS/decorsElements/dark portal");
        Instantiate(tpPref,exitPos.position,Quaternion.identity);
    }


}
