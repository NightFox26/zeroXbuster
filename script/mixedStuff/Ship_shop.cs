using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Ship_shop : MonoBehaviour
{    
    public GameObject[] itemsToSell;    
    public GameObject itemToSell;
    public Dictionary<string, object> itemToSellDatas;
    public TextMeshPro priceText;
    private int priceItem; 
    private Locker locker;

    private void Start() {
        locker = GetComponent<Locker>();
        if(locker.isUnlocked()){
            itemToSellDatas = new Dictionary<string, object>();

            int indexOfRandomItem = Random.Range(0,itemsToSell.Length);       

            itemToSell = Instantiate(itemsToSell[indexOfRandomItem],new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity);

            StartCoroutine(setDatas(itemToSell));
            
            priceItem = itemToSell.GetComponent<ItemEquipement>().price;
            priceText.text = ""+priceItem;
        }else{
            transform.parent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && locker.isUnlocked()){
            if(priceItem <= PlayerStats.instance.totalShards){
                CrystalsShardsCounter.instance.removeCrystalShardsValue(priceItem);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator setDatas(GameObject item){
        yield return new WaitForSeconds(1);
        itemToSellDatas = item.GetComponent<ItemEquipement>().itemDatas;        
    }
   
}
