using UnityEngine;

public class LoadSystem : MonoBehaviour
{    
    public GameObject triggerTp;
    
    void Start()
    {
        if(MenuManager.needToLoad){
            Debug.Log("je load data");
            MenuManager.needToLoad = false;
            TeleportScript.instance.teleportPlayer("QG",true,false);
            SaveSystem.loadAllDatas();
        }else{
            Debug.Log("je NE load PAS data");
            string sceneToTp = triggerTp.GetComponent<TeleportScript>().sceneName;
            TeleportScript.instance.teleportPlayer(sceneToTp,true,false);
        }
    }
}
