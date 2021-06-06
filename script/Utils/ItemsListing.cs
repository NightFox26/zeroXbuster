using UnityEngine;

public class ItemsListing : MonoBehaviour
{
    private Item[] healingItems;
    private Item[] bulletsItems;
    private Item[] allBoosters;
    private Item[] allNeuroHacks;
    private Item[] allBgm;
    private Item[] allPicture;
    private Item[] allComponents;
    private Item[] allTankers;
    private Item shards;
    private Item hunterPts;
    private Item chipset;
    public static ItemsListing instance;

     private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de ItemsListing");
            return;
        }
        instance = this;
    }

    private void Start() {
        getHealingItemsList();   
        getBulletsReloadItemsList();
        getBoosterList();
        getNeuroHackList();
        getComponentsList();
        getShards();
        getTankers();
    }

    public Item[] getTankers()
    {
        if(allTankers == null){
            allTankers = new Item[]{Resources.Load<Item>("PREFABS/itemsBlackMarket/energieTanker"),
                                    Resources.Load<Item>("PREFABS/itemsBlackMarket/lifeTanker")};
        }
        return allTankers; 
    }

    public Item[] getHealingItemsList(){
        if(healingItems == null){
            healingItems = new Item[]{  Resources.Load<Item>("PREFABS/items/bigHeal"),
                                        Resources.Load<Item>("PREFABS/items/mediumHeal"),
                                        Resources.Load<Item>("PREFABS/items/smallHeal")};
        }
        return healingItems;        
    }

    public Item[] getBulletsReloadItemsList(){
        if(bulletsItems == null){
            bulletsItems = new Item[]{  Resources.Load<Item>("PREFABS/items/bigReloadBullet"),
                                        Resources.Load<Item>("PREFABS/items/mediumReloadBullet"),
                                        Resources.Load<Item>("PREFABS/items/smallReloadBullet")};
        }
        return bulletsItems;        
    }

    public Item[] getBoosterList(){
        if(allBoosters == null){
            allBoosters = Resources.LoadAll<Item>("PREFABS/itemsBooster");
        }
        return allBoosters;        
    }

    public Item[] getNeuroHackList(){
        if(allNeuroHacks == null){
            allNeuroHacks = Resources.LoadAll<Item>("PREFABS/itemsNeuroHack");
        }
        return allNeuroHacks;        
    }

    public Item getRandomBooster(){
        return allBoosters[Random.Range(0,allBoosters.Length)];
    }
    public Item getRandomNeuroHack(){
        return allNeuroHacks[Random.Range(0,allNeuroHacks.Length)];
    }

    public Item[] getBgmList(){
        if(allBgm == null){
            allBgm = Resources.LoadAll<Item>("PREFABS/itemsBGM");
        }
        return allBgm;        
    }

    public Item[] getPictureList(){
        if(allPicture == null){
            allPicture = Resources.LoadAll<Item>("PREFABS/itemsGallery");
        }
        return allPicture;        
    }

    public Item[] getComponentsList(){
        if(allComponents == null){
            allComponents = Resources.LoadAll<Item>("PREFABS/itemsComponents");            
        }
        return allComponents;        
    }

    public Item getRandomComponent(){
        return allComponents[Random.Range(0,allComponents.Length)];
    }

    public Item getShards(){
        if(shards == null){
            shards = Resources.Load<Item>("PREFABS/items/crystal");
        }
        return shards;        
    }

    public Item getHunterPts(){
        if(hunterPts == null){
            hunterPts = Resources.Load<Item>("PREFABS/items/HunterPts");
        }
        return hunterPts;        
    }

    public Item getChipset(){
        if(chipset == null){
            chipset = Resources.Load<Item>("PREFABS/items/Chipset");
        }
        return chipset;        
    }



}
