using System.Collections.Generic;
public class SaberUpgradeUiManagement : BaseUpgraderShop
{
    public new void Start()
    {
        armorType = "sword";
        base.Start();
    }
    public override void displayItemsPossessed(){
        items.Clear();     
        foreach (Dictionary<string, object> item in PlayerGainsObjects.instance.allEquipementsSword)
        {
            items.Add(item);
        }
        items.Remove(PlayerEquipments.instance.slotSword);
        base.displayItemsPossessed();
    }

    public override void showEquippedItem(){
        itemToUpgrade = new Dictionary<string, object>();
        if(PlayerEquipments.instance.slotSword.Count > 0){
            itemToUpgrade = PlayerEquipments.instance.slotSword;
            panelUpgrader.SetActive(true);
            panelNoItemUpgradable.SetActive(false);
            uiBoxComponents = panelUpgrader.transform.Find("composNeeded").gameObject;
            needToRefresh = true;  
            base.showEquippedItem();
        }else{
            itemToUpgrade.Clear();
            panelUpgrader.SetActive(false);
            panelNoItemUpgradable.SetActive(true);
        }  
    }
}