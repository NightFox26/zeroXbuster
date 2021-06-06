using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipLootBox : MonoBehaviour
{
    public bool randLootShip = true;
    public bool isBonusItemShip = false;
    public int maxLootOnShip = 9;
    public GameObject[] randomLoots; 
    public GameObject[] fixedLoots; 
    public GameObject[] lootPoints;

    [HideInInspector]
    public bool eventStarted = false;

    
    private bool shipBottom = false;
    private Vector3 initPosition;

    private void Start() {
        initPosition = gameObject.transform.position;

        if(randLootShip){
            initiateRandomLoot();
        }else if(isBonusItemShip){
            initiateBonusLoot(StageParameters.instance.itemBonusName);
        }else{
            initiateFixedLoot();
        }
    }
    
    private void initiateRandomLoot(){
        int randNbLoots = Random.Range(8,maxLootOnShip+1); 
        while(randNbLoots > 0){
            int randIndexLootStuff = Random.Range(0,randomLoots.Length);            
            GameObject obj = Instantiate(randomLoots[randIndexLootStuff],lootPoints[randNbLoots%3].transform.position,Quaternion.identity);  
            obj.name = randomLoots[randIndexLootStuff].name;         
            randNbLoots--;
        }
    }

    private void initiateBonusLoot(string bonusName){
        Item[] itemsPossibility = null;       
        switch (bonusName)
        {
            case "heal":
                itemsPossibility = ItemsListing.instance.getHealingItemsList();
                maxLootOnShip = Random.Range(1,maxLootOnShip);
                break;
            case "bullet":
                itemsPossibility = ItemsListing.instance.getBulletsReloadItemsList();
                maxLootOnShip = Random.Range(1,maxLootOnShip);
                break;
            case "crystal":
                itemsPossibility = new Item[]{ItemsListing.instance.getShards()};
                maxLootOnShip = Random.Range(5,9);
                break;
            case "components":
                itemsPossibility = ItemsListing.instance.getComponentsList();
                maxLootOnShip = Random.Range(1,maxLootOnShip);
                break;   
            case "booster":
                itemsPossibility = ItemsListing.instance.getBoosterList();
                maxLootOnShip = 1;
                break;        
        }

        while(maxLootOnShip > 0 && itemsPossibility != null){                       
            maxLootOnShip--;
            int randLootIndex = Random.Range(0,itemsPossibility.Length);
            GameObject obj = Instantiate(itemsPossibility[randLootIndex],lootPoints[maxLootOnShip%3].transform.position,Quaternion.identity).gameObject;    
            obj.name = itemsPossibility[randLootIndex].name;         
        }
    }

    private void initiateFixedLoot(){
        int fixedNbLoots = fixedLoots.Length; 
        while(fixedNbLoots > 0){                       
            fixedNbLoots--;
            GameObject obj = Instantiate(fixedLoots[fixedNbLoots],lootPoints[fixedNbLoots%3].transform.position,Quaternion.identity); 
            obj.name = fixedLoots[fixedNbLoots].name;           
        }
    }

    private void FixedUpdate() {        
        if(eventStarted == true){                
            transform.Translate(0,Time.deltaTime*-4f,0);
            StartCoroutine(spaceShipGoingDown());            
        }

        if(shipBottom == true && eventStarted ){                
            transform.Translate(0,Time.deltaTime*8f,0);  
        }    

        if(eventStarted && gameObject.transform.position.y > initPosition.y + 10){
            Destroy(gameObject); 
        }    
    }

   IEnumerator spaceShipGoingDown(){
       yield return new WaitForSeconds(2.5f);
       foreach(BoxCollider2D col in GetComponents<BoxCollider2D>()){
            col.enabled = false;  
       }
       shipBottom = true;
   }  
}
