using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArmorDestroyManegement : MonoBehaviour
{

    public GameObject panelSat1;
    public GameObject panelSat2;
    public GameObject panelHelmets;
    public GameObject panelBodys;
    public GameObject panelGuns;
    public GameObject panelArms;
    public GameObject panelLegs;
    public GameObject panelBoosters;
    public GameObject panelSwords;
    public ArmorDestroyer DestroyerSpawnerPoint;
    public static ArmorDestroyManegement instance;
     private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de ArmorDestroyManegement");
            return;
        }
        instance = this;
    }


    public void refreshAllList() { 
        showListSatelite1();
        showListSatelite2();
        showListHelmet();
        showListBody();
        showListGun();
        showListArm();
        showListLeg();
        showListBooster();
        showListSword();
    }

    public void showListSatelite1(){
        showList(PlayerGainsObjects.instance.allEquipementsSatelite1,"satelite1",panelSat1);    
    }
    public void showListSatelite2(){
        showList(PlayerGainsObjects.instance.allEquipementsSatelite2,"satelite2",panelSat2);    
    }
    public void showListHelmet(){  
        showList(PlayerGainsObjects.instance.allEquipementsHelmet,"helmet",panelHelmets);    
    }
    public void showListBody(){
        showList(PlayerGainsObjects.instance.allEquipementsBody,"body",panelBodys);    
    }
    public void showListGun(){         
        showList(PlayerGainsObjects.instance.allEquipementsGun,"gun",panelGuns);    
    }
    public void showListArm(){           
        showList(PlayerGainsObjects.instance.allEquipementsArm,"arm",panelArms);    
    }
    public void showListLeg(){          
        showList(PlayerGainsObjects.instance.allEquipementsLeg,"leg",panelLegs);    
    }
    public void showListBooster(){        
        showList(PlayerGainsObjects.instance.allEquipementsBooster,"booster",panelBoosters);    
    }
    public void showListSword(){          
        showList(PlayerGainsObjects.instance.allEquipementsSword,"sword",panelSwords);    
    }   

    private void showList(List<Dictionary<string,object>> equipements,string selectedPart,GameObject panelItems){
        deleteAllItemsButton(panelItems);
        int i=0;        
        foreach (Dictionary<string,object> item in equipements)
        {
            if(!isArmorEquiped(item,selectedPart)){
                GameObject btn = (GameObject)Instantiate(Resources.Load("PREFABS/UI/pauseMenu/components/ButtonEquipment"));
                GameObject itemPrefab = Resources.Load("PREFABS/itemsEquipements/"+item["equipementType"]+"/"+item["itemName"]) as GameObject;
                Sprite cadrePrefab = Resources.Load<Sprite>("utils/cadre_"+item["rarity"]);  

                btn.transform.Find("Image").GetComponent<Image>().sprite = itemPrefab.GetComponent<SpriteRenderer>().sprite;
                btn.transform.Find("ImageCadre").GetComponent<Image>().sprite = cadrePrefab;
                
                btn.transform.SetParent(panelItems.transform, false);    
                btn.GetComponent<Button>().onClick.AddListener(()=>destroyItem(item,selectedPart));             
                if(i==0 && selectedPart == "satelite1")setPointerCursor(btn);
                i++;  
            }    
        }        
    }

    private void destroyItem(Dictionary<string,object> item, string selectedPart){        
        DestroyerSpawnerPoint.addThisArmorToDestroy(item,selectedPart);
        if(selectedPart == "satelite1"){
            showListSatelite1();    
        }else if(selectedPart == "satelite2"){
            showListSatelite2();            
        }else if(selectedPart == "helmet"){
            showListHelmet();            
        }else if(selectedPart == "body"){
            showListBody();        
        }else if(selectedPart == "gun"){
            showListGun();          
        }else if(selectedPart == "arm"){
            showListArm();           
        }else if(selectedPart == "leg"){
            showListLeg();        
        }else if(selectedPart == "booster"){
            showListBooster();         
        }else if(selectedPart == "sword"){
            showListSword();         
        }
    }

     private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    private void deleteAllItemsButton(GameObject panelItems){
        foreach (Transform child in panelItems.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private bool isArmorEquiped(Dictionary<string,object> equipement,string selectedPart){
        if(selectedPart == "satelite1"){
            if(PlayerEquipments.instance.slotSatelite1 == equipement) return true;        
        }else if(selectedPart == "satelite2"){
            if(PlayerEquipments.instance.slotSatelite2 == equipement) return true;            
        }else if(selectedPart == "helmet"){
            if(PlayerEquipments.instance.slotHead == equipement) return true;           
        }else if(selectedPart == "body"){
            if(PlayerEquipments.instance.slotBody == equipement) return true;         
        }else if(selectedPart == "gun"){
            if(PlayerEquipments.instance.slotGun == equipement) return true;            
        }else if(selectedPart == "arm"){
            if(PlayerEquipments.instance.slotArm == equipement) return true;            
        }else if(selectedPart == "leg"){
            if(PlayerEquipments.instance.slotLeg == equipement) return true;         
        }else if(selectedPart == "booster"){
            if(PlayerEquipments.instance.slotBooster == equipement) return true;         
        }else if(selectedPart == "sword"){
            if(PlayerEquipments.instance.slotSword == equipement) return true;           
        }
        return false;
    }
}
