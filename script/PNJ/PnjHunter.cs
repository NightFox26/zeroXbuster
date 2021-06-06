public class PnjHunter : PnjQG
{
    private void Update() {                
        if(menu.GetComponent<HunterUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){  
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;           
            PlayerMove.instance.moveDisable(); 
            PlayerActions.instance.actionsDisable();            
            menu.GetComponent<HunterUiManagement>().isMenuOpen = true;
            menu.GetComponent<HunterUiManagement>().showPanelEnmies();            
            menu.SetActive(true);            
        }
    }
    
}
