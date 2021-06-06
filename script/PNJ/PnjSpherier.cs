
public class PnjSpherier : PnjQG
{
    private void Update() {                
        if(menu.GetComponent<SpherierUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){ 
            dialogue.isDialogueFinished = true;
            playerOnPnj = false;            
            PlayerMove.instance.moveDisable();  
            PlayerActions.instance.actionsDisable();            
            menu.GetComponent<SpherierUiManagement>().isMenuOpen = true;
            menu.GetComponent<SpherierUiManagement>().showPanelAtk();            
            menu.SetActive(true);            
        }
    }
    
    
}
