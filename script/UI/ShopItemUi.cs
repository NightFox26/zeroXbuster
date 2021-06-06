using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUi : MonoBehaviour
{
    public GameObject panelEquiped;
    public GameObject panelItemShop;
    public Ship_shop shopSlot;

    private void Start() {
        hidePanels();        
    }

    private void Update() {
         if(Input.GetButtonDown("Pause")){              
            hidePanels();                      
        }
    }

    public void showPanels(){
        panelEquiped.SetActive(true);
        panelItemShop.SetActive(true);
    }

    public void hidePanels(){
        panelEquiped.SetActive(false);
        panelItemShop.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            if(shopSlot != null){
                showPanels();
                Dictionary<string, object> itemDatas = new Dictionary<string, object>();
                itemDatas = shopSlot.itemToSell.GetComponent<ItemEquipement>().itemDatas;            
                showItemInfoComparePanel(itemDatas, shopSlot.itemToSell);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            hidePanels();
        }
    }

    private void showItemInfoComparePanel(Dictionary<string,object> itemDatas, GameObject itemPrefab ){ 
        emptyInfosPanel(panelItemShop);  
        fillInfosPanel(itemDatas,panelItemShop);
        showItemEquiped();
    }

    private void showItemEquiped(){
        Dictionary<string,object> itemEquiped = new Dictionary<string, object>();
        string selectedPart = shopSlot.itemToSell.GetComponent<ItemEquipement>().equipementType+"";
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

        emptyInfosPanel(panelEquiped);
        if(itemEquiped.Count>0){
            fillInfosPanel(itemEquiped,panelEquiped,selectedPart);            
        }
    }

    public void emptyInfosPanel(GameObject infosPanel){
        infosPanel.transform.Find("ButtonEquipment/Image").GetComponent<Image>().sprite = null;
        infosPanel.transform.Find("ButtonEquipment/ImageCadre").GetComponent<Image>().sprite = null;

        infosPanel.transform.Find("name_item").GetComponent<Text>().text = "";
        infosPanel.GetComponent<Image>().color = findItemColor(Rarity.List.normal);        
        infosPanel.transform.Find("infosItem").GetComponent<Text>().text = "";
    }

    public void fillInfosPanel(Dictionary<string,object> item, GameObject infosPanel, string selectedPart=""){
        GameObject itemPrefab;
        if(selectedPart != ""){
            itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+selectedPart+"/"+item["itemName"]) as GameObject; 
        }else{
            itemPrefab = shopSlot.itemToSell;
        }
        
        Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_"+item["rarity"]);   
        print(item["rarity"]);
        
        infosPanel.GetComponent<Image>().color = findItemColor((Rarity.List)item["rarity"]);
        
        infosPanel.transform.Find("ButtonEquipment/Image").GetComponent<Image>().sprite = itemPrefab.GetComponent<SpriteRenderer>().sprite;
        infosPanel.transform.Find("ButtonEquipment/ImageCadre").GetComponent<Image>().sprite = cadrePrefab;

        infosPanel.transform.Find("name_item").GetComponent<Text>().text = (string)item["itemName"];

        infosPanel.transform.Find("infosItem").GetComponent<Text>().text = getAllStatsItem(item);
    }
    
    public Color findItemColor(Rarity.List rarity){        
        if(rarity == Rarity.List.green){
            return ItemColor.green();
        }else if(rarity == Rarity.List.blue){
            return ItemColor.blue();
        }else if(rarity == Rarity.List.purple){
            return ItemColor.purple();;
        }else{
            return ItemColor.gray();;
        }
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

}
