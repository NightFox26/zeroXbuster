using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject HUD;
    private Animator playerAnimator;
    private GameObject player;
    public GameObject dialPanelHaut;
    public GameObject dialPanelBas;
    private Animator dialPanelHautAnimator;
    private Animator dialPanelBasAnimator;
    private Image dialPanelHautFace;
    private Image dialPanelBasFace;
    private Text dialPanelHautName;
    private Text dialPanelBasName;
    private Text dialPanelHautText;
    private Text dialPanelBasText;
    private Queue<Sentence> sentences;
    private bool messageNeedToComplete = false;
    private bool messageAccelaratorAvailable = false;
    private bool messageAccelerate = false;

    private bool dialogueManagerActif = false;

    public GameObject teleportManager;
    private string teleportTo;
    private bool showRanking = false;
    private bool isBlockingMessage = false;

    private bool dialogueFinished = false;

    private DialogueTrigger dialTrigger;
   
     private void Start() {
         sentences = new Queue<Sentence>();
         player = GameObject.FindGameObjectWithTag("Player");
         playerAnimator = player.GetComponent<Animator>();

         dialPanelHautAnimator = dialPanelHaut.GetComponent<Animator>();
         dialPanelBasAnimator = dialPanelBas.GetComponent<Animator>();
        
         dialPanelHautFace = dialPanelHaut.transform.Find("face").GetComponent<Image>();
         dialPanelBasFace = dialPanelBas.transform.Find("face").GetComponent<Image>();

         dialPanelHautName = dialPanelHaut.transform.Find("Name").GetComponent<Text>();
         dialPanelBasName = dialPanelBas.transform.Find("Name").GetComponent<Text>();

         dialPanelHautText = dialPanelHaut.transform.Find("Text").GetComponent<Text>();
         dialPanelBasText = dialPanelBas.transform.Find("Text").GetComponent<Text>();
     }

     private void Update() {        
        if(Input.GetButtonDown("Fire1") && messageNeedToComplete == false && dialogueManagerActif){           
           messageAccelaratorAvailable = false; 
           messageAccelerate = false;
           DisplayNextSentence();          
        }

        // pour accelerer l'ecriture de la phrase
        if(Input.GetButtonDown("Fire1") && messageAccelaratorAvailable && messageNeedToComplete){                 
            messageAccelerate = true;
         }
     }

     IEnumerator autoSkipSentence(){
        yield return new WaitForSeconds(5);
        messageAccelaratorAvailable = false; 
        messageAccelerate = false;
        DisplayNextSentence();
     }

     public void StartDialogue(Sentence[] allSentences, bool isBlocking, string tpTo, bool showRankingBeforeTp, DialogueTrigger trigger){ 
         dialTrigger = trigger; 
         dialogueFinished = false;
         dialogueManagerActif = true; 
         teleportTo = tpTo;
         isBlockingMessage = isBlocking;
         showRanking = showRankingBeforeTp;

         if(isBlockingMessage){
            PlayerMove.instance.animator.Play("playerIdle");
            PlayerMove.instance.moveDisable();
            HUD.SetActive(false);   
         }   

         sentences.Clear();
         foreach( Sentence sentence in allSentences){            
               sentences.Enqueue(sentence);                  
         }
         DisplayNextSentence();              
     }

     private void DisplayNextSentence(){
        if(sentences.Count == 0){
           StopDialogue();
           return;
        }
        Sentence sentence = sentences.Dequeue();

        if(sentence.stopingBgm != null){
           sentence.stopingBgm.Stop();
        }

        if(sentence.playAudio != null){
           sentence.playAudio.Play();
        }

        if(sentence.setActiveGameObject != null){
           sentence.setActiveGameObject.SetActive(true);
        }

        if(sentence.setInactiveGameObject != null){
           sentence.setInactiveGameObject.SetActive(false);
        }
       
        if(sentence.postionTop == true){            
            dialPanelHaut.SetActive(true);            
            dialPanelHautAnimator.SetBool("isShowing",true);
            hydrateMessagePanel(dialPanelHautFace, dialPanelHautName, dialPanelHautText, sentence);           
         }else if(sentence.postionTop == false){    
            dialPanelBas.SetActive(true);   
            dialPanelBasAnimator.SetBool("isShowing",true);            
            hydrateMessagePanel(dialPanelBasFace, dialPanelBasName, dialPanelBasText, sentence);       
         }        
     }

     private void hydrateMessagePanel(Image panelImage, Text panelName,Text panelText, Sentence sentence){         
         panelImage.sprite = sentence.face;
         panelName.text = sentence.persoName;          
         StopAllCoroutines();
         StartCoroutine(typingMessage(panelText,sentence.message));  

         if(!isBlockingMessage){              
           StartCoroutine(autoSkipSentence());
        }      
     }

     IEnumerator typingMessage(Text panelText, string message){
        panelText.text = ""; 
        messageNeedToComplete = true;
              
        foreach(char letter in message.ToCharArray()){             
           if(messageAccelerate){              
              panelText.text = message;                            
              break;
           }
           panelText.text += letter;
           yield return new WaitForSeconds(0.03f);
           messageAccelaratorAvailable = true;
        }
        messageAccelaratorAvailable = false;
        messageNeedToComplete = false;
     }

     private void StopDialogue(){  

        if(dialogueFinished == false){
            if(dialPanelHaut.activeSelf){
                  dialPanelHautAnimator.SetBool("isShowing",false); 
            } 
            if(dialPanelBas.activeSelf){
                  dialPanelBasAnimator.SetBool("isShowing",false);
            }    
            
            //playerAnimator.SetBool("isWaiting",false);    
            HUD.SetActive(true);
            dialogueFinished = true;
            dialTrigger.isDialogueFinished = true;
            PlayerMove.instance.moveEnable();
           
            if(showRanking && LevelConfig.instance.isLevelWithRanking){   
               RankingPanel.instance.teleportToAfterRanking = teleportTo;          
               RankingPanel.instance.showRankingPanel();
               return;
            }

            if(teleportTo != ""){                 
               TeleportScript.instance.teleportPlayer(teleportTo,true,true);
            }
        }        
     }
}
