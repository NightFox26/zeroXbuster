using UnityEngine;

[System.Serializable]
public class Achievements
{
    public Sprite icon;  
    public bool isGoldMedal;
    public int nbKillEnemies;
    public int nbKillReploids;
    public int nbKillBoss;
    public int nbKillDarkBoss;
    public int maxLevel;
    public int maxZchain;
    public int maxLevelAlia;
    public int maxLevelAxl;
    public int maxLevelIris;
    public int maxLevelDynamo;
    public int maxLevelMegaman;
    public int maxLevelSigma;
    public int maxLevelVile;
    public bool isComplete = false;

    public Sprite getUnknowIcon(){
        return Resources.LoadAll<Sprite>("achievements/achievement-1")[52];
    }

    public void setCompleted(){
        isComplete = true;        
    }
    
}
