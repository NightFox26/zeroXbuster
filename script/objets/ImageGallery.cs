using UnityEngine;

public class ImageGallery : Item
{    
    public Sprite image;
    public string galleryName = "artworks";
    public int galleryPos = 0;
    public int statPrice = 0;

    public PowersUp.List powerUp;
    public int powerUpValue = 5;
    public GameObject chest;



    void Start()
    {
        foreach(ImageGallery img in PlayerGainsObjects.instance.allLootGalleryImage){
            if(img.galleryName == this.galleryName && img.galleryPos == this.galleryPos){                
                Instantiate(chest,gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public new void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GameObject item = Resources.Load("PREFABS/itemsGallery/"+gameObject.name.Replace("(Clone)","")) as GameObject;
            PlayerGainsObjects.instance.allLootGalleryImage.Add(item.GetComponent<ImageGallery>());
        }
        base.OnTriggerEnter2D(other);   
    }
}
