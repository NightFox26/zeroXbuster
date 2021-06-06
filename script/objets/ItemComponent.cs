using UnityEngine;

public class ItemComponent : Item
{
    protected new void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")){
            string itemName = gameObject.name.Replace("(Clone)","");
            if(type == ItemType.List.composants){
                Object item = null;
                if(itemName == "Chipset" || itemName == "HunterPts"){
                    item = Resources.Load("PREFABS/items/"+itemName);
                }else{
                    item = Resources.Load("PREFABS/itemsComponents/"+itemName);
                }
                PlayerGainsObjects.instance.allComponents.Add(item);
            }
        }
        base.OnTriggerEnter2D(col);
    } 
}
