using System.Collections.Generic;
using UnityEngine;
using System;

public class BossSpawner : MonoBehaviour
{
    public string[] bossType;

    [HideInInspector]
    public List<Boss> bossSpawned;
    
    [HideInInspector]
    public int totalNbBossSpawned;
    
    private bool needToRemapBoss = true;

    private string[] difficulty = new string[3]{"easy","normal","hard"};

    private List<int> spawnPosList;

    [HideInInspector]
    public DateTime dateSpawning;

    [HideInInspector]
    public int nbPurifDoneToday;

    public static BossSpawner instance;

     private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de BossSpawner");
            return;
        }
        instance = this;
    } 

    private void Start() {
        //restoreQtPurifAllowedToday();
    }
    private void Update() {  
        if(needToRemapBoss){
            randomiseBossSpawn();            
        }
    } 

    private void randomiseBossSpawn(){       
        dateSpawning = DateTime.Now;
        needToRemapBoss = false;
        bossSpawned = new List<Boss>();
        spawnPosList = new List<int>(){0,1,2,3,4,5,6,7,8,9,10,11};
        int randNbBossSpawning = UnityEngine.Random.Range(1,spawnPosList.Count+1);
        totalNbBossSpawned = randNbBossSpawning;

        for (int i = 0; i < randNbBossSpawning; i++)
        {
            string randTypeBoss = bossType[UnityEngine.Random.Range(0,bossType.Length)];
            string randDifficultyBoss = difficulty[UnityEngine.Random.Range(0,3)];
            int randPosBoss = spawnPosList[UnityEngine.Random.Range(0,spawnPosList.Count)];            
            Boss boss = new Boss(i,randTypeBoss,randDifficultyBoss,randPosBoss);
            spawnPosList.Remove(randPosBoss);            
            bossSpawned.Add(boss);
            //Utility.DumpToConsole(boss);
        }
    }

    public void restoreQtPurifAllowedToday(){
        nbPurifDoneToday = 0;
    }

    public void activateBossRemaping(){
        BossSpawner.instance.needToRemapBoss = true;    
        PlayerStats.instance.restoreFaillingSystem(); 
    }

    public void loadBossSpawned(PlayerDatas datas){
        needToRemapBoss         = false;
        bossSpawned             = datas.bossSpawned;
        dateSpawning            = datas.dateSpawningBoss;
        totalNbBossSpawned      = datas.totalNbBossSpawned;
        nbPurifDoneToday        = datas.nbPurifDoneToday;
    }


}
