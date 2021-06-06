
public class PnjCrafting : PnjQG
{
    private void Update() { 
        if(menu.GetComponent<CraftingUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){  
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;
            PlayerMove.instance.moveDisable();  
            PlayerActions.instance.actionsDisable();           
            menu.GetComponent<CraftingUiManagement>().isMenuOpen = true;
            menu.GetComponent<CraftingUiManagement>().showCraftingPanel();            
            menu.SetActive(true);            
        }
    }
    
    
}
