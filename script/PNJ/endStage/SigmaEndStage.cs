using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SigmaEndStage : PnjEndStageItemizer
{
    public static SigmaEndStage instance;
    
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de SigmaEndStage");
            return;
        }
        instance = this;
    } 
  
    new void Update()
    {
        if(Input.GetButtonDown("Fire1") && uiPanelItem.activeSelf && itemListReady){
            Dictionary<string, object> item = allPossessedItems[indexItem];
            if(PlayerStats.instance.totalShards >= (int)item["price"]){
                CrystalsShardsCounter.instance.removeCrystalShardsValue((int)item["price"]);
                PlayerGainsObjects.instance.removeOneNeuroHacks(item);
                GameObject itemPrefab = (GameObject) Resources.Load("PREFABS/itemsNeuroHack/"+item["itemName"]);
                GameObject itemInst = (GameObject) Instantiate(itemPrefab,lootPos.position,Quaternion.identity);
                itemInst.GetComponent<ItemNeuroHack>().destroyHack();
                setPossessedItems();
            }
        }

        base.Update();
    }

    protected override void setPossessedItems(){ 
        indexItem = 0;       
        allPossessedItems = new List<Dictionary<string, object>>();
        allPossessedItems = PlayerGainsObjects.instance.allNeuroHacks;
        showUiItems();
        itemListReady = true;
    }
    protected override void fillItemIconUi(Dictionary<string, object> item){
        GameObject itemPrefab = (GameObject) Resources.Load("PREFABS/itemsNeuroHack/"+item["itemName"]);        
        uiItemsPrice.GetComponent<Text>().text = ""+item["price"];
        uiItemsBox.transform.Find("rarity").GetComponent<Image>().color = ItemColor.red();
        uiItemsBox.transform.Find("icon").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;        
        fillItemInfosUi(item);
    }

    protected override void fillItemInfosUi(Dictionary<string, object> item)
    {
        emptyInfos();
        uiPanelInfos.transform.Find("itemName").GetComponent<Text>().text = ""+item["itemName"];
        uiPanelInfos.transform.Find("itemName").GetComponent<Text>().color = ItemColor.red();        
        uiPanelInfos.transform.Find("Cadre").GetComponent<Image>().color = ItemColor.red();
        uiPanelInfos.transform.Find("stat1").GetComponent<Text>().text = item["powerUp1"]+" : "+ item["value1"];
    }
    private void emptyInfos(){
        uiPanelInfos.transform.Find("itemName").GetComponent<Text>().text = "";
        uiPanelInfos.transform.Find("stat1").GetComponent<Text>().text = "";
    }
}
