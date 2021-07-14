using UnityEngine;

[System.Serializable]
public class DialogueTrigger : MonoBehaviour
{        
    public Sentence[] sentences;
    public bool needInputToActivate = false;
    public Transform forcePlayerPosition;
    public bool forcePlayerFacingRight;
    public bool isBlocking = false;
    public bool multiRead = false;
    public bool showRankingBeforeTp = false;
    public string teleportTo = null;
    private bool hasBeenReaded = false;

    private bool onEventTrigger = false;
    [HideInInspector]
    public bool isDialogueFinished = false;
    public GameObject activateObjOnFinish;
    public AudioSource unPausingAudio;

    private void Update() {  
        if(isDialogueFinished == false && PlayerActions.instance.actionsEnabled){
            if(needInputToActivate && onEventTrigger && Input.GetButtonDown("Fire1") && (multiRead == true || hasBeenReaded == false )){
                isDialogueFinished = false;                
                launchDialogue();
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){  
            isDialogueFinished = false;
            onEventTrigger = true; 
            if(needInputToActivate == false) launchDialogue();        
        }
    }

    public void launchDialogue(){
         if(multiRead == true || hasBeenReaded == false){                              
            hasBeenReaded = true;

            if(forcePlayerPosition){
                GameObject player = PlayerMove.instance.gameObject;
                player.transform.position = forcePlayerPosition.position;
            }

            FindObjectOfType<DialogueManager>().StartDialogue(sentences,isBlocking,teleportTo,showRankingBeforeTp,this,forcePlayerFacingRight);            
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        onEventTrigger = false;
    }

    public void unPausingAudioSource(){
        unPausingAudio.UnPause();
    }



}
