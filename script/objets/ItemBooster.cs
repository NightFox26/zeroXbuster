using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemBooster : Item
{   
    private string itemName;
    public Rarity.List rarity;
    public PowersUp.List powerUp1;
    public PowersUp.List powerUp2;
    public PowersUp.List powerUp3;
    public float value1;
    public float value2;
    public float value3;
    public float minValue1 = 1;
    public float maxValue1 = 3;
    public float minValue2 = 1;
    public float maxValue2 = 2;
    public float minValue3 = 5;
    public float maxValue3 = 10;

    public Dictionary<string,object> itemDatas;
    private GameObject player;
    protected GameObject windowInfosItem;
    private bool alreadyTaken = false;


    protected void Start() { 
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject windowInfosItemPrefab = (GameObject) Resources.Load("PREFABS/UI/windowInfoItem");
        windowInfosItem = Instantiate(windowInfosItemPrefab);
        windowInfosItem.SetActive(false);
        

        itemName = gameObject.name;  
        itemDatas = new Dictionary<string,object>();
        GameObject colorItem = transform.Find("cadreColor").gameObject;
        GameObject lightItem = transform.Find("light").gameObject;
             
        if(rarity == Rarity.List.random){
            float randDrop = Random.Range(0,PlayerDropRate.purpleItem + 5); 
            randDrop = (float)System.Math.Round(randDrop,1);  
            randDrop += PlayerStats.instance.luck;            
            
            rarity = Rarity.List.normal;            
            if(randDrop >= PlayerDropRate.purpleItem){
                rarity = Rarity.List.purple;               
            }else if(randDrop >= PlayerDropRate.blueItem){
                rarity = Rarity.List.blue;                
            }else if(randDrop >= PlayerDropRate.greenItem){
                rarity = Rarity.List.green;                
            }
        }

        value1 = minValue1;
        colorItem.GetComponent<SpriteRenderer>().color = ItemColor.gray();
        lightItem.GetComponent<Light>().color = ItemColor.gray();

        if(rarity != Rarity.List.normal){
            value1 = Random.Range(minValue1,maxValue1);            
            value1 = (float)System.Math.Round(value1,1);
            colorItem.GetComponent<SpriteRenderer>().color = ItemColor.green();
            lightItem.GetComponent<Light>().color = ItemColor.green();
        }

        if(rarity == Rarity.List.blue){
            value2 = Random.Range(minValue2,maxValue2);
            value2 = (float)System.Math.Round(value2,1);
            colorItem.GetComponent<SpriteRenderer>().color = ItemColor.blue();
            lightItem.GetComponent<Light>().color = ItemColor.blue();
        }else if(rarity == Rarity.List.purple){
            value2 = Random.Range(minValue2,maxValue2);
            value2 = (float)System.Math.Round(value2,1);

            value3 = Random.Range(minValue3,maxValue3);
            value3 = (float)System.Math.Round(value3,1);
            colorItem.GetComponent<SpriteRenderer>().color = ItemColor.purple();
            lightItem.GetComponent<Light>().color = ItemColor.purple();
        }
        itemDatas = serialisationDataItem();
        fillInfosSmallWindow();
    }

    private void Update() {   
        if(player.transform.position.x >= (transform.position.x - 2f) && player.transform.position.x <= (transform.position.x + 2f)){
            windowInfosItem.SetActive(true);
            windowInfosItem.transform.position = transform.position + new Vector3(0,0.3f,0); 
            windowInfosItem.GetComponent<Animator>().SetBool("isOpen", true);       
        }else{
            windowInfosItem.GetComponent<Animator>().SetBool("isOpen", false);
        }
    }

    protected new void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player") && !alreadyTaken){ 
            if(type == ItemType.List.booster){
                alreadyTaken = true;
                playerGainBooster();
                PlayerAnimationUi.instance.playAnimGetBooster();                
                Destroy(windowInfosItem);
            }                        
        }
        base.OnTriggerEnter2D(col);
    }

    public void playerGainBooster(){
        PlayerGainsObjects.instance.allBoosters.Add(itemDatas); 
        giveStatsToPlayer();
    }

    public void giveStatsToPlayer(){
        if(value1>0)
            PlayerStats.instance.increaseStats("booster", powerUp1, value1);

        if(value2>0)
            PlayerStats.instance.increaseStats("booster", powerUp2, value2);

        if(value3>0)
            PlayerStats.instance.increaseStats("booster", powerUp3, value3);
    }

    private void fillInfosSmallWindow(){
        windowInfosItem.transform.Find("Canvas/Panel/itemName").GetComponent<Text>().text = ""+itemName;
        windowInfosItem.transform.Find("Canvas/Panel/itemName").GetComponent<Text>().color = Utility.findItemColor(itemDatas);
        windowInfosItem.transform.Find("Canvas/Cadre").GetComponent<Image>().color = Utility.findItemColor(itemDatas);
        windowInfosItem.transform.Find("Canvas/Panel/stat1").GetComponent<Text>().text = ""+powerUp1+" : "+value1;
        if(value2 > 0){
            windowInfosItem.transform.Find("Canvas/Panel/stat2").GetComponent<Text>().text = ""+powerUp2+" : "+value2;
        }
        
        if(value3 > 0){
            windowInfosItem.transform.Find("Canvas/Panel/stat3").GetComponent<Text>().text = ""+powerUp3+" : "+value3;
        }
    }
   

    public Dictionary<string,object> serialisationDataItem(string str = null, object obj = null){
        Dictionary<string,object> datas = new Dictionary<string, object>();
        datas.Add("itemName",itemName);
        datas.Add("rarity",rarity);
        datas.Add("powerUp1",powerUp1);
        datas.Add("powerUp2",powerUp2);
        datas.Add("powerUp3",powerUp3);
        datas.Add("value1",value1);
        datas.Add("minValue1",minValue1);
        datas.Add("maxValue1",maxValue1);
        datas.Add("value2",value2);
        datas.Add("minValue2",minValue2);
        datas.Add("maxValue2",maxValue2);
        datas.Add("value3",value3);
        datas.Add("minValue3",minValue3);
        datas.Add("maxValue3",maxValue3);
        datas.Add("price",price);
        if( str != null && obj != null){
            datas.Add(str,obj);
        }        
        return datas;
    }

    

    
}
