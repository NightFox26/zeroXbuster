public class PnjSaber : PnjQG
{
    private void Update() {                
        if(menu.GetComponent<SaberUpgradeUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){  
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;           
            PlayerMove.instance.moveDisable(); 
            PlayerActions.instance.actionsDisable();            
            menu.GetComponent<SaberUpgradeUiManagement>().isMenuOpen = true;
            menu.GetComponent<SaberUpgradeUiManagement>().showMainPanel();            
            menu.SetActive(true);            
        }
    }
    
}