using UnityEngine;

public class PnjSelectStage : MonoBehaviour
{
    private GameObject menu;   

    [HideInInspector]
    public bool playerOnPnj = false;

    private DialogueTrigger dialogue = null;

    private void Awake() {
        menu = RunCompletion.instance.menuStageSlect;
    }

    private void Start() {
        dialogue = GetComponent<DialogueTrigger>();
    }

    private void Update() {                
        if(menu.GetComponent<SelectStageUiManagement>().isMenuOpen == false && playerOnPnj && dialogue.isDialogueFinished){   
            dialogue.isDialogueFinished = false;
            playerOnPnj = false;          
            PlayerMove.instance.moveDisable(); 
            menu.GetComponent<SelectStageUiManagement>().isMenuOpen = true;
            menu.GetComponent<SelectStageUiManagement>().showPanelBoss();            
            menu.SetActive(true);            
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerOnPnj = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerOnPnj = false;
        }
    }

    
}
