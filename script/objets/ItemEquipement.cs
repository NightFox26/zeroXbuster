using UnityEngine;

public class ItemEquipement : ItemBooster
{  
    public EquipementType.List equipementType;

    protected new void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")){ 
            if(type == ItemType.List.equipement){ 
                if(equipementType == EquipementType.List.satelite1){                   
                    PlayerGainsObjects.instance.allEquipementsSatelite1.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.satelite2){                   
                    PlayerGainsObjects.instance.allEquipementsSatelite2.Add(base.serialisationDataItem("equipementType", equipementType));
                }else if(equipementType == EquipementType.List.arm){                   
                    PlayerGainsObjects.instance.allEquipementsArm.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.body){                   
                    PlayerGainsObjects.instance.allEquipementsBody.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.booster){                   
                    PlayerGainsObjects.instance.allEquipementsBooster.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.gun){                   
                    PlayerGainsObjects.instance.allEquipementsGun.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.helmet){                   
                    PlayerGainsObjects.instance.allEquipementsHelmet.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.leg){                   
                    PlayerGainsObjects.instance.allEquipementsLeg.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }else if(equipementType == EquipementType.List.sword){                   
                    PlayerGainsObjects.instance.allEquipementsSword.Add(base.serialisationDataItem("equipementType", equipementType)); 
                }                
            }   
            Destroy(windowInfosItem);          
        }
        base.OnTriggerEnter2D(col);
    }
    
}
