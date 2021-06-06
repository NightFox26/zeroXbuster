using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegamanEndStage : PnjEndStageItemizer
{
    public static MegamanEndStage instance;    
    
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de megamanEndStage");
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
                PlayerGainsObjects.instance.removeOneBooster(item);
                GameObject itemPrefab = (GameObject) Resources.Load("PREFABS/itemsBooster/"+item["itemName"]);
                itemPrefab.GetComponent<ItemBooster>().rarity = getNewRarity((Rarity.List)item["rarity"]);
                GameObject itemInst = (GameObject) Instantiate(itemPrefab,lootPos.position,Quaternion.identity);

                itemInst.name = itemPrefab.name;
                setPossessedItems();
            }
        }

        base.Update();        
    }

    private Rarity.List getNewRarity(Rarity.List rarity)
    {
        if(rarity == Rarity.List.normal){
            return Rarity.List.green;
        }else if(rarity == Rarity.List.green){
            return Rarity.List.blue;
        }else if(rarity == Rarity.List.blue){
            return Rarity.List.purple;
        }else{
            return Rarity.List.purple;
        }
    }

    protected override void setPossessedItems()
    {
        indexItem = 0;
        allPossessedItems = new List<Dictionary<string, object>>();
        allPossessedItems = PlayerGainsObjects.instance.allBoosters;
        showUiItems();        
        itemListReady = true;
    }

    protected override void fillItemIconUi(Dictionary<string, object> item){
        GameObject itemPrefab = (GameObject) Resources.Load("PREFABS/itemsBooster/"+item["itemName"]);
        uiItemsBox.transform.Find("rarity").GetComponent<Image>().color = Utility.findItemColor(item);
        uiItemsPrice.GetComponent<Text>().text = ""+item["price"];
        uiItemsBox.transform.Find("icon").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
        fillItemInfosUi(item);
    }

    protected override void fillItemInfosUi(Dictionary<string, object> item)
    {
        emptyInfos();
        uiPanelInfos.transform.Find("itemName").GetComponent<Text>().text = ""+item["itemName"];
        uiPanelInfos.transform.Find("itemName").GetComponent<Text>().color = Utility.findItemColor(item);
        uiPanelInfos.transform.Find("Cadre").GetComponent<Image>().color = Utility.findItemColor(item);
        uiPanelInfos.transform.Find("stat1").GetComponent<Text>().text = item["powerUp1"]+" : "+ item["value1"];
        if((float)item["value2"]>0)
            uiPanelInfos.transform.Find("stat2").GetComponent<Text>().text = item["powerUp2"]+" : "+ item["value2"];
        if((float)item["value3"]>0)
            uiPanelInfos.transform.Find("stat3").GetComponent<Text>().text = item["powerUp3"]+" : "+ item["value3"];
    }

    private void emptyInfos(){
        uiPanelInfos.transform.Find("itemName").GetComponent<Text>().text = "";
        uiPanelInfos.transform.Find("stat1").GetComponent<Text>().text = "";
        uiPanelInfos.transform.Find("stat2").GetComponent<Text>().text = "";
        uiPanelInfos.transform.Find("stat3").GetComponent<Text>().text = "";
    }
}
