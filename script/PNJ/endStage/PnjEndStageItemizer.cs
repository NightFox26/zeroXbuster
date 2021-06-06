using UnityEngine;
using System.Collections.Generic;

public abstract class PnjEndStageItemizer : PnjEndStage
{
    protected bool playerInsidePnjBox;
    private GameObject ui;
    protected GameObject uiPanelItem;
    private GameObject uiPanelNoItem;
    protected GameObject uiPanelInfos;
    protected GameObject uiItemsBox;
    protected GameObject uiItemsPrice;
    private GameObject btnForUi;  
    private bool arrowReleased;
    protected bool itemListReady;
    protected List<Dictionary<string,object>> allPossessedItems;
    protected int indexItem = 0;

    new void Start()
    {
        base.Start();
        allPossessedItems = new List<Dictionary<string, object>>();

        if(transform.Find("Canvas")){
            ui = transform.Find("Canvas").gameObject;
            uiPanelItem = transform.Find("Canvas/PanelItems").gameObject;
            uiPanelNoItem = transform.Find("Canvas/PanelNoItems").gameObject;
            uiPanelInfos = transform.Find("Canvas/PanelInfos").gameObject;
            uiItemsBox = transform.Find("Canvas/PanelItems/IconObj").gameObject;
            uiItemsPrice = transform.Find("Canvas/PanelItems/BtnTransmut/cost").gameObject;
            btnForUi = transform.Find("Btn").gameObject;
            hideUi();
        }
    }
    protected void Update()
    {
        if(PauseMenuManager.instance.isMenuOpen) return;
        
        if(playerInsidePnjBox){
            if(Input.GetButtonDown("Fire2") && ui.activeSelf){
                hideUi();
                PlayerMove.instance.moveEnable();
            }

            if(Input.GetButtonDown("Fire1") && !ui.activeSelf){
                showUi();
                PlayerMove.instance.moveDisable();
            }
        }

        if(uiPanelItem.activeSelf){
            float horizontalMovement = Input.GetAxisRaw("Horizontal"); 
            if(horizontalMovement == 1 && arrowReleased){
                indexItemIncrement();
                fillItemIconUi(allPossessedItems[indexItem]);
                arrowReleased = false;
            }

            if(horizontalMovement == -1 && arrowReleased){
                indexItemDecrement();
                fillItemIconUi(allPossessedItems[indexItem]);
                arrowReleased = false;
            }

            if(horizontalMovement == 0){
                arrowReleased = true;
            }
        }
    }

    private void indexItemIncrement()
    {
        indexItem++;
        if(indexItem >= allPossessedItems.Count){
            indexItem = 0;
        }
    }

    private void indexItemDecrement()
    {
        indexItem--;
        if(indexItem < 0){
            indexItem = allPossessedItems.Count - 1;
        }
    }

    protected void showUi(){
        if(ui != null){
            itemListReady = false;
            ui.SetActive(true);
            btnForUi.SetActive(false);
            setPossessedItems();
        }
    }

    protected void hideUi(){
        if(ui != null){
            ui.SetActive(false);
            btnForUi.SetActive(true);
            uiPanelItem.SetActive(false); 
            uiPanelNoItem.SetActive(false); 
            uiPanelInfos.SetActive(false); 
        }
    } 

    protected void showUiItems(){
        if(allPossessedItems.Count > 0){
            uiPanelItem.SetActive(true); 
            uiPanelNoItem.SetActive(false); 
            uiPanelInfos.SetActive(true);
            fillItemIconUi(allPossessedItems[0]);            
        }else{
            uiPanelItem.SetActive(false); 
            uiPanelNoItem.SetActive(true); 
            uiPanelInfos.SetActive(false); 
        }
    } 

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInsidePnjBox = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            playerInsidePnjBox = false;
        }
    }
    abstract protected void setPossessedItems();
    abstract protected void fillItemIconUi(Dictionary<string, object> item);
    abstract protected void fillItemInfosUi(Dictionary<string, object> item);

}