using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PauseBoosterPanel : MonoBehaviour
{
    public GameObject boosterContainer;
    public GameObject neuroHackContainer;
    public GameObject itemInfosPanel;
    public GameObject hackInfosPanel;
    public static PauseBoosterPanel instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y'a plusieurs instance de PauseBoosterPanel");
            return;
        }
        instance = this;        
    }
    private void Update() {
        if(Input.GetButtonDown("Fire2")){
            hideInfosWindows();
            itemInfosPanel.SetActive(false);
        }

        if(Input.GetButtonDown("Fire3")){
            if(boosterContainer.transform.childCount>0){
                hideInfosWindows();
                setPointerCursor(boosterContainer.transform.GetChild(0).GetComponent<Button>().gameObject);
            }
        }

        if(Input.GetButtonDown("Jump")){
            if(neuroHackContainer.transform.childCount>0){
                hideInfosWindows();
                itemInfosPanel.SetActive(false);
                setPointerCursor(neuroHackContainer.transform.GetChild(0).GetComponent<Button>().gameObject);
            }
        }
    }
    
    public void updateListBooster(){ 
        hideInfosWindows();
        deleteAllButtonBooster(); 
        int i=0;
        foreach (Dictionary<string,object> item in PlayerGainsObjects.instance.allBoosters)
        {
            GameObject btn = (GameObject)Instantiate(Resources.Load("PREFABS/UI/pauseMenu/components/ButtonBooster"));
            GameObject itemPrefab = null;
            if(item["itemName"].ToString().IndexOf("MusicBooster")>-1){
                itemPrefab = Resources.Load("PREFABS/itemsBGMBooster/"+item["itemName"]) as GameObject;
            }else{
                itemPrefab = Resources.Load("PREFABS/itemsBooster/"+item["itemName"]) as GameObject;
            }
            Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_"+item["rarity"]);
            btn.transform.Find("Image").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
            btn.transform.Find("ImageCadre").GetComponent<Image>().sprite = cadrePrefab;
            btn.transform.SetParent(boosterContainer.transform, false);    
            btn.GetComponent<Button>().onClick.AddListener(()=>showItemInfoPanel(item,itemPrefab,cadrePrefab,"booster")); 
            if(i==0)setPointerCursor(btn);            
            i++;      
        }
        displayStatsBoosters();
    }

    public void updateListNeuroHack(){       
        deleteAllButtonNeuroHack();
        foreach (Dictionary<string,object> item in PlayerGainsObjects.instance.allNeuroHacks)
        {
            GameObject btn = (GameObject)Instantiate(Resources.Load("PREFABS/UI/pauseMenu/components/ButtonBooster"));                       
            GameObject itemPrefab = Resources.Load("PREFABS/itemsNeuroHack/"+item["itemName"]) as GameObject;
           
            Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_red");
            btn.transform.Find("Image").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
            btn.transform.Find("ImageCadre").GetComponent<Image>().sprite = cadrePrefab;
            btn.transform.SetParent(neuroHackContainer.transform, false);    
            btn.GetComponent<Button>().onClick.AddListener(()=>showItemInfoPanel(item,itemPrefab,cadrePrefab,"neuroHack"));
        }
    }    

    private void showItemInfoPanel(Dictionary<string,object> item, GameObject itemPrefab, Sprite cadre, string type){
        hideInfosWindows();
        if(type == "booster"){
            itemInfosPanel.SetActive(true);

            itemInfosPanel.GetComponent<Image>().color = findItemColor((Rarity.List)item["rarity"]);
            itemInfosPanel.transform.Find("boxInfos/name").GetComponent<Text>().text = ""+item["itemName"];

            itemInfosPanel.transform.Find("boxInfos/powerUp1").GetComponent<Text>().text = "Overclocking : "+ item["powerUp1"] + " +"+item["value1"]+" ( min/max : "+item["minValue1"]+"-"+item["maxValue1"]+" )";

            itemInfosPanel.transform.Find("boxInfos/powerUp2").GetComponent<Text>().text = "";
            if((float)item["value2"]>0){
                itemInfosPanel.transform.Find("boxInfos/powerUp2").GetComponent<Text>().text = "Overclocking : "+ item["powerUp2"] + " +"+item["value2"]+" ( min/max : "+item["minValue2"]+"-"+item["maxValue2"]+" )";
            }

            itemInfosPanel.transform.Find("boxInfos/powerUp3").GetComponent<Text>().text = "";
            if((float)item["value3"]>0){
                itemInfosPanel.transform.Find("boxInfos/powerUp3").GetComponent<Text>().text = "Overclocking : "+ item["powerUp3"] + " +"+item["value3"]+" ( min/max : "+item["minValue3"]+"-"+item["maxValue3"]+" )";
            }

            itemInfosPanel.transform.Find("boxInfos/Image").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
            itemInfosPanel.transform.Find("boxInfos/ImageCadre").GetComponent<Image>().sprite = cadre;
        }else if(type == "neuroHack"){
            hackInfosPanel.SetActive(true);
            hackInfosPanel.transform.Find("boxInfos/name").GetComponent<Text>().text = ""+item["itemName"];

            hackInfosPanel.transform.Find("boxInfos/powerUp1").GetComponent<Text>().text = "DownGrade : "+ item["powerUp1"] + " -"+item["value1"];

            hackInfosPanel.transform.Find("boxInfos/Image").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
            hackInfosPanel.transform.Find("boxInfos/ImageCadre").GetComponent<Image>().sprite = cadre;
        }
    }

    private void displayStatsBoosters(){
        changeUiInfosValueColor(transform.Find("StatsInfos/maxHealthVal"),PlayerStats.instance.maxHealthBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/maxBulletQuantityVal"),PlayerStats.instance.maxBulletQuantityBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/damageVal"),PlayerStats.instance.damageBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/bulletDamageVal"),PlayerStats.instance.bulletDamageBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/DefenceVal"),PlayerStats.instance.defenceBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/velocityVal"),PlayerStats.instance.velocityBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/fireRateVal"),PlayerStats.instance.fireRateBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/dashVelocityVal"),PlayerStats.instance.dashVelocityBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/jumpPowerVal"),PlayerStats.instance.jumpPowerBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/luckVal"),PlayerStats.instance.luckBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/criticalVal"),PlayerStats.instance.criticalDmgBoosted);
        changeUiInfosValueColor(transform.Find("StatsInfos/criticalRateVal"),PlayerStats.instance.criticalFreguencyBoosted);      
    }

    private string writePostiveStat(float val){
        if(val>=0){
            return " +"+val;
        }else{
            return " "+val;
        }
    }
    private void changeUiInfosValueColor(Transform textValue, float val){
        if(val < 0){
            textValue.GetComponent<TextMeshProUGUI>().color = Color.red;
        }else{
            textValue.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        textValue.GetComponent<TextMeshProUGUI>().text = writePostiveStat(val);
    }

    private void hideInfosWindows(){
        itemInfosPanel.SetActive(false);
        hackInfosPanel.SetActive(false);
    }

    private void deleteAllButtonBooster(){
        foreach (Transform child in boosterContainer.transform){
            Destroy(child.gameObject);
        }
    }
    private void deleteAllButtonNeuroHack(){
        foreach (Transform child in neuroHackContainer.transform){
            Destroy(child.gameObject);
        }
    }

    private Color findItemColor(Rarity.List rarity){        
        if(rarity == Rarity.List.green){
            return ItemColor.green();
        }else if(rarity == Rarity.List.blue){
            return ItemColor.blue();
        }else if(rarity == Rarity.List.purple){
            return ItemColor.purple();
        }else{
            return ItemColor.gray();
        }
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }
}
