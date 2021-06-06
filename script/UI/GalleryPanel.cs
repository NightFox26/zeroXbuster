using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GalleryPanel : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    public GameObject btnAtworks;
    public GameObject btnZero;
    public GameObject btngirls;
    public GameObject btnHentai;
    public GameObject BtnBuyStat;
    public GameObject panelImgArtwork;
    public GameObject panelImgZero;
    public GameObject panelImgGirls;
    public GameObject panelImgHentai;
    public GameObject viewImagePanel;
    private ImageGallery imageSelected;

    public GameObject popupNotEnoughtShards;


    private void Update() {
        if(Input.GetButtonDown("Fire2") && viewImagePanel.activeSelf == false){
            closeMenu();
        }
        if(Input.GetButtonDown("Fire2") && viewImagePanel.activeSelf == true){
            viewImagePanel.SetActive(false);
            setPointerCursor(btnAtworks);
        }
    }

    public void showPanelArtworks(){
        closeAllArtsPanels();
        updateChestAvailable();
        panelImgArtwork.SetActive(true);
        setPointerCursor(btnAtworks);
    }

    public void showPanelZero(){
        closeAllArtsPanels();
        updateChestAvailable();
        panelImgZero.SetActive(true);
        setPointerCursor(btnZero);
    }

    public void showPanelGils(){
        closeAllArtsPanels();
        updateChestAvailable();
        panelImgGirls.SetActive(true);
        setPointerCursor(btngirls);
    }

    public void showPanelHentai(){
        closeAllArtsPanels();
        updateChestAvailable();
        panelImgHentai.SetActive(true);
        setPointerCursor(btnHentai);
    }

    private void closeAllArtsPanels(){        
        panelImgGirls.SetActive(false);
        panelImgHentai.SetActive(false);
        panelImgArtwork.SetActive(false);
        panelImgZero.SetActive(false);    
        viewImagePanel.SetActive(false);    
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    public void closeMenu(){
        if(isMenuOpen){            
            isMenuOpen = false;
            gameObject.SetActive(false);
            PlayerMove.instance.moveEnable();
            PlayerActions.instance.actionsEnable();
            SaveSystem.saveAllDatas();
        }
    }

    public void showImage(ImageGallery img){
         viewImagePanel.SetActive(true);
         imageSelected = img;
         viewImagePanel.transform.Find("Image").GetComponent<Image>().sprite = img.image;         

        if(PlayerGainsObjects.instance.allBoughtGalleryImage.Contains(imageSelected)){
            BtnBuyStat.SetActive(false);
            viewImagePanel.transform.Find("PanelStatUp").gameObject.SetActive(true);
            viewImagePanel.transform.Find("PanelStatUp/TextStatUp").GetComponent<Text>().text = "Obtenu : "+img.powerUp+"+" + img.powerUpValue;
            return;
        }

         BtnBuyStat.SetActive(true);
         viewImagePanel.transform.Find("PanelStatUp").gameObject.SetActive(false);
         setPointerCursor(BtnBuyStat);
         viewImagePanel.transform.Find("buyStatBtn/TextStatUp").GetComponent<Text>().text = ""+img.powerUp+"+" + img.powerUpValue;
         viewImagePanel.transform.Find("buyStatBtn/price/TextPrice").GetComponent<Text>().text = ""+img.statPrice;
    } 

    public void buyStatImage(){
        if(PlayerStats.instance.totalShards >= imageSelected.statPrice){
            CrystalsShardsCounter.instance.removeCrystalShardsValue(imageSelected.statPrice);
            BtnBuyStat.SetActive(false);
            PlayerGainsObjects.instance.allBoughtGalleryImage.Add(imageSelected);
            updateChestAvailable();
            PlayerStats.instance.increaseStats("permaStat",imageSelected.powerUp,imageSelected.powerUpValue);
            SaveSystem.saveAllDatas();
        }else{
            PopupMessage.instance.showPopup(popupNotEnoughtShards); 
        }
    }  

    private void updateChestAvailable(){
        foreach(ImageGallery img in PlayerGainsObjects.instance.allBoughtGalleryImage){
            if(img.galleryName == "artworks"){
                panelImgArtwork.transform.Find("Image ("+img.galleryPos+")"+"/chest").gameObject.SetActive(false);
            }
        }
    }
}
