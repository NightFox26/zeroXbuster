using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static string pathSaveLocation = Application.persistentDataPath + "/ZeroXbusterSave.nightfox";
    
    public static void saveAllDatas(){        
        GameObject.FindGameObjectWithTag("SaveIcon").GetComponent<Animator>().SetTrigger("saving");        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathSaveLocation,FileMode.Create);

        PlayerDatas datas = new PlayerDatas();

        formatter.Serialize(stream, datas);
        stream.Close();
    }

    public static void loadAllDatas(){
        if(!File.Exists(pathSaveLocation))
            Debug.LogWarning("Fichier de sauvegarde introuvable "+pathSaveLocation);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathSaveLocation, FileMode.Open);

        PlayerDatas playerDatas = formatter.Deserialize(stream) as PlayerDatas;
        PlayerStats.instance.loadStats(playerDatas);
        BossSpawner.instance.loadBossSpawned(playerDatas);
        PlayerGainsObjects.instance.loadAllObjects(playerDatas);
        playerEnemyKillCounter.instance.loadDatasHuntingEnemies(playerDatas);
        PlayerEquipments.instance.loadEquippedStuff(playerDatas);
        ComputerParameters.instance.loadComputerParams(playerDatas);
        PlayerAchievements.instance.loadAchiv(playerDatas);
        PlayerNewMovements.instance.loadNewMovementsObtained(playerDatas);
        loadAllStatsPnj(playerDatas);
        stream.Close();
    }

    public static int[] loadSpherier(string spherierType){
        if(!File.Exists(pathSaveLocation))
            Debug.LogWarning("Fichier de sauvegarde introuvable "+pathSaveLocation);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(pathSaveLocation, FileMode.Open);

        PlayerDatas playerDatas = formatter.Deserialize(stream) as PlayerDatas;
        stream.Close();

        if(spherierType == "atk"){
            return playerDatas.allSphereAtkPanel;
        }else if(spherierType == "dext"){
            return playerDatas.allSphereDextPanel;
        }else if(spherierType == "surv"){
            return playerDatas.allSphereSurvPanel;
        }
        return null;
    }

    private static void loadAllStatsPnj(PlayerDatas playerDatas){
        AliaStat.instance.loadStats(playerDatas.aliaStats);
        AxlStat.instance.loadStats(playerDatas.axlStats);
        DynamoStat.instance.loadStats(playerDatas.dynamoStats);
        IrisStat.instance.loadStats(playerDatas.irisStats);
        MegamanStat.instance.loadStats(playerDatas.megamanStats);
        SigmaStat.instance.loadStats(playerDatas.sigmaStats);
        VileStat.instance.loadStats(playerDatas.vileStats);
    }
}
