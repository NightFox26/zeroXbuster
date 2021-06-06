using System.Collections.Generic;
using UnityEngine;

public class ArmorDestroyer : MonoBehaviour
{
    public List<Dictionary<string, object>> armorsToDestroy;
    public GameObject[] allComponents;
    
    private void Start() {
         armorsToDestroy = new List<Dictionary<string, object>>();
    }

    public void addThisArmorToDestroy(Dictionary<string, object> armor, string selectedPart){
        Time.timeScale = 0;
        if(selectedPart == "satelite1"){
            PlayerGainsObjects.instance.allEquipementsSatelite1.Remove(armor);
        }else if(selectedPart == "satelite2"){
            PlayerGainsObjects.instance.allEquipementsSatelite2.Remove(armor); 
        }else if(selectedPart == "helmet"){
            PlayerGainsObjects.instance.allEquipementsHelmet.Remove(armor); 
        }else if(selectedPart == "body"){
            PlayerGainsObjects.instance.allEquipementsBody.Remove(armor); 
        }else if(selectedPart == "gun"){
            PlayerGainsObjects.instance.allEquipementsGun.Remove(armor);   
        }else if(selectedPart == "arm"){
            PlayerGainsObjects.instance.allEquipementsArm.Remove(armor);  
        }else if(selectedPart == "leg"){
            PlayerGainsObjects.instance.allEquipementsLeg.Remove(armor);  
        }else if(selectedPart == "booster"){
            PlayerGainsObjects.instance.allEquipementsBooster.Remove(armor);
        }else if(selectedPart == "sword"){
            PlayerGainsObjects.instance.allEquipementsSword.Remove(armor); 
        }
        armorsToDestroy.Add(armor);
    }

    private void Update() {
        if(armorsToDestroy.Count > 0){
            foreach(Dictionary<string, object> armor in armorsToDestroy){               
               makeLootsComponents(armor);                
            }
            armorsToDestroy.Clear();
        }
    }

    private void makeLootsComponents(Dictionary<string, object> armor){
        int i = Random.Range(0,allComponents.Length);
        int t=0;
        if((Rarity.List)armor["rarity"] == Rarity.List.normal){
            instantiateLoot(allComponents[i]);
        }else if((Rarity.List)armor["rarity"] == Rarity.List.green){
            while(t<2){
                i = Random.Range(0,allComponents.Length);
                instantiateLoot(allComponents[i]);
                t++;
            }
        }else if((Rarity.List)armor["rarity"] == Rarity.List.blue){
            while(t<3){
                i = Random.Range(0,allComponents.Length);
                instantiateLoot(allComponents[i]);
                t++;
            }
        }else if((Rarity.List)armor["rarity"] == Rarity.List.purple){
            while(t<4){
                i = Random.Range(0,allComponents.Length);
                instantiateLoot(allComponents[i]);
                t++;
            }
        }
    }

    private void instantiateLoot(GameObject loot){
        GameObject cloneLoot = Instantiate(loot,transform.position,Quaternion.identity);
        cloneLoot.name = loot.name;
    }
}
