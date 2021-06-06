using UnityEngine;
using System.Collections.Generic;

public class PlayerEquipments : MonoBehaviour
{
    public Dictionary<string,object> slotSatelite1;
    public Dictionary<string,object> slotSatelite2;
    public Dictionary<string,object> slotHead;
    public Dictionary<string,object> slotBody;
    public Dictionary<string,object> slotGun;
    public Dictionary<string,object> slotArm;
    public Dictionary<string,object> slotLeg;
    public Dictionary<string,object> slotBooster;
    public Dictionary<string,object> slotSword;
    public static PlayerEquipments instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de PlayerEquipments");
            return;
        }
        instance = this;
        slotSatelite1 = new Dictionary<string, object>();
        slotSatelite2 = new Dictionary<string, object>();
        slotHead = new Dictionary<string, object>();
        slotBody = new Dictionary<string, object>();
        slotGun = new Dictionary<string, object>();
        slotArm = new Dictionary<string, object>();
        slotLeg = new Dictionary<string, object>();
        slotBooster = new Dictionary<string, object>();
        slotSword = new Dictionary<string, object>();
    }

    public void removeEquipement(Dictionary<string,object> equipment){ 
         if( (EquipementType.List)equipment["equipementType"] == EquipementType.List.satelite1){
             removeStatsOldEquipement(PlayerEquipments.instance.slotSatelite1);             
             PlayerEquipments.instance.slotSatelite1 = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.satelite2){
             removeStatsOldEquipement(PlayerEquipments.instance.slotSatelite2);
             PlayerEquipments.instance.slotSatelite2 = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.helmet){
             removeStatsOldEquipement(PlayerEquipments.instance.slotHead);
             PlayerEquipments.instance.slotHead = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.body){
             removeStatsOldEquipement(PlayerEquipments.instance.slotBody);
             PlayerEquipments.instance.slotBody = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.gun){
             removeStatsOldEquipement(PlayerEquipments.instance.slotGun);
             PlayerEquipments.instance.slotGun = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.arm){
             removeStatsOldEquipement(PlayerEquipments.instance.slotArm);
             PlayerEquipments.instance.slotArm = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.sword){
             removeStatsOldEquipement(PlayerEquipments.instance.slotSword);
             PlayerEquipments.instance.slotSword = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.leg){
             removeStatsOldEquipement(PlayerEquipments.instance.slotLeg);
             PlayerEquipments.instance.slotLeg = new Dictionary<string, object>();
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.booster){
             removeStatsOldEquipement(PlayerEquipments.instance.slotBooster);
             PlayerEquipments.instance.slotBooster = new Dictionary<string, object>();
        }
    }

    public void equipeNewPart(Dictionary<string,object> equipment){ 
        if((float)equipment["value1"]>0)
            PlayerStats.instance.increaseStats("equipement", 
                                   (PowersUp.List)equipment["powerUp1"], 
                                   (float)equipment["value1"]);

        if((float)equipment["value2"]>0)
            PlayerStats.instance.increaseStats("equipement", 
                                   (PowersUp.List)equipment["powerUp2"], 
                                   (float)equipment["value2"]);

        if((float)equipment["value3"]>0)
            PlayerStats.instance.increaseStats("equipement",
                                   (PowersUp.List)equipment["powerUp3"],
                                   (float)equipment["value3"]);



        if( (EquipementType.List)equipment["equipementType"] == EquipementType.List.satelite1){
             removeStatsOldEquipement(PlayerEquipments.instance.slotSatelite1);
             PlayerEquipments.instance.slotSatelite1 = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.satelite2){
             removeStatsOldEquipement(PlayerEquipments.instance.slotSatelite2);
             PlayerEquipments.instance.slotSatelite2 = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.helmet){
             removeStatsOldEquipement(PlayerEquipments.instance.slotHead);
             PlayerEquipments.instance.slotHead = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.body){
             removeStatsOldEquipement(PlayerEquipments.instance.slotBody);
             PlayerEquipments.instance.slotBody = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.gun){
             removeStatsOldEquipement(PlayerEquipments.instance.slotGun);
             PlayerEquipments.instance.slotGun = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.arm){
             removeStatsOldEquipement(PlayerEquipments.instance.slotArm);
             PlayerEquipments.instance.slotArm = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.sword){
             removeStatsOldEquipement(PlayerEquipments.instance.slotSword);
             PlayerEquipments.instance.slotSword = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.leg){
             removeStatsOldEquipement(PlayerEquipments.instance.slotLeg);
             PlayerEquipments.instance.slotLeg = equipment;
        }else if((EquipementType.List)equipment["equipementType"] == EquipementType.List.booster){
             removeStatsOldEquipement(PlayerEquipments.instance.slotBooster);
             PlayerEquipments.instance.slotBooster = equipment;
        }
    }

    private void removeStatsOldEquipement(Dictionary<string,object> oldEquipment){
          if(oldEquipment.Count>0){
               if((float)oldEquipment["value1"]>0)
                    PlayerStats.instance.increaseStats("equipement", 
                                             (PowersUp.List)oldEquipment["powerUp1"], 
                                             -(float)oldEquipment["value1"]);

               if((float)oldEquipment["value2"]>0)
                    PlayerStats.instance.increaseStats("equipement", 
                                             (PowersUp.List)oldEquipment["powerUp2"], 
                                             -(float)oldEquipment["value2"]);

               if((float)oldEquipment["value3"]>0)
                    PlayerStats.instance.increaseStats("equipement",
                                             (PowersUp.List)oldEquipment["powerUp3"],
                                             -(float)oldEquipment["value3"]);
          }
    }

    public void loadEquippedStuff(PlayerDatas datas){
         slotSatelite1   = datas.slotSatelite1;
         slotSatelite2   = datas.slotSatelite2;
         slotHead        = datas.slotHead;
         slotBody        = datas.slotBody;
         slotGun         = datas.slotGun;
         slotArm         = datas.slotArm;
         slotLeg         = datas.slotLeg;
         slotBooster     = datas.slotBooster;
         slotSword       = datas.slotSword;
    }

    

    
}
