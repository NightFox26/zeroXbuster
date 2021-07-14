using System.Collections.Generic;
using UnityEngine;

public class LootPoint : MonoBehaviour
{
    [Header("Liste d'objets")]
    public List<GameObject> loots;

    [Header("Ou aleatoir dans un type")]    
    public ItemType.List itemType;
    public Rarity.List rarityType;
    public int nbLootType;

    [Header("options")]
    public GameObject lootPos;
    public bool isAutomatic = false;    
    public bool triggerForLaunching = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && isAutomatic==false){      
            if(!checkIfLocked()){
                makeAllSpawned();
            }    
        }
    }

    private void Update() {
        if(isAutomatic && triggerForLaunching){
            if(!checkIfLocked()){
                triggerForLaunching = false;
                makeAllSpawned();
            }            
        }
    }

    private void makeAllSpawned(){
        if(loots.Count > 0){
            foreach(GameObject loot in loots){
                GameObject obj = Instantiate(loot,lootPos.transform.position,Quaternion.identity);
                obj.name = loot.name;
            }  
        }else if(itemType != ItemType.List.aucun && nbLootType>0 ){
            if(itemType == ItemType.List.booster){
                for (int i = 0; i < nbLootType; i++)
                {
                    GameObject booster = ItemsListing.instance.getRandomBooster().gameObject;           
                    booster.GetComponent<ItemBooster>().rarity = rarityType;

                    GameObject cloneLoot = Instantiate(booster,lootPos.transform.position,Quaternion.identity);
                    cloneLoot.name = booster.name;
                }
            }
        }
        Destroy(gameObject,1.5f);
    }

    private bool checkIfLocked(){
        if(GetComponent<Locker>()){
            if(GetComponent<Locker>().isUnlocked()) return false;

            return true;
        }
        return false;
    }
}
