using UnityEngine;
using System.Collections.Generic;

public class PlayerGainsObjects : MonoBehaviour
{       
    public List<Object> allLootGalleryImage;
    public List<Object> allBoughtGalleryImage;
    public List<Object> allLootedBGMusics;
    public List<Object> allBoughtBGMusics;

    public int[] allSphereAtkPanel = new int[10];
    public int[] allSphereDextPanel = new int[10];
    public int[] allSphereSurvPanel = new int[10];
    public List<Dictionary<string,object>> allBoosters;
    public List<Dictionary<string,object>> allNeuroHacks;
    public List<Dictionary<string,object>> allEquipementsSatelite1;
    public List<Dictionary<string,object>> allEquipementsSatelite2;
    public List<Dictionary<string,object>> allEquipementsHelmet;
    public List<Dictionary<string,object>> allEquipementsBody;
    public List<Dictionary<string,object>> allEquipementsGun;
    public List<Dictionary<string,object>> allEquipementsArm;
    public List<Dictionary<string,object>> allEquipementsLeg;
    public List<Dictionary<string,object>> allEquipementsBooster;
    public List<Dictionary<string,object>> allEquipementsSword;  
    public List<Object> allBlackMarketComponents;  
    public List<Object> allComponents;
    public static PlayerGainsObjects instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de playergainobjects");
            return;
        }
        instance = this;
        allLootedBGMusics = new List<Object>();
        allBoughtBGMusics = new List<Object>();
        allLootGalleryImage = new List<Object>();
        allBoughtGalleryImage = new List<Object>();
        allBlackMarketComponents = new List<Object>();
        allComponents = new List<Object>();
        allBoosters = new List<Dictionary<string, object>>();
        allNeuroHacks = new List<Dictionary<string, object>>();
        allEquipementsSatelite1 = new List<Dictionary<string, object>>();
        allEquipementsSatelite2 = new List<Dictionary<string, object>>();
        allEquipementsHelmet = new List<Dictionary<string, object>>();
        allEquipementsBody = new List<Dictionary<string, object>>();
        allEquipementsGun = new List<Dictionary<string, object>>();
        allEquipementsArm = new List<Dictionary<string, object>>();
        allEquipementsLeg = new List<Dictionary<string, object>>();
        allEquipementsBooster = new List<Dictionary<string, object>>();
        allEquipementsSword = new List<Dictionary<string, object>>();
    }
    
    public int countItem(GameObject itemToFind, List<Object> list){
        int i = 0;
        foreach (GameObject item in list)
        {           
            if(item.name == itemToFind.name){
                i++;
            }
        }
        return i;
    }

    public void buyBgm(GameObject bgm){  
        allBoughtBGMusics.Add(bgm);
    }

    public void removeAllBoosters(){
        foreach(Dictionary<string,object> booster in allBoosters){
            removeOneBooster(booster, false);
        }
        PlayerGainsObjects.instance.allBoosters.Clear();
    }

    public void removeOneBooster(Dictionary<string,object> booster, bool remove = true){        
        if((float)booster["value1"]>0)
            PlayerStats.instance.increaseStats("booster", 
                                (PowersUp.List)booster["powerUp1"], 
                                -(float)booster["value1"]);

        if((float)booster["value2"]>0)
            PlayerStats.instance.increaseStats("booster", 
                                (PowersUp.List)booster["powerUp2"], 
                                -(float)booster["value2"]);

        if((float)booster["value3"]>0)
            PlayerStats.instance.increaseStats("booster",
                                (PowersUp.List)booster["powerUp3"],
                                -(float)booster["value3"]);

        if(remove)
            PlayerGainsObjects.instance.allBoosters.Remove(booster);
    }

    public void removeAllNeuroHacks(){
        foreach(Dictionary<string,object> neuroHack in allNeuroHacks){
            if((float)neuroHack["value1"]<0)
                PlayerStats.instance.increaseStats("neuroHack", 
                                   (PowersUp.List)neuroHack["powerUp1"], 
                                   Mathf.Abs((float)neuroHack["value1"]));
        }
        PlayerGainsObjects.instance.allNeuroHacks.Clear();
    }

    public void removeOneNeuroHacks(Dictionary<string,object> neuroHack){
        if((float)neuroHack["value1"]<0){
            PlayerStats.instance.increaseStats("neuroHack", 
                                (PowersUp.List)neuroHack["powerUp1"], 
                                Mathf.Abs((float)neuroHack["value1"]));
        }
        PlayerGainsObjects.instance.allNeuroHacks.Remove(neuroHack);
    }

    public void deleteEquipement(Dictionary<string,object> equipment){ 
        if( (EquipementType.List)equipment["equipementType"] == EquipementType.List.satelite1){
            allEquipementsSatelite1.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.satelite2){
            allEquipementsSatelite2.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.helmet){
            allEquipementsHelmet.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.body){
            allEquipementsBody.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.gun){
            allEquipementsGun.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.arm){
            allEquipementsArm.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.sword){
            allEquipementsSword.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.leg){
            allEquipementsLeg.Remove(equipment);
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.booster){
            allEquipementsBooster.Remove(equipment);
        }
    }

    public void loadAllObjects(PlayerDatas datas){        
        foreach (string compo in datas.allComponents)
        {
            Object item = Resources.Load("PREFABS/itemsComponents/"+compo);
            allComponents.Add(item);            
        }

        foreach (string lootedBgm in datas.allLootedBGMusics)
        {
            Object item = Resources.Load("PREFABS/itemsBGM/"+lootedBgm);
            allLootedBGMusics.Add(item);            
        }

        foreach (string boughtBgm in datas.allBoughtBGMusics)
        {
            Object item = Resources.Load("PREFABS/itemsBGM/"+boughtBgm);
            allBoughtBGMusics.Add(item);            
        }

        foreach (string lootedImage in datas.allLootGalleryImage)
        {
            GameObject item = Resources.Load("PREFABS/itemsGallery/"+lootedImage) as GameObject;
            allLootGalleryImage.Add(item.GetComponent<ImageGallery>());            
        }

        foreach (string boughtImage in datas.allBoughtGalleryImage)
        {
            GameObject item = Resources.Load("PREFABS/itemsGallery/"+boughtImage) as GameObject;
            allBoughtGalleryImage.Add(item.GetComponent<ImageGallery>());            
        }

        allSphereAtkPanel = datas.allSphereAtkPanel;
        allSphereDextPanel = datas.allSphereDextPanel;
        allSphereSurvPanel = datas.allSphereSurvPanel;

        allEquipementsSatelite1 = datas.allEquipementsSatelite1;
        allEquipementsSatelite2 = datas.allEquipementsSatelite2;
        allEquipementsHelmet    = datas.allEquipementsHelmet;
        allEquipementsBody      = datas.allEquipementsBody;
        allEquipementsGun       = datas.allEquipementsGun;
        allEquipementsArm       = datas.allEquipementsArm;
        allEquipementsLeg       = datas.allEquipementsLeg;
        allEquipementsBooster   = datas.allEquipementsBooster;
        allEquipementsSword     = datas.allEquipementsSword;
    }

}