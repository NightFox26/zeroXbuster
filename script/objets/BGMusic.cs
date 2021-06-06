using UnityEngine;

public class BGMusic : Item
{
    public string prefabName;
    public string title;
    public Sprite image;   
    public PowersUp.List powerup;
    public float powerupIncreasing = 1;
    public bool extraCategorie = true;
    public bool hasBeenBuy = false;
    private Object chest;

    private bool isForHydrate = false;

    void Start()
    {
        if(isForHydrate){
            return;
        }

        chest = Resources.Load("PREFABS/items/chest", typeof(GameObject));
        foreach (GameObject music in PlayerGainsObjects.instance.allLootedBGMusics)
        {            
            if(music.GetComponent<BGMusic>().prefabName == prefabName){
                Instantiate(chest,gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        foreach (GameObject music in PlayerGainsObjects.instance.allBoughtBGMusics)
        {            
            if(music.GetComponent<BGMusic>().prefabName == prefabName){
                Instantiate(chest,gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
    
    private new void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            PlayerGainsObjects.instance.allLootedBGMusics.Add(Resources.Load("PREFABS/itemsBGM/"+prefabName, typeof(GameObject)));
        }
        base.OnTriggerEnter2D(other);
    }

    public void hydrate(GameObject music){
        isForHydrate = true;
        prefabName = music.GetComponent<BGMusic>().prefabName;
        title = music.GetComponent<BGMusic>().title;
        image = music.GetComponent<BGMusic>().image;
        powerup = music.GetComponent<BGMusic>().powerup;
        powerupIncreasing = music.GetComponent<BGMusic>().powerupIncreasing;
        extraCategorie = music.GetComponent<BGMusic>().extraCategorie;
        price = music.GetComponent<BGMusic>().price;
        hasBeenBuy = music.GetComponent<BGMusic>().hasBeenBuy;
    }
}
