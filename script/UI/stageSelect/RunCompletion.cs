using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunCompletion : MonoBehaviour
{    
    public GameObject menuStageSlect; 
    public List<Stage> stagesGrid;
    public string bossType; 
    public string difficulty;
    public Sprite spriteBoss;
    public int bossId;


    public bool completionFinish = false;
    public int rowOnMap = 1;
    public static RunCompletion instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de RunCompletion");
            return;
        }
        instance = this;
    }
    
    public void goToLevelSelection(){ 
        rowOnMap++;  
        if(rowOnMap>2 && difficulty=="easy"){
            TeleportScript.instance.teleportPlayer("QG",false,true);
            completionFinish = true;
            return;
        }else if(rowOnMap>4 && difficulty=="normal"){
            TeleportScript.instance.teleportPlayer("QG",false,true);
            completionFinish = true;
            return;
        }else if(rowOnMap>6 && difficulty=="hard"){
            TeleportScript.instance.teleportPlayer("QG",false,true);
            completionFinish = true;
            return;
        }
        StartCoroutine(showLevelSelection());
    }

    public void hydrateRun(BossInfo bossInfos){
        stagesGrid = bossInfos.stagesGrid;
        bossType = bossInfos.type;
        bossId = bossInfos.id;
        difficulty = bossInfos.difficulty;
        spriteBoss = Resources.Load<Sprite>("UI/selectStage/boss/"+bossType+"_"+difficulty);
    }

    public void resetCompletion(){
        completionFinish = false;
        rowOnMap = 1;
    }

    IEnumerator showLevelSelection(){
        yield return new WaitForSeconds(0.8f);
        menuStageSlect.SetActive(true);
        menuStageSlect.GetComponent<SelectStageUiManagement>().isMenuOpen = true;
        menuStageSlect.GetComponent<SelectStageUiManagement>().showPanelStage(null);
    }




}
