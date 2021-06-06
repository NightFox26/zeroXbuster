using UnityEngine;
using UnityEngine.UI;

public class AchievmentsManagement : MonoBehaviour
{
    public Achievements[] allAchievements;
    public GameObject achievementPanel;
    public GameObject medalsPanel;
    public Sprite normalMedalSprite;
    public Sprite goldMedalSprite;

    [HideInInspector]
    public int nbNormalMedal;

    [HideInInspector]
    public int nbGoldMedal;

       
    public void showListUi(){
        emptyAchivUi();
        foreach (Achievements achiv in allAchievements)
        {
            GameObject achivVignetRes = (GameObject)Resources.Load("PREFABS/UI/achievment/trophyVignet");
            GameObject achivVignet = Instantiate(achivVignetRes);
            
            string[] achivInfos = getInfosAchiv(achiv);
            achivVignet.transform.Find("Text").GetComponent<Text>().text = achivInfos[0];
            achivVignet.transform.Find("Slider").GetComponent<Slider>().maxValue = int.Parse(achivInfos[1]);
            achivVignet.transform.Find("Slider").GetComponent<Slider>().value =  int.Parse(achivInfos[2]);
            achivVignet.transform.SetParent(achievementPanel.transform);
            achivVignet.transform.localScale = new Vector3(1,1,1);

            
            achivVignet.transform.Find("Image").GetComponent<Image>().sprite = achiv.getUnknowIcon(); 

            if(achiv.isComplete){
                achivVignet.GetComponent<Image>().color = new Color(0.7f,0.2f,0.9f);
                achivVignet.transform.Find("Image").GetComponent<Image>().sprite = achiv.icon;                
                if(achiv.isGoldMedal){
                    nbGoldMedal++;
                }else{
                    nbNormalMedal++;
                }
            }
        }
        updateMedalsCounter();
    }

    private void updateMedalsCounter(){
        medalsPanel.transform.Find("qtMedal1").GetComponent<Text>().text = ""+nbNormalMedal;
        medalsPanel.transform.Find("qtMedal2").GetComponent<Text>().text = ""+nbGoldMedal;
    }

    private string[] getInfosAchiv(Achievements achiv){
        string[] infos = new string[3];
        if(achiv.nbKillEnemies>0){
            infos[0] = "Vaincre "+achiv.nbKillEnemies+" enemies.";
            infos[1] = ""+achiv.nbKillEnemies;
            infos[2] = ""+PlayerAchievements.instance.nbKillEnemies;
            if(PlayerAchievements.instance.nbKillEnemies >= achiv.nbKillEnemies)
                achiv.setCompleted();
        }else if(achiv.nbKillReploids>0){
            infos[0] = "Vaincre "+achiv.nbKillReploids+" Reploids.";
            infos[1] = ""+achiv.nbKillReploids;
            infos[2] = ""+PlayerAchievements.instance.nbKillReploids;
            if(PlayerAchievements.instance.nbKillReploids >= achiv.nbKillReploids)
                achiv.setCompleted();
        }else if(achiv.nbKillBoss>0){
            infos[0] = "Vaincre "+achiv.nbKillBoss+" Mavericks.";
            infos[1] = ""+achiv.nbKillBoss;
            infos[2] = ""+PlayerAchievements.instance.nbKillBoss;
            if(PlayerAchievements.instance.nbKillBoss >= achiv.nbKillBoss)
                achiv.setCompleted();
        }else if(achiv.nbKillDarkBoss>0){
            infos[0] = "Vaincre "+achiv.nbKillDarkBoss+" DarkMavericks.";
            infos[1] = ""+achiv.nbKillDarkBoss;
            infos[2] = ""+PlayerAchievements.instance.nbKillDarkBoss;
            if(PlayerAchievements.instance.nbKillDarkBoss >= achiv.nbKillDarkBoss)
                achiv.setCompleted();
        }else if(achiv.maxLevel>0){
            infos[0] = "Atteindre le level "+achiv.maxLevel;
            infos[1] = ""+achiv.maxLevel;
            infos[2] = ""+PlayerStats.instance.level;
            if(PlayerStats.instance.level >= achiv.maxLevel)
                achiv.setCompleted();
        }else if(achiv.maxZchain>0){
            infos[0] = "Obtenir un Zchain de "+achiv.maxZchain;
            infos[1] = ""+achiv.maxZchain;
            infos[2] = ""+PlayerAchievements.instance.maxZchain;
            if(PlayerAchievements.instance.maxZchain >= achiv.maxZchain)
                achiv.setCompleted();
        }else if(achiv.maxLevelAlia>0){
            infos[0] = "Alia a atteint le level "+achiv.maxLevelAlia;
            infos[1] = ""+achiv.maxLevelAlia;
            infos[2] = ""+AliaStat.instance.level;
            if(AliaStat.instance.level >= achiv.maxLevelAlia)
                achiv.setCompleted();
        }else if(achiv.maxLevelAxl>0){
            infos[0] = "Axl a atteint le level "+achiv.maxLevelAxl;
            infos[1] = ""+achiv.maxLevelAxl;
            infos[2] = ""+AxlStat.instance.level;
            if(AxlStat.instance.level >= achiv.maxLevelAxl)
                achiv.setCompleted();
        }else if(achiv.maxLevelIris>0){
            infos[0] = "Iris a atteint le level "+achiv.maxLevelIris;
            infos[1] = ""+achiv.maxLevelIris;
            infos[2] = ""+IrisStat.instance.level;
            if(IrisStat.instance.level >= achiv.maxLevelIris)
                achiv.setCompleted();
        }else if(achiv.maxLevelDynamo>0){
            infos[0] = "Dynamo a atteint le level "+achiv.maxLevelDynamo;
            infos[1] = ""+achiv.maxLevelDynamo;
            infos[2] = ""+DynamoStat.instance.level;
            if(DynamoStat.instance.level >= achiv.maxLevelDynamo)
                achiv.setCompleted();
        }else if(achiv.maxLevelSigma>0){
            infos[0] = "Sigma a atteint le level "+achiv.maxLevelSigma;
            infos[1] = ""+achiv.maxLevelSigma;
            infos[2] = ""+SigmaStat.instance.level;
            if(SigmaStat.instance.level >= achiv.maxLevelSigma)
                achiv.setCompleted();
        }else if(achiv.maxLevelMegaman>0){
            infos[0] = "Megaman a atteint le level "+achiv.maxLevelMegaman;
            infos[1] = ""+achiv.maxLevelMegaman;
            infos[2] = ""+MegamanStat.instance.level;
            if(MegamanStat.instance.level >= achiv.maxLevelMegaman)
                achiv.setCompleted();
        }else if(achiv.maxLevelVile>0){
            infos[0] = "Vile a atteint le level "+achiv.maxLevelVile;
            infos[1] = ""+achiv.maxLevelVile;
            infos[2] = ""+VileStat.instance.level;
            if(VileStat.instance.level >= achiv.maxLevelVile)
                achiv.setCompleted();
        }
        return infos;
    }

    private void emptyAchivUi(){
        nbNormalMedal = 0;
        nbGoldMedal = 0;
        foreach (Transform child in achievementPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
