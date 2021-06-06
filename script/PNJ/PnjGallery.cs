
public class PnjGallery : PnjQG
{
    private void Update() {            
        if( menu.GetComponent<GalleryPanel>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){  
            dialogue.isDialogueFinished = true;
            playerOnPnj = false;         
            PlayerMove.instance.moveDisable();   
            PlayerActions.instance.actionsDisable();          
            menu.GetComponent<GalleryPanel>().isMenuOpen = true;
            menu.GetComponent<GalleryPanel>().showPanelArtworks();
            foreach(ImageGallery imageGallery in PlayerGainsObjects.instance.allLootGalleryImage){                
                if(imageGallery.galleryName == "artworks"){
                    menu.transform.Find("panelArtworks/Image ("+imageGallery.galleryPos+")").gameObject.SetActive(true);
                }else if(imageGallery.galleryName == "zero"){
                    menu.transform.Find("panelZero/Image ("+imageGallery.galleryPos+")").gameObject.SetActive(true);
                }else if(imageGallery.galleryName == "girls"){
                    menu.transform.Find("panelGirl/Image ("+imageGallery.galleryPos+")").gameObject.SetActive(true);
                }else if(imageGallery.galleryName == "hentai"){
                    menu.transform.Find("panelHentai/Image ("+imageGallery.galleryPos+")").gameObject.SetActive(true);
                }                
            }
            menu.SetActive(true);            
        }
    }    

    
}
