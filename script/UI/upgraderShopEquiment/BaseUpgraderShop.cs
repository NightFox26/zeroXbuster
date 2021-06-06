using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public abstract class BaseUpgraderShop : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;

    public GameObject firstBtnSelected;
    public GameObject nextBtnSelected;
    public GameObject panelUpgrader;
    public GameObject panelDismantle;
    public GameObject panelNoItemUpgradable;
    public Text shardsQt;
    public GameObject windowInfoItem;
    public GameObject boxInfObjToUp;
    public GameObject boxInfObjToUped;
    private GameObject uiBoxAllitems;
    public GameObject panelItemMaximised;
    protected GameObject uiBoxComponents;
    public Button btnUpgrade;

    private GameObject prefabIconObj;
    public ArmorDestroyer DestroyerSpawnerPoint;
    protected float amoutValUpgrade = 0;
    protected bool isUpgradable;
    protected bool needToRefresh;

    [HideInInspector]
    protected string armorType;
    public List<GameObject> allComponentsPossible;
    protected Dictionary<string, object> itemToUpgrade;
    private Dictionary<GameObject, int> componentsNeeded;
    private int priceUpgrade = 0;
    protected List<Dictionary<string, object>> items;
      
    public void Start()
    {             
        items = new List<Dictionary<string, object>>();           
        uiBoxAllitems = panelDismantle.transform.Find("viewportItems/Scroll/panelItems").gameObject;        
        prefabIconObj = (GameObject)Resources.Load("PREFABS/UI/IconObj");        
    }

    public virtual void Update() {
        if(Input.GetButtonDown("Fire2")){
            closeMenu();
        }
        if(itemToUpgrade.Count>0 && needToRefresh){
            shardsQt.text = PlayerStats.instance.totalShards.ToString();
            showItemComponentsNeeded(itemToUpgrade);
            showItemToUpgradeInfo(itemToUpgrade);
            btnUpgrade.transform.Find("price").GetComponent<Text>().text = ""+priceUpgrade;
            checkIfUpgradable();
            needToRefresh = false;
        }
    }

    public void showMainPanel(){        
        closeAllPanels();        
        setPointerCursor(firstBtnSelected);
        btnUpgrade.gameObject.SetActive(true);
        showEquippedItem();
    }

    public void showDismantlePanel(Button btn){
        closeAllPanels();
        panelUpgrader.SetActive(false);
        panelDismantle.SetActive(true);
        setPointerCursor(btn.gameObject);
        emptyInfosPanel();
        windowInfoItem.SetActive(false);
        displayItemsPossessed();
    }

    private void closeAllPanels(){   
        panelUpgrader.SetActive(false);
        panelDismantle.SetActive(false);
        panelNoItemUpgradable.SetActive(false);
        panelItemMaximised.SetActive(false);     
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    public void closeMenu(){
        if(isMenuOpen){            
            isMenuOpen = false;
            gameObject.SetActive(false);
            PlayerMove.instance.moveEnable();
            PlayerActions.instance.actionsEnable();
            Time.timeScale = 1;
            SaveSystem.saveAllDatas();
        }
    }
    
    private void emptyListItemsPossessed(){
        foreach (Transform item in uiBoxAllitems.transform)
        {
            Destroy(item.gameObject);
        }
    }
    public virtual void displayItemsPossessed(){
        emptyListItemsPossessed();
        nextBtnSelected = firstBtnSelected;
        foreach (Dictionary<string,object> item in items)
        {
            GameObject iconItem = Instantiate(prefabIconObj);   
            GameObject itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+item["equipementType"]+"/"+item["itemName"]) as GameObject; 
            iconItem.transform.Find("rarity").GetComponent<Image>().color = Utility.findItemColor(item);  
            iconItem.transform.Find("icon").GetComponent<Image>().sprite = itemPrefab.GetComponent<SpriteRenderer>().sprite;  
            iconItem.GetComponent<Button>().onClick.AddListener(delegate {destroyItem(item,armorType);});
            nextBtnSelected = iconItem;

            //pour afficher les stat au survol de l'objet
            UnityEngine.EventSystems.EventTrigger trigger = iconItem.GetComponent<UnityEngine.EventSystems.EventTrigger>();
            UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((data) => { showItemInfo(item); });
            trigger.triggers.Add(entry);
            /***************************/

            iconItem.transform.SetParent(uiBoxAllitems.transform);
            iconItem.transform.localScale =new Vector3(1,1,1);            
        }
    }
    private void destroyItem(Dictionary<string,object> item, string selectedPart){  
        windowInfoItem.SetActive(false);      
        DestroyerSpawnerPoint.addThisArmorToDestroy(item,selectedPart);
        displayItemsPossessed();
        setPointerCursor(nextBtnSelected);
    }
   
    private void showItemInfo(Dictionary<string,object> item){
        windowInfoItem.SetActive(true);
        GameObject itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+item["equipementType"]+"/"+item["itemName"]) as GameObject;  
        Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_"+item["rarity"]);      
        
        windowInfoItem.GetComponent<Image>().color = Utility.findItemColor(item);
        
        windowInfoItem.transform.Find("ButtonEquipment/Image").GetComponent<Image>().sprite = itemPrefab.GetComponent<SpriteRenderer>().sprite;
        windowInfoItem.transform.Find("ButtonEquipment/ImageCadre").GetComponent<Image>().sprite = cadrePrefab;

        windowInfoItem.transform.Find("name_item").GetComponent<Text>().text = (string)item["itemName"];

        windowInfoItem.transform.Find("infosItem").GetComponent<Text>().text = getAllStatsItem(item);
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
    
    public virtual void showEquippedItem(){        
        GameObject iconItemUi = panelUpgrader.transform.Find("itemToUpgrade/icon").gameObject;
        GameObject iconColorItemUi = panelUpgrader.transform.Find("itemToUpgrade/rarity").gameObject;
        GameObject itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+itemToUpgrade["equipementType"]+"/"+itemToUpgrade["itemName"]) as GameObject; 

        iconItemUi.GetComponent<Image>().sprite = itemPrefab.GetComponent<SpriteRenderer>().sprite;
        iconColorItemUi.GetComponent<Image>().color = Utility.findItemColor(itemToUpgrade);         
    }

    private void showItemComponentsNeeded(Dictionary<string,object> item){
        hideAllComponentsUi();
        if((float)itemToUpgrade["value1"] >= (float)itemToUpgrade["maxValue1"]){
            panelItemMaximised.SetActive(true);
            btnUpgrade.gameObject.SetActive(false);
            return;
        }
        
        if((Rarity.List)item["rarity"] == Rarity.List.normal){
            showComponentUi(1,item["rarity"].ToString());            
            amoutValUpgrade = 0.25f;
        }else if((Rarity.List)item["rarity"] == Rarity.List.green){
            showComponentUi(1,item["rarity"].ToString());
            amoutValUpgrade = 0.5f;
        }else if((Rarity.List)item["rarity"] == Rarity.List.blue){
            showComponentUi(2,item["rarity"].ToString());
            amoutValUpgrade = 0.75f;
        }else if((Rarity.List)item["rarity"] == Rarity.List.purple){
            showComponentUi(3,item["rarity"].ToString());
            amoutValUpgrade = 1;
        }
        calculatePriceUpgrade(amoutValUpgrade);
    }

    private void hideAllComponentsUi(){
        foreach (Transform compoUi in uiBoxComponents.transform){
            compoUi.gameObject.SetActive(false);
        }
    }

    private void showComponentUi(int n,string rarity){
        int i=0;
        componentsNeeded = new Dictionary<GameObject, int>();     
        foreach (Transform compoUi in uiBoxComponents.transform){
            GameObject compo;
            if(i<n){
                compoUi.gameObject.SetActive(true);             

                if(rarity == "normal"){
                    compo = allComponentsPossible[0];                    
                }else{
                    compo = allComponentsPossible[i+1];
                }

                int nbPossessedCompo = PlayerGainsObjects.instance.countItem(compo, PlayerGainsObjects.instance.allComponents);                
                int nbCompoNeed = (int)Mathf.Ceil((float)itemToUpgrade["value1"]/(1+n));

                compoUi.transform.Find("Image").GetComponent<Image>().sprite = compo.GetComponent<SpriteRenderer>().sprite;
                compoUi.transform.Find("Text").GetComponent<Text>().text = " x "+ nbCompoNeed;
                compoUi.transform.Find("nbPossessed").GetComponent<Text>().text = " x "+ nbPossessedCompo;
                
                componentsNeeded.Add(compo,nbCompoNeed);                
            }
            i++;
        }
    }

    private void calculatePriceUpgrade(float priceScale){
        priceUpgrade = (int)Mathf.Ceil(PlayerStats.instance.level * 100 * priceScale * (float)itemToUpgrade["value1"]);
    }

    private void showItemToUpgradeInfo(Dictionary<string,object> item){   
        boxInfObjToUp.SetActive(true); 
        boxInfObjToUped.SetActive(true); 
        boxInfObjToUp.transform.Find("statToUp").GetComponent<TextMeshProUGUI>().text = ""+item["powerUp1"]+" : ";        
        boxInfObjToUp.transform.Find("value").GetComponent<Text>().text = item["value1"]+"/"+item["maxValue1"];    

        boxInfObjToUped.transform.Find("statToUp").GetComponent<TextMeshProUGUI>().text = ""+item["powerUp1"]+" : ";    
        float newVal = (float)item["value1"] + amoutValUpgrade;    
        if(newVal>=(float)item["maxValue1"]){
            newVal = (float)item["maxValue1"];
        }
        boxInfObjToUped.transform.Find("value").GetComponent<Text>().text = ""+newVal;        
    }

    public void emptyInfosPanel(){
        windowInfoItem.transform.Find("ButtonEquipment/Image").GetComponent<Image>().sprite = null;
        windowInfoItem.transform.Find("ButtonEquipment/ImageCadre").GetComponent<Image>().sprite = null;

        windowInfoItem.transform.Find("name_item").GetComponent<Text>().text = "";
        windowInfoItem.GetComponent<Image>().color = ItemColor.gray();        
        windowInfoItem.transform.Find("infosItem").GetComponent<Text>().text = "";
    }    

    private void checkIfUpgradable(){
        isUpgradable = true;                
        btnUpgrade.interactable = true;

        if(PlayerStats.instance.totalShards < priceUpgrade){
            isUpgradable = false;
        }

        foreach(var compo in componentsNeeded)
        {            
            if(PlayerGainsObjects.instance.countItem(compo.Key, PlayerGainsObjects.instance.allComponents)< compo.Value){
                isUpgradable = false;
            }
        }

        if((float)itemToUpgrade["value1"] >= (float)itemToUpgrade["maxValue1"]){
            isUpgradable = false;
        }

        if(!isUpgradable){
            btnUpgrade.interactable = false;
        }
    }

    public void upgradeItem(){
        foreach(var compo in componentsNeeded)
        {          
            int nbComp = compo.Value;
            while (nbComp>0)
            {
                PlayerGainsObjects.instance.allComponents.Remove(compo.Key);
                nbComp--;
            }
            
        }
        CrystalsShardsCounter.instance.removeCrystalShardsValue(priceUpgrade);
        PlayerEquipments.instance.removeEquipement(itemToUpgrade);
        itemToUpgrade["value1"] = (float)itemToUpgrade["value1"] + amoutValUpgrade;
        PlayerEquipments.instance.equipeNewPart(itemToUpgrade);        
        needToRefresh = true;
    }
    
    
}
