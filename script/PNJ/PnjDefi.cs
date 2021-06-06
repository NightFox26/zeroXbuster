public class PnjDefi : PnjQG
{
    private void Update() {                
        if(menu.GetComponent<DefiUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){  
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;           
            PlayerMove.instance.moveDisable(); 
            PlayerActions.instance.actionsDisable();            
            menu.GetComponent<DefiUiManagement>().isMenuOpen = true;
            menu.GetComponent<DefiUiManagement>().showMainPanel();            
            menu.SetActive(true);            
        }
    }
    
}