using UnityEngine;
using System.Collections;
public class QG_manager : MonoBehaviour
{    
    public GameObject looterCompletion;
    public AudioSource ada_good_morning;
    private bool respawnQuotidienne = false;
    
    void Start()
    {            
        PlayerActions.instance.actionsEnable();
        if(RunCompletion.instance.completionFinish){
            RankingPanel.instance.hideRankingPanel();
            makeLooterFall(RunCompletion.instance.difficulty);            
            BossSpawner.instance.bossSpawned.RemoveAll(x => x.id == RunCompletion.instance.bossId);
        }  

        if(BossSpawner.instance.bossSpawned.Count <= 0){            
            SelectStageUiManagement.instance.purificationCompleted = true;
            BossSpawner.instance.activateBossRemaping();
        } 
       
        if(24-Utility.getTimeOfTheDAY() <= 0 || Utility.getDateOfTheDay().Day - BossSpawner.instance.dateSpawning.Day >= 1){            
            respawnQuotidienne = true;
            BossSpawner.instance.restoreQtPurifAllowedToday();
            BossSpawner.instance.activateBossRemaping();
        }

        if(PlayerStats.instance.faillureSystemCurrentValue <= 0){
            BossSpawner.instance.activateBossRemaping();                      
        }
        
        RunCompletion.instance.resetCompletion();
        PlayerGainsObjects.instance.removeAllNeuroHacks();
        PlayerGainsObjects.instance.removeAllBoosters();
        PlayerStats.instance.fullRestore();
        StartCoroutine(waitForSaving());
    }

    public void clickToSave(){
         SaveSystem.saveAllDatas();
    }

    private void giveXshards(int i){
        while(i>0){
            giveSingleItem(ItemsListing.instance.getShards());
            i--;
        }
    }

    private void giveRandomItemInList(Item[] items){
        int randomId = Random.Range(0,items.Length);
        giveSingleItem(items[randomId]);
    }

    private void giveSingleItem(Item item){
        looterCompletion.GetComponent<LootPoint>().loots.Add(item.gameObject);
    }

    private void  makeLooterFall(string difficulty){
        giveXshards(6);
        giveRandomItemInList(ItemsListing.instance.getComponentsList());
        giveRandomItemInList(ItemsListing.instance.getBgmList());

        if(difficulty != "easy"){
            giveXshards(2);            
            giveRandomItemInList(ItemsListing.instance.getComponentsList());
            giveRandomItemInList(ItemsListing.instance.getPictureList());
        }
        
        if(difficulty != "easy" && difficulty != "normal"){
            giveXshards(2);
            giveRandomItemInList(ItemsListing.instance.getComponentsList());
            giveRandomItemInList(ItemsListing.instance.getPictureList());
            giveSingleItem(ItemsListing.instance.getHunterPts());
        }

        StartCoroutine(waitForLooter());
    }   

    IEnumerator waitForLooter(){
        yield return new WaitForSeconds(1.7f);
        looterCompletion.GetComponent<LootPoint>().triggerForLaunching = true;
    }

    IEnumerator waitForSaving(){
        yield return new WaitForSeconds(2);
        SaveSystem.saveAllDatas();

        if(respawnQuotidienne)
            ada_good_morning.Play();
    }

}
