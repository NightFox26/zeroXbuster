using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectStageUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;

    public GameObject firstBtnSelectedBoss;
    public GameObject firstBtnSelectedStage;
    public GameObject secondBtnSelectedStage;
    public GameObject thirdBtnSelectedStage;
    public GameObject fourthBtnSelectedStage;
    public GameObject fiveBtnSelectedStage;
    public GameObject sixBtnSelectedStage;

    public GameObject panelSelectBoss;
    public GameObject panelSelectStage;
    public GameObject panelPurificationTimerSystem;
    public GameObject panelPurificationLoot;

    public Text purifValue;
    public Text timeValue;
    public GameObject cadrePurifItem;

    public int[] purifValueBoss = new int[]{1,3,5};

    private int qualityValuePurifLoot;


    public Slider faillureSlider;
    public Slider purificationSlider;
    public GameObject[] bossSpawnPoints;

    private int indexBossPointingNav = 0;

    [HideInInspector]
    public bool purificationCompleted = false;
    public static SelectStageUiManagement instance;


    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de SelectStageUiManagement");
            return;
        }
        instance = this;
    }    
    
    private void Update() { 
        if(panelSelectBoss.activeSelf){
                        
            if(purificationCompleted){
                restoreFaillureSystem();
                if(isPurifLootAllowed()){
                    getPurificationReward();
                    BossSpawner.instance.nbPurifDoneToday++;
                }
            }
            if(faillureSlider.value<=0){
                restoreFaillureSystem();
            }
        }

        if(Input.GetButtonDown("Fire2") && RunCompletion.instance.rowOnMap == 1){
            if(panelSelectStage.activeSelf){
                showPanelBoss();
            }else{
                closeMenu();
            }
        }
    }

    public void showPanelBoss(){        
        closeAllPanels();
        drawBossOnMap(BossSpawner.instance.bossSpawned);
        panelSelectBoss.SetActive(true);
        updateFaillureSystem();

        panelPurificationTimerSystem.SetActive(false);        
        if(isPurifLootAllowed()){
            panelPurificationTimerSystem.SetActive(true);
            updateSliderTimer();
            setQualityValuePurifLoot();
        }

        setPointerCursor(firstBtnSelectedBoss);
    }

    private bool isPurifLootAllowed(){
        if(BossSpawner.instance.nbPurifDoneToday < ComputerParameters.instance.qtMaxPurificationAllowed) return true;

        return false;
    }

    private int calculateQualityPurifItem(){
        int purifValue = 0;
        foreach(Boss boss in BossSpawner.instance.bossSpawned){
            if(boss.difficulty == "easy"){
                purifValue += purifValueBoss[0];
            }else if(boss.difficulty == "normal"){
                purifValue += purifValueBoss[1];
            }else if(boss.difficulty == "hard"){
                purifValue += purifValueBoss[2];
            }
        }
        return purifValue;
    }

    private void setQualityValuePurifLoot(){
        qualityValuePurifLoot = calculateQualityPurifItem();
        
        Sprite cadreLoot = null;
        if(qualityValuePurifLoot <= 10){            
            cadreLoot = Resources.Load<Sprite>("utils/cadre_normal") as Sprite;
        }else if(qualityValuePurifLoot <= 23){ 
            cadreLoot = Resources.Load<Sprite>("utils/cadre_green") as Sprite;
        }else if(qualityValuePurifLoot <= 35){        
            cadreLoot = Resources.Load<Sprite>("utils/cadre_blue") as Sprite;
        }else if(qualityValuePurifLoot > 35){   
            cadreLoot = Resources.Load<Sprite>("utils/cadre_purple") as Sprite;                
        }
        cadrePurifItem.GetComponent<Image>().sprite = cadreLoot;
    }

    public void showPanelStage(BossInfo bossInfo){         
        if(RunCompletion.instance.rowOnMap == 1){
            RunCompletion.instance.hydrateRun(bossInfo);
            StageParameters.instance.stageDifficulty = bossInfo.difficulty;
        }

        if(RunCompletion.instance.stagesGrid != null ){
            closeAllPanels();
            buildUIstagesGrid();
            panelSelectStage.SetActive(true);
            setPointerOnRow(RunCompletion.instance.rowOnMap);
        }
    }

    private void setPointerOnRow(int i){        
        GameObject row = transform.Find("PanelSelectStage/panelStage/row"+i).gameObject;
        setPointerCursor(findFirstActiveBtnInRow(row));                
    }

    private GameObject findFirstActiveBtnInRow(GameObject row){        
        for (int i = 0; i < row.transform.childCount; i++)
        {
            if(row.transform.GetChild(i).gameObject.activeSelf == true)
            {
                return row.transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

    private void closeAllPanels(){        
        panelSelectBoss.SetActive(false);
        panelSelectStage.SetActive(false);
        panelPurificationLoot.SetActive(false);
    }

    public void closeMenu(){
        if(isMenuOpen){
            gameObject.SetActive(false);
            isMenuOpen = false;
            PlayerMove.instance.moveEnable();
            SaveSystem.saveAllDatas(); 
        }
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    private void drawBossOnMap(List<Boss> allBoss){
        disableAllPointSpawningBoss();
        foreach (Boss boss in allBoss)
        {
            GameObject boxOnMap = bossSpawnPoints[boss.positionOnMap];
            boxOnMap.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/selectStage/boss/"+boss.type+"_"+boss.difficulty);
            boxOnMap.GetComponent<Image>().enabled = true;
            boxOnMap.GetComponent<BossInfo>().hydrate(boss);
        }
    }

    private void disableAllPointSpawningBoss(){
        foreach (GameObject point in bossSpawnPoints)
        {
            point.GetComponent<Image>().enabled = false;
        }
    }

    private void nextBossPointing(){
        indexBossPointingNav++;
        if(indexBossPointingNav > BossSpawner.instance.bossSpawned.Count-1){
            indexBossPointingNav = 0;
        }
        print(indexBossPointingNav);
    }

    private void previousBossPointing(){
        indexBossPointingNav--;
        if(indexBossPointingNav < 0){
            indexBossPointingNav = BossSpawner.instance.bossSpawned.Count-1;
        }
        print(indexBossPointingNav);
    }


    // gestion du panel STAGE
    private void buildUIstagesGrid(){
        disableRows(1, 6);
        desactivateAllStageVignet();

        foreach(Stage stage in RunCompletion.instance.stagesGrid){
            enableRow(stage.floor);            
            activateStageVignet(stage.name,stage.floor,stage.room,stage.isBonusStage,stage.item_bonus);

            panelSelectStage.transform.Find("panelStage/row"+stage.floor+"/stage_img_"+stage.room).gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

            panelSelectStage.transform.Find("panelStage/row"+stage.floor+"/stage_img_"+stage.room).gameObject.GetComponent<Button>().onClick.AddListener(delegate {launchLevel(stage.name,stage.item_bonus);});

            if(stage.isFinished){
                panelSelectStage.transform.Find("panelStage/row"+stage.floor+"/stage_img_"+stage.room+"/zero").gameObject.GetComponent<Image>().enabled = true;
            }

            if(stage.room == 2){
                panelSelectStage.transform.Find("panelStage/row"+stage.floor+"/stage_img_"+stage.room+"/boss_img").gameObject.GetComponent<Image>().enabled = false;
            }

            if(stage.hasBoss){               
                panelSelectStage.transform.Find("panelStage/row"+stage.floor+"/stage_img_"+stage.room+"/boss_img").gameObject.GetComponent<Image>().enabled = true;
                panelSelectStage.transform.Find("panelStage/row"+stage.floor+"/stage_img_"+stage.room+"/boss_img").gameObject.GetComponent<Image>().sprite = RunCompletion.instance.spriteBoss;
            }
        }
    }

    

    private void disableRows(int mini, int maxi){
        for (int i = mini; i <= maxi; i++){
            panelSelectStage.transform.Find("panelStage/row"+i).gameObject.SetActive(false); 
        }
    }

    private void enableRow(int i){       
        panelSelectStage.transform.Find("panelStage/row"+i).gameObject.SetActive(true);         
    }

    private void desactivateAllStageVignet(){
        for (int i = 1; i <= 6; i++){
            for (int j = 1; j <= 3; j++){
                desactivateStageVignet(i,j);
            } 
        }
    }

    private void activateStageVignet(string name,int floor,int room,bool isBonusStage,string itemBonus){
        panelSelectStage.transform.Find("panelStage/row"+floor+"/stage_img_"+room+"/bonus_img").gameObject.SetActive(true);
        panelSelectStage.transform.Find("panelStage/row"+floor+"/stage_img_"+room+"/bonus_img").gameObject.GetComponent<Image>().sprite = getBonusSpriteUrl(itemBonus);

        panelSelectStage.transform.Find("panelStage/row"+floor+"/stage_img_"+room).gameObject.SetActive(true);        
        panelSelectStage.transform.Find("panelStage/row"+floor+"/stage_img_"+room).gameObject.GetComponent<Image>().sprite = getStageImgUrl(isBonusStage, name);
        if(isBonusStage){
            panelSelectStage.transform.Find("panelStage/row"+floor+"/stage_img_"+room+"/bonus_img").gameObject.SetActive(false);
        }       
    }

    private Sprite getBonusSpriteUrl(string item_bonus){
        if(item_bonus == "heal"){
            return Resources.LoadAll<Sprite>("objets/bigHeal")[11];
        }else if(item_bonus == "bullet"){
            return Resources.LoadAll<Sprite>("objets/bigReloadBullet")[7];
        }else if(item_bonus == "crystal"){
            return Resources.Load<Sprite>("UI/crystalsShards/crystalsShards");
        }else if(item_bonus == "components"){
            return Resources.LoadAll<Sprite>("objets/headBoost")[0];
        }else if(item_bonus == "booster"){
            return Resources.Load<Sprite>("objets/iconBooster");
        }
        return null;
    }

    private Sprite getStageImgUrl(bool isBonusStage, string StageName){
        if(isBonusStage){
            return Resources.Load<Sprite>("UI/selectStage/stage_bonus/"+StageName); 
        }else{
            return Resources.Load<Sprite>("UI/selectStage/stage/"+StageName);            
        }
    }


    private void desactivateStageVignet(int floor,int room){
        panelSelectStage.transform.Find("panelStage/row"+floor+"/stage_img_"+room).gameObject.SetActive(false);
    }

    public void launchLevel(string levelName, string item_bonus){          
        setPointerCursor(null);
        StageParameters.instance.itemBonusName = item_bonus;
        StageParameters.instance.stageName = levelName;
        GameoverManager.instance.resetLostCrystals();
        StartCoroutine(hideScreen());
        TeleportScript.instance.teleportPlayer(levelName,true,true);        
    }

    private void updateSliderTimer(){
        purificationSlider.value = 24-Utility.getTimeOfTheDAY();
        purifValue.text = BossSpawner.instance.bossSpawned.Count + "/"+BossSpawner.instance.totalNbBossSpawned;
        timeValue.text = purificationSlider.value+"H";
    }

    private void updateFaillureSystem(){
        faillureSlider.maxValue = PlayerStats.instance.faillureSystemMaxValue;
        faillureSlider.value = PlayerStats.instance.faillureSystemCurrentValue;
    }

    private void restoreFaillureSystem(){
        faillureSlider.value = PlayerStats.instance.faillureSystemMaxValue;
    }

    IEnumerator hideScreen(){
        yield return new WaitForSeconds(1);
        closeMenu();
    }

    private void getPurificationReward(){
        purificationCompleted = false;
        panelPurificationLoot.SetActive(true);
        StartCoroutine(hidePurificationReward());
    }

    IEnumerator hidePurificationReward(){
        restoreFaillureSystem();
        yield return new WaitForSeconds(2);        
        panelPurificationLoot.SetActive(false);
    }

}
