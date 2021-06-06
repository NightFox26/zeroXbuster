using System.Collections.Generic;
public class BlasterUpgradeUiManagement : BaseUpgraderShop
{     
    
    public new void Start()
    {
        armorType = "gun";
        base.Start();
    }

    public override void displayItemsPossessed(){   
        items.Clear();     
        foreach (Dictionary<string, object> item in PlayerGainsObjects.instance.allEquipementsGun)
        {
            items.Add(item);
        }
        items.Remove(PlayerEquipments.instance.slotGun);
        base.displayItemsPossessed();
    }

    public override void showEquippedItem(){
        itemToUpgrade = new Dictionary<string, object>();
        if(PlayerEquipments.instance.slotGun.Count > 0){
            itemToUpgrade = PlayerEquipments.instance.slotGun;            
            panelUpgrader.SetActive(true);
            panelNoItemUpgradable.SetActive(false);
            uiBoxComponents = panelUpgrader.transform.Find("composNeeded").gameObject;            
            needToRefresh = true;            
            base.showEquippedItem();            
        }else{            
            panelUpgrader.SetActive(false);
            panelNoItemUpgradable.SetActive(true);
        }        
    }
}
