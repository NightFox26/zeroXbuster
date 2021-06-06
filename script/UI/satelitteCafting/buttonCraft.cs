using UnityEngine;
using UnityEngine.UI;

public class buttonCraft : MonoBehaviour
{
    public GameObject compo1;
    public int compo1Qt;
    public GameObject compo2;
    public int compo2Qt;
    public GameObject compo3;
    public int compo3Qt;
    public Rarity.List rarity;
    public bool rightSatelite;
    public GameObject spawnSatPoint;
    public GameObject popupNotEnoughtCompo;
    public GameObject popupCraftedSat;
    public GameObject componentBox;
    private GameObject componentBox1;
    private GameObject componentBox2;
    private GameObject componentBox3;

    private void Start() {
        displayNeededCompo();
    }

    public void craftItem(){
        bool craftOk = true;

        if(compo1 != null && compo1Qt > 0){
            int item1Possessed = PlayerGainsObjects.instance.countItem(compo1,PlayerGainsObjects.instance.allComponents);
            if(item1Possessed < compo1Qt){                
                craftOk = false;
            }            
        }

        if(compo2 != null && compo2Qt > 0){
            int item2Possessed = PlayerGainsObjects.instance.countItem(compo2,PlayerGainsObjects.instance.allComponents);
            if(item2Possessed < compo2Qt){                
                craftOk = false;
            }
        }        

        if(compo3 != null && compo3Qt > 0){
            int item3Possessed = PlayerGainsObjects.instance.countItem(compo3,PlayerGainsObjects.instance.allComponents);
            if(item3Possessed < compo3Qt){                
                craftOk = false;
            }
        }

        hideAllPopups();
        if(craftOk){       

            if(compo1 != null && compo1Qt > 0)     
                removeComponentForCrafting(compo1,compo1Qt);

            if(compo2 != null && compo2Qt > 0)
                removeComponentForCrafting(compo2,compo2Qt);
            
            if(compo3 != null && compo3Qt > 0)
                removeComponentForCrafting(compo3,compo3Qt);
            
            CraftingUiManagement.instance.countAllComponentsPossessed();
            instantiateSatelite(rarity,rightSatelite);
            PopupMessage.instance.showPopup(popupCraftedSat);  
        }else{
            PopupMessage.instance.showPopup(popupNotEnoughtCompo);            
        }
    }

    private void displayNeededCompo(){
        if(compo1 != null && compo1Qt > 0){
            componentBox1 = componentBox.transform.Find("compo").gameObject;
            componentBox1.transform.Find("Image").GetComponent<Image>().sprite = compo1.GetComponent<SpriteRenderer>().sprite;
            componentBox1.transform.Find("Text").GetComponent<Text>().text = ""+compo1Qt;
        }

        if(compo2 != null && compo2Qt > 0){
            componentBox2 = componentBox.transform.Find("compo (1)").gameObject;
            componentBox2.transform.Find("Image").GetComponent<Image>().sprite = compo2.GetComponent<SpriteRenderer>().sprite;
            componentBox2.transform.Find("Text").GetComponent<Text>().text = ""+compo2Qt;
        }

        if(compo3 != null && compo3Qt > 0){
            componentBox3 = componentBox.transform.Find("compo (2)").gameObject;
            componentBox3.transform.Find("Image").GetComponent<Image>().sprite = compo3.GetComponent<SpriteRenderer>().sprite;
            componentBox3.transform.Find("Text").GetComponent<Text>().text = ""+compo3Qt;
        }
    }

    private void removeComponentForCrafting(GameObject compo, int compoQt){
        int i = 0;
        while(i<compoQt){
            PlayerGainsObjects.instance.allComponents.Remove(compo);
            i++;
        }

    }

    private void instantiateSatelite(Rarity.List rarity,bool rightSatelite){ 
        GameObject loot; 
        if(rightSatelite){            
            int randInt = Random.Range(0,AllLeftSateliteAvailable.instance.satelites.Length);
            print("rand  = "+randInt);
            loot = Resources.Load("PREFABS/itemsEquipements/satelite2/"+AllLeftSateliteAvailable.instance.satelites[randInt].name) as GameObject;
        }else{            
            int randInt = Random.Range(0,AllLeftSateliteAvailable.instance.satelites.Length);
            loot = Resources.Load("PREFABS/itemsEquipements/satelite2/"+AllLeftSateliteAvailable.instance.satelites[randInt].name) as GameObject;
        }     
        loot.GetComponent<ItemEquipement>().rarity = rarity;
        GameObject cloneLoot = Instantiate(loot,spawnSatPoint.transform.position,Quaternion.identity);
        cloneLoot.name = loot.name;
    }

    private void hideAllPopups(){
        popupCraftedSat.SetActive(false);
        popupNotEnoughtCompo.SetActive(false);
    }

   

}
