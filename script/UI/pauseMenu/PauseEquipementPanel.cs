using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseEquipementPanel : MonoBehaviour
{
    public GameObject firstBtnPartEquipment;
    public GameObject panelEquipements;
    public GameObject panelItems;
    public GameObject panelCompare;
    public GameObject panelCompareTo;
    public GameObject panelEquipedPart;
    public GameObject btnEquiper;
    private Dictionary<string,object> selectedItemEquipement;
    private string selectedPart;
    public static PauseEquipementPanel instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y'a plusieurs instance de PauseBoosterPanel");
            return;
        }
        instance = this; 
        selectedItemEquipement = new Dictionary<string,object>();    
    }

    private void Update() {
        if(Input.GetButtonDown("Fire3") && btnEquiper.activeSelf){
            btnEquiper.GetComponent<Button>().onClick.Invoke();
        }
    }  

    public void openMenuEquipement(){
        panelItems.SetActive(false);
        panelCompare.SetActive(false);
        btnEquiper.SetActive(false);
        setPointerCursor(firstBtnPartEquipment);
    }

    public void clickOnArmor(){
        deleteAllButton(); 
        panelItems.SetActive(true);
        panelCompare.SetActive(true);
    }

    public void showListSatelite(){
        selectedPart = "satelite1";         
        showList(PlayerGainsObjects.instance.allEquipementsSatelite1);    
    }
    public void showListSatelite2(){
        selectedPart = "satelite2";         
        showList(PlayerGainsObjects.instance.allEquipementsSatelite2);    
    }
    public void showListHelmet(){   
        selectedPart = "helmet";         
        showList(PlayerGainsObjects.instance.allEquipementsHelmet);    
    }
    public void showListBody(){
        selectedPart = "body";    
        showList(PlayerGainsObjects.instance.allEquipementsBody);    
    }
    public void showListGun(){
        selectedPart = "gun";    
        showList(PlayerGainsObjects.instance.allEquipementsGun);    
    }
    public void showListArm(){
        selectedPart = "arm";    
        showList(PlayerGainsObjects.instance.allEquipementsArm);    
    }
    public void showListLeg(){
        selectedPart = "leg";    
        showList(PlayerGainsObjects.instance.allEquipementsLeg);    
    }
    public void showListBooster(){
        selectedPart = "booster";    
        showList(PlayerGainsObjects.instance.allEquipementsBooster);    
    }
    public void showListSword(){
        selectedPart = "sword";    
        showList(PlayerGainsObjects.instance.allEquipementsSword);    
    }   

    private void showList(List<Dictionary<string,object>> equipements){
        int i=0;
        emptyInfosPanel(panelCompareTo);
        emptyInfosPanel(panelEquipedPart);
        foreach (Dictionary<string,object> item in equipements)
        {
            GameObject btn = (GameObject)Instantiate(Resources.Load("PREFABS/UI/pauseMenu/components/ButtonEquipment"));
            GameObject itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+item["equipementType"]+"/"+item["itemName"]) as GameObject;
            Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_"+item["rarity"]);  

            btn.transform.Find("Image").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
            btn.transform.Find("ImageCadre").GetComponent<Image>().sprite = cadrePrefab;
            
            btn.transform.SetParent(panelItems.transform, false);    
            btn.GetComponent<Button>().onClick.AddListener(()=>showItemInfoComparePanel(item,itemPrefab));             
            if(i==0)setPointerCursor(btn);
            i++;      
        }
        showItemEquiped();
    }

    private void showItemInfoComparePanel(Dictionary<string,object> item, GameObject itemPrefab ){ 
        selectedItemEquipement = item;
        emptyInfosPanel(panelCompareTo);  
        fillInfosPanel(selectedItemEquipement,panelCompareTo);
        showItemEquiped();
        btnEquiper.SetActive(true);
    }

    private void showItemEquiped(){
        Dictionary<string,object> itemEquiped = new Dictionary<string, object>();
        if(PlayerEquipments.instance.slotSatelite1.Count > 0 && selectedPart == "satelite1"){
            itemEquiped = PlayerEquipments.instance.slotSatelite1;            
        }else if(PlayerEquipments.instance.slotSatelite2.Count > 0 && selectedPart == "satelite2"){
            itemEquiped = PlayerEquipments.instance.slotSatelite2;            
        }else if(PlayerEquipments.instance.slotHead.Count > 0 && selectedPart == "helmet"){
            itemEquiped = PlayerEquipments.instance.slotHead;            
        }else if(PlayerEquipments.instance.slotBody.Count > 0 && selectedPart == "body"){
            itemEquiped = PlayerEquipments.instance.slotBody;            
        }else if(PlayerEquipments.instance.slotGun.Count > 0 && selectedPart == "gun"){
            itemEquiped = PlayerEquipments.instance.slotGun;            
        }else if(PlayerEquipments.instance.slotArm.Count > 0 && selectedPart == "arm"){
            itemEquiped = PlayerEquipments.instance.slotArm;            
        }else if(PlayerEquipments.instance.slotLeg.Count > 0 && selectedPart == "leg"){
            itemEquiped = PlayerEquipments.instance.slotLeg;            
        }else if(PlayerEquipments.instance.slotBooster.Count > 0 && selectedPart == "booster"){
            itemEquiped = PlayerEquipments.instance.slotBooster;            
        }else if(PlayerEquipments.instance.slotSword.Count > 0 && selectedPart == "sword"){
            itemEquiped = PlayerEquipments.instance.slotSword;            
        }

        emptyInfosPanel(panelEquipedPart);
        if(itemEquiped.Count>0){
            fillInfosPanel(itemEquiped,panelEquipedPart);            
        }
    }

    public void equipeNewPart(){  
        PlayerEquipments.instance.equipeNewPart(selectedItemEquipement);        
        fillInfosPanel(selectedItemEquipement, panelEquipedPart);
        btnEquiper.SetActive(false);
    }
    public void emptyInfosPanel(GameObject infosPanel){
        infosPanel.transform.Find("ButtonEquipment/Image").GetComponent<Image>().sprite = null;
        infosPanel.transform.Find("ButtonEquipment/ImageCadre").GetComponent<Image>().sprite = null;

        infosPanel.transform.Find("name_item").GetComponent<Text>().text = "";
        infosPanel.GetComponent<Image>().color = findItemColor(Rarity.List.normal);        
        infosPanel.transform.Find("infosItem").GetComponent<Text>().text = "";
    }
    public void fillInfosPanel(Dictionary<string,object> item, GameObject infosPanel){
        GameObject itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+item["equipementType"]+"/"+item["itemName"]) as GameObject;  
        Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_"+item["rarity"]);      
        
        infosPanel.GetComponent<Image>().color = findItemColor((Rarity.List)item["rarity"]);
        
        infosPanel.transform.Find("ButtonEquipment/Image").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
        infosPanel.transform.Find("ButtonEquipment/ImageCadre").GetComponent<Image>().sprite = cadrePrefab;

        infosPanel.transform.Find("name_item").GetComponent<Text>().text = (string)item["itemName"];

        infosPanel.transform.Find("infosItem").GetComponent<Text>().text = getAllStatsItem(item);
    }

    private string getAllStatsItem(Dictionary<string,object> item){
        string datas = "";

        if((float)item["value1"]>0)
        datas += item["powerUp1"] + " : "+ item["value1"]+"\n";

        if((float)item["value2"]>0)
        datas += item["powerUp2"] + " : "+ item["value2"]+"\n";

        if((float)item["value3"]>0)
        datas += item["powerUp3"] + " : "+ item["value3"]+"\n";

        return datas;
    }

    public Color findItemColor(Rarity.List rarity){        
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

    private void deleteAllButton(){
        foreach (Transform child in panelItems.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }
}
