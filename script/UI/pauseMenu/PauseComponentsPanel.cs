using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseComponentsPanel : MonoBehaviour
{
    public GameObject itemsContainer;
    public GameObject blackMarketContainer;
    public static PauseComponentsPanel instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y'a plusieurs instance de PauseComponentsPanel");
            return;
        }
        instance = this;        
    }
    
    public void updateListCompo(){
        deleteAllButton();  
        List<string> tempItemsList = new List<string>();    

        foreach (GameObject item in PlayerGainsObjects.instance.allComponents)
        {
            if(!tempItemsList.Contains(item.name)){
                tempItemsList.Add(item.name);
                GameObject btn = (GameObject)Instantiate(Resources.Load("PREFABS/UI/pauseMenu/components/ButtonCompo"));
                btn.transform.Find("Image").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
                int nbItem = PlayerGainsObjects.instance.countItem(item,PlayerGainsObjects.instance.allComponents);
                btn.transform.Find("Text").GetComponent<Text>().text = ""+nbItem;
                btn.transform.SetParent(itemsContainer.transform, false);
            }
        }

        foreach (GameObject item in PlayerGainsObjects.instance.allBlackMarketComponents)
        {            
            GameObject btn = (GameObject)Instantiate(Resources.Load("PREFABS/UI/pauseMenu/components/ButtonBooster"));
            btn.transform.Find("Image").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            btn.transform.SetParent(blackMarketContainer.transform, false);            
        }
    }

    private void deleteAllButton(){
        foreach (Transform child in itemsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in blackMarketContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
