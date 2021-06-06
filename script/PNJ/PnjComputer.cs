
public class PnjComputer : PnjQG
{
    private void Update() {                   
        if(menu.GetComponent<ComputerUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){   
            dialogue.isDialogueFinished = true;
            playerOnPnj = false;          
            PlayerMove.instance.moveDisable();              
            PlayerActions.instance.actionsDisable();         
            menu.GetComponent<ComputerUiManagement>().isMenuOpen = true;       
            menu.SetActive(true);            
        }
    }
    
    
}
