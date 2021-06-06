public class PnjBlaster : PnjQG
{
    private void Update() {                
        if(menu.GetComponent<BlasterUpgradeUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){  
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;           
            PlayerMove.instance.moveDisable(); 
            PlayerActions.instance.actionsDisable();            
            menu.GetComponent<BlasterUpgradeUiManagement>().isMenuOpen = true;
            menu.GetComponent<BlasterUpgradeUiManagement>().showMainPanel();            
            menu.SetActive(true);            
        }
    }
    
}