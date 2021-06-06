using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class ShopSimple : MonoBehaviour
{
    private List<Transform> presentoirsPos;
    private GameObject bulleDial;
    private Text priceDial;
    public Item[] itemList;     
    public Item moneyOfTheShop;
    private List<Item> itemsInShop; 
    private float delayTimefreshDial = 0;
    private GameObject player;  

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        presentoirsPos = new List<Transform>();
        itemsInShop = new List<Item>();

        foreach(Transform presentoir in transform.parent.Find("shopPresentoir")){
            presentoirsPos.Add(presentoir);
        }
        bulleDial = transform.parent.Find("bulleDialogue").gameObject;
        bulleDial.SetActive(false);
        priceDial = bulleDial.transform.Find("Canvas/price").GetComponent<Text>();
        instantiateItemsToSell();        
    }

    private void instantiateItemsToSell(){
        for (int i = 0; i < presentoirsPos.Count; i++){
            Item item =  Instantiate(itemList[Random.Range(0,itemList.Length)],presentoirsPos[i].position,Quaternion.identity);

            item.transform.parent = presentoirsPos[i];
            itemsInShop.Add(item);
            foreach (Collider2D col in item.GetComponents<Collider2D>()){
                col.enabled = false;
            }
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void Update()
    {        
        for (int i = 0; i < presentoirsPos.Count; i++){
            if(checkPlayerInFrontItem(i)){
                if((delayTimefreshDial -= Time.deltaTime) <= 0){
                    priceDial.text = ""+itemsInShop[i].price;
                }

                StopAllCoroutines();
                if(Input.GetButtonDown("Fire2")){                    
                    buyItem(itemsInShop[i]);
                    delayTimefreshDial = 1.5f;
                }
                StartCoroutine(displayBulleDialogue());
            }
        }
    }

    private bool checkPlayerInFrontItem(int i){
        if(player.transform.position.x >= presentoirsPos[i].position.x - 0.75f &&
        player.transform.position.x <= presentoirsPos[i].position.x + 0.75f &&
        player.transform.position.y >= presentoirsPos[i].position.y - 0.75f &&
        player.transform.position.y <= presentoirsPos[i].position.y + 0.75f){
            return true;
        }
        return false;
    }

    private bool checkMoneyPlayer(Item item){
        if(moneyOfTheShop.name == "crystal"){
            if(PlayerStats.instance.totalShards>=item.price){
                CrystalsShardsCounter.instance.removeCrystalShardsValue(item.price);
                return true;
            }
        }else if(moneyOfTheShop.type == ItemType.List.composants){
            if(PlayerGainsObjects.instance.countItem(moneyOfTheShop.gameObject,PlayerGainsObjects.instance.allComponents)>=item.price){
                removeComponentForBuy(moneyOfTheShop.gameObject, item.price);
                return true;
            }
        }
        return false;
    }

    private void buyItem(Item item){
        if(checkMoneyPlayer(item)){
            foreach (Collider2D col in item.GetComponents<Collider2D>()){
                col.enabled = true;
            }
            item.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            presentoirsPos.RemoveAt(itemsInShop.IndexOf(item));
            itemsInShop.Remove(item);
            priceDial.text = "Merci";
        }else{
            priceDial.text = "Insuffisant !";
        }
    }

    private void removeComponentForBuy(GameObject compo, int compoQt){
        int i = 0;
        while(i<compoQt){
            PlayerGainsObjects.instance.allComponents.Remove(compo);
            i++;
        }

    }

    IEnumerator displayBulleDialogue(){
        bulleDial.SetActive(true);
        yield return new WaitForSeconds(3f);
        bulleDial.SetActive(false);
    }
}
