using UnityEngine;
public abstract class PnjEndStage : MonoBehaviour
{   
    public SpawnConditions.List spawnCondition;
    protected bool conditionOk;
    public bool isSpawningOnBossLevel = false;    
    protected Transform lootPos;
      
    protected private void Start() {
        gameObject.SetActive(false);        
        lootPos = transform.Find("lootPos").transform;
    }   

    public void checkCondition()
    {
        if(!isSpawningOnBossLevel && StageParameters.instance.isBossLevel)
            return;
        
        if(checkTimeCondition() || checkDamageCondition() || checkEnemiesFleeCondition()){
            conditionOk = true;
            showPnj();
        }else{
            conditionOk = false;
        }
    }

    private bool checkTimeCondition(){
        if(spawnCondition == SpawnConditions.List.timeToRun){
            float timeLevel = Time.timeSinceLevelLoad;
            if(timeLevel/60 <= LevelConfig.instance.timeToRunMinutes){
                return true;
            }
        }
        return false;
    }
    private bool checkDamageCondition(){
        if(spawnCondition == SpawnConditions.List.noDamage){
            if(!StageParameters.instance.playerHasTakeDamage){
                return true;
            }
        }
        return false;
    }
    private bool checkEnemiesFleeCondition(){
        if(spawnCondition == SpawnConditions.List.noFleeEnemies){
            if(RankingPanel.instance.nbEnemiesKill >= StageParameters.instance.nbMobSpawn){
                return true;
            }
        }
        return false;
    } 

    void showPnj(){
        gameObject.SetActive(true);
    }

}