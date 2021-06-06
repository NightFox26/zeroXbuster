using UnityEngine;

public class PlayerAchievements : MonoBehaviour
{
    public int nbKillEnemies;
    public int nbKillReploids;
    public int nbKillBoss;
    public int nbKillDarkBoss;
    public int maxZchain;
    public static PlayerAchievements instance;

    private void Awake() {
        if(instance !=null){
            return;
        }
        instance = this;
    }

    public void loadAchiv(PlayerDatas datas){
        nbKillEnemies = datas.nbKillEnemies;
        nbKillReploids = datas.nbKillReploids;
        nbKillBoss = datas.nbKillBoss;
        nbKillDarkBoss = datas.nbKillDarkBoss;
        maxZchain = datas.maxZchain;
    }
}
