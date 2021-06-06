using UnityEngine;

public abstract class Item : MonoBehaviour
{   
    public ItemType.List type;
    public int price = 350;

    protected void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player") || col.CompareTag("deadLine")){            
            Destroy(gameObject);
        }
    } 

}
