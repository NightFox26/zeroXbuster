using System.Collections.Generic;
using UnityEngine;

public class playerEnemyKillCounter : MonoBehaviour
{
    public static playerEnemyKillCounter instance;

    private Dictionary<string,int> listEnemies;
    public List<string> listEnemiesGetReward;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une isntance de playerEnemyKillCounter dans la scene");
            return;
        }
        instance = this;
        listEnemies = new Dictionary<string,int>();
        listEnemiesGetReward = new List<string>();        
    }

    public void addKillEnemyCounter(GameObject enemy){   
        if(PlayerHealth.instance.getStealthMode() == true){
            AudioManager.Instance.PlayVoice(PlayerSounds.instance.stealthKillSound);
        }

        string enemyName = enemy.GetComponent<Enemy>().name;     
        if(listEnemies.ContainsKey(enemyName) == false){
            listEnemies.Add(enemyName,1);
        }else{
            listEnemies[enemyName]++;
        }
    }

    public int getNbEnemyKill(GameObject enemy){
        string enemyName = enemy.name; 
        if(listEnemies.ContainsKey(enemyName)==false){
            return 0;
        }
        return listEnemies[enemyName];
    }

    public Dictionary<string,int> getListEnemyKill(){
        return listEnemies;
    }

    public void loadDatasHuntingEnemies(PlayerDatas datas){
        listEnemies = datas.listEnemies;
        listEnemiesGetReward = datas.listEnemiesGetReward;
    }



}
