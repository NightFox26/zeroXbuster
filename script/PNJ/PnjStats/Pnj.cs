using UnityEngine;
using UnityEngine.UI;

public abstract class Pnj : MonoBehaviour
{
    protected bool playerOnPnj = false;
    protected bool alreadyTalk = false;
    protected bool alreadyHearted = false;
    protected PnjStat stat;
    public GameObject itemForFeed;

    protected void Start() {
        hidePanelHeartGui();
        activateNextDial(stat.getTalked());
        updateLevelGui(stat.getLevel());        
    }

    protected void Update()
    {
        if(playerOnPnj && !alreadyHearted){
            checkIfHeartIsInInventory();
        }

        if(Input.GetButtonDown("Fire1") && playerOnPnj && !alreadyTalk){  
            alreadyTalk = true;            
            stat.talk(); 
            hidePanelTalkGui();     
        }

        if(Input.GetButtonDown("Fire2") && playerOnPnj && !alreadyHearted){
            if(PlayerGainsObjects.instance.allComponents.Contains(itemForFeed)){
                PlayerGainsObjects.instance.allComponents.Remove(itemForFeed);
                alreadyHearted = true;          
                stat.giveHeart();  
                updateLevelGui(stat.getLevel());  
                hidePanelHeartGui();

                if(alreadyTalk){
                    stat.talk(); 
                }  
            }
        }
    }

    protected void checkIfHeartIsInInventory(){
        if(PlayerGainsObjects.instance.allComponents.Contains(itemForFeed)){
            showPanelHeartGui();        
        }
    }

    protected void updateLevelGui(int level){
        transform.Find("Canvas/PanelLevel/Text").GetComponent<Text>().text = "Level "+level;
    }

    protected void hidePanelTalkGui(){
        transform.Find("Canvas/PanelTalk").gameObject.SetActive(false);
    }
    protected void hidePanelHeartGui(){
        transform.Find("Canvas/PanelHeart").gameObject.SetActive(false);
    }
    protected void showPanelHeartGui(){
        transform.Find("Canvas/PanelHeart").gameObject.SetActive(true);
    }

    protected void activateNextDial(int talked){
        try
        {
            transform.Find("Dials/dial-"+talked).gameObject.SetActive(true);            
        }
        catch (System.Exception)
        {
            Debug.LogWarning("le dialogue num: "+talked+" n'existe pas");
        }
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerOnPnj = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerOnPnj = false;
        }
    }
}
