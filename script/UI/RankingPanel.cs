using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RankingPanel : MonoBehaviour
{

    public static RankingPanel instance;

    public GameObject rankingPanel;
    public int score = 0;
  
    public int[] palierRanking;

    public int secondsCost = -2;
    public int enemiesCost = 80;
    public int reploidCost = 900;
    public int damageCost = -50;
    public int zchainCost = 120;

    public GameObject teleportManager;

    private bool isRankingWindowActive = false;
    
    public string teleportToAfterRanking = "QG";

    public Text tempsTxt;
    public Text enemiesKillTxt;
    public Text reploidsKillTxt;
    public Text damageTxt;
    public Text comboTxt;
    public Text scoreTxt;
    public Text rankTxt;

    public GameObject rewardShardsPanel; 
    public GameObject rewardBoostersPanel; 
    public GameObject rewardExtraPanel; 

    public Sprite crystalSprite;
    private List<GameObject> extraRewardObj;
    

    public float timeLevel;    
    public int nbEnemiesKill = 0;
    [HideInInspector]
    public int nbReploidsKill = 0;
    [HideInInspector]
    public int nbMavericksKill = 0;
    [HideInInspector]
    public float totalDamageTaken = 0;
    [HideInInspector]
    public int zChainMax = 0;
    
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("deja une instance de ranking panel");
            return;
        }        
        instance = this;
    }

    private void Start() {    
        resetRanking();
        extraRewardObj = new List<GameObject>();
        extraRewardObj.Add(ItemsListing.instance.getHunterPts().gameObject);
        extraRewardObj.Add(ItemsListing.instance.getHunterPts().gameObject);        
        extraRewardObj.Add(ItemsListing.instance.getChipset().gameObject);
    }

    public void resetRanking(){        
        hideAllRewardPanels();         
        nbEnemiesKill = 0;   
        nbReploidsKill = 0;
        nbMavericksKill = 0; 
        totalDamageTaken = 0;
        zChainMax = 0;
        timeLevel = 0;
        tempsTxt.text = "0";
        enemiesKillTxt.text = "0";
        reploidsKillTxt.text = "0";
        damageTxt.text = "0";
        comboTxt.text = "0";
        rankTxt.text = "D";
    }

    private void Update() {
        if(LevelConfig.instance != null){
            if(LevelConfig.instance.isLevelWithRanking){
                timeLevel = Time.timeSinceLevelLoad;
                if(Input.GetButtonDown("Fire1") && isRankingWindowActive){                          
                    hideRankingPanel();            
                }
            }
        }
    }

    private void updateRunStats(){        
        tempsTxt.text = getTimeOfLevel();
        enemiesKillTxt.text = nbEnemiesKill+""; 
        reploidsKillTxt.text = nbReploidsKill + nbMavericksKill +"";
        damageTxt.text = totalDamageTaken+"";
        comboTxt.text = zChainMax+"";
        rankTxt.text = calculateRank();
    }


    private string getTimeOfLevel(){
        int minutes = (int)timeLevel/60;
        int secondes = (int)Mathf.Round(timeLevel - minutes*60);
        return minutes+"min " + secondes+"s";
    }

    private string calculateRank(){
        score = (int)Mathf.Round(timeLevel)*secondsCost + (enemiesCost*nbEnemiesKill) + (reploidCost*(nbReploidsKill+nbMavericksKill))+(int)(damageCost*totalDamageTaken)+(zchainCost*zChainMax);

        // print("timecost = "+ (int)Mathf.Round(timeLevel)*secondsCost);
        // print("enemi = "+ (enemiesCost*nbEnemiesKill));
        // print("reploid = "+ (reploidCost*nbReploidsKill));
        // print("damege = "+ (damageCost*totalDamageTaken));
        // print("chain = "+ (zchainCost*zChainMax));

        scoreTxt.text = score +"";

        if(score < palierRanking[0]){
            return "D";
        }else if(score < palierRanking[1]){
            return "C";
        }else if(score < palierRanking[2]){
            return "B";
        }else if(score < palierRanking[3]){
            return "A";
        }else if(score >= palierRanking[3]){
            return "S";
        }
        return "D";
    }

    private void showRewards(){
        if(rankTxt.text == "D"){
            rankingPanel.transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ranking/rankingD");
            return;
        }else if(rankTxt.text == "C"){  
            rankingPanel.transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ranking/rankingC");          
            displayReward(1);
        }else if(rankTxt.text == "B"){
            rankingPanel.transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ranking/rankingB");
            displayReward(2);
        }else if(rankTxt.text == "A"){
            rankingPanel.transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ranking/rankingA");
            displayReward(3);
        }else if(rankTxt.text == "S"){
            rankingPanel.transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ranking/rankingS");
            displayReward(3);
            rewardExtraPanel.SetActive(true);
        }
    }

    private void displayReward(int nbLoot){
        rewardShardsPanel.SetActive(true);
        rewardBoostersPanel.SetActive(true);
        for (int i = 1; i <= nbLoot; i++)
        {  
            double ratio = System.Math.Round(Random.Range(i-1.0f,i), 2);

            GameObject shardLine = rewardShardsPanel.transform.Find("Loot"+i+"Panel").gameObject;
            shardLine.SetActive(true);
            shardLine.transform.Find("lootImage").GetComponent<Image>().sprite = crystalSprite;
            shardLine.transform.Find("lootText").GetComponent<Text>().text = Mathf.RoundToInt(score/100) + " x " +ratio+" = "+calculateShardByRatio(ratio);

            CrystalsShardsCounter.instance.addCrystalShardsValue(calculateShardByRatio(ratio));
            GameObject boosterLine = rewardBoostersPanel.transform.Find("ImageBoost"+i).gameObject; 
            boosterLine.SetActive(true);
            if(i == 1){
                intantiateBooster(Rarity.List.normal);
            }else if(i == 2){
                intantiateBooster(Rarity.List.green);
            }else if(i == 3){
                intantiateBooster(Rarity.List.blue);
            }
        }

        if(rankTxt.text == "S"){
            rewardBoostersPanel.transform.Find("ImageBoost4").gameObject.SetActive(true);
            int randNbExtraLoot = Random.Range(1,3);
            for (int i = 1; i <= randNbExtraLoot; i++)
            {
                GameObject extraItem = extraRewardObj[Random.Range(0,extraRewardObj.Count)];
                GameObject extraLootBox = rewardExtraPanel.transform.Find("ImageExtraLoot"+i).gameObject;
                extraLootBox.SetActive(true);
                extraLootBox.transform.Find("extraImg").GetComponent<Image>().sprite = extraItem.GetComponent<SpriteRenderer>().sprite;
                PlayerGainsObjects.instance.allComponents.Add(extraItem);
            } 
            intantiateBooster(Rarity.List.purple);
        }
    }

    private void intantiateBooster(Rarity.List rarity){ 
        GameObject booster = ItemsListing.instance.getRandomBooster().gameObject;           
        booster.GetComponent<ItemBooster>().rarity = rarity;
        
        GameObject cloneLoot = Instantiate(booster,new Vector3(0,0,1),Quaternion.identity);
        cloneLoot.name = booster.name;
        StartCoroutine(gainbooster(cloneLoot));        
    }

    IEnumerator gainbooster(GameObject booster){
        yield return new WaitForSeconds(0.5f);
        booster.GetComponent<ItemBooster>().playerGainBooster();    
        print("gain booster "+ booster.name + " - "+ booster.GetComponent<ItemBooster>().rarity +" "+ booster.GetComponent<ItemBooster>().value1);
    }

    private int calculateShardByRatio(double i){
        return (int) System.Math.Round((score/100) * i);
    }

    public void showRankingPanel(){  
        PlayerMove.instance.moveDisable();           
        PlayerActions.instance.actionsDisable();           
        rankingPanel.SetActive(true);         
        updateRunStats();   
        showRewards();
        StartCoroutine(timerBeforeSkip());   
    }    
    public void hideRankingPanel(){        
        rankingPanel.SetActive(false);
        isRankingWindowActive = false;
    }

    private void hideAllRewardPanels(){
        rewardShardsPanel.transform.Find("Loot1Panel").gameObject.SetActive(false);
        rewardShardsPanel.transform.Find("Loot2Panel").gameObject.SetActive(false);
        rewardShardsPanel.transform.Find("Loot3Panel").gameObject.SetActive(false);
        rewardBoostersPanel.transform.Find("ImageBoost1").gameObject.SetActive(false);
        rewardBoostersPanel.transform.Find("ImageBoost2").gameObject.SetActive(false);
        rewardBoostersPanel.transform.Find("ImageBoost3").gameObject.SetActive(false);
        rewardBoostersPanel.transform.Find("ImageBoost4").gameObject.SetActive(false);
        rewardExtraPanel.transform.Find("ImageExtraLoot1").gameObject.SetActive(false);
        rewardExtraPanel.transform.Find("ImageExtraLoot2").gameObject.SetActive(false);
        rewardShardsPanel.SetActive(false);
        rewardBoostersPanel.SetActive(false);
        rewardExtraPanel.SetActive(false);
    }

    IEnumerator timerBeforeSkip(){
        yield return new WaitForSeconds(1f);
        isRankingWindowActive = true;
    }
}
