public class PnjMusicShop : PnjQG
{
    private void Update() {                
        if(menu.GetComponent<MusicShopUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){ 
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;            
            PlayerMove.instance.moveDisable();  
            PlayerActions.instance.actionsDisable();            
            menu.GetComponent<MusicShopUiManagement>().isMenuOpen = true;
            menu.GetComponent<MusicShopUiManagement>().showPanelOriginal();            
            menu.SetActive(true);            
        }
    }    
    
}
