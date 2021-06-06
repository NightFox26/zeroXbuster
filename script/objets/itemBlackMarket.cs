using UnityEngine;

public class itemBlackMarket : Item
{
    protected new void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")){
            if(type == ItemType.List.blackMarket){
                string itemName = gameObject.name.Replace("(Clone)","");
                Object item = null;
                item = Resources.Load("PREFABS/itemsBlackMarket/"+itemName);
                PlayerGainsObjects.instance.allBlackMarketComponents.Add(item);
            }
        }
        base.OnTriggerEnter2D(col);
    }  
}
