using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningIntro : MonoBehaviour
{    
    [SerializeField]
    private Image imageUI = null;

    [SerializeField]
    private Text txtUI = null;

    [SerializeField]
    private StepIntro[] steps = null;

    private StepIntro stepActual;
    private bool canSkipText = false;

    private Queue<StepIntro> stepsIntro;    

    private void Start() {
        stepsIntro = new Queue<StepIntro>();
        foreach(StepIntro step in steps){            
            stepsIntro.Enqueue(step);                  
        }
        nextStep();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canSkipText){            
            nextStep();
        }

        if(Input.GetButtonDown("Menu")){
            SceneManager.LoadScene("preLoadGameComponents");
        }
    }

    void nextStep(){
        canSkipText = false;
        if(stepsIntro.Count == 0){
            imageUI.GetComponent<Animator>().SetTrigger("fonduOff");
            txtUI.text = "";
            Invoke("launchGame",1.2f);
            return;
        }

        stepActual = stepsIntro.Dequeue();
        
        if(imageUI.sprite != null && !stepActual.keepLastImage){
            imageUI.GetComponent<Animator>().SetTrigger("fonduOff");
            Invoke("displayNextPicture",1.2f);
            Invoke("displayNextText",1.2f);
        }else if(imageUI.sprite != null && stepActual.keepLastImage){
            displayNextText();
        }else{
            displayNextPicture();
            displayNextText();
        }        
    }

    void displayNextPicture(){
        if(!stepActual.image){
            imageUI.sprite = null;
            imageUI.color = Color.black;
        }else{
            imageUI.sprite = stepActual.image;
            imageUI.color = Color.white;
            imageUI.GetComponent<Animator>().SetTrigger("fonduOn");
        }
    }

    void displayNextText(){  
        StopAllCoroutines();
        StartCoroutine(typingMessage(stepActual.text));
    }

    IEnumerator typingMessage(string message){
        txtUI.text = "";              
        foreach(char letter in message.ToCharArray()){ 
           txtUI.text += letter;
           yield return new WaitForSeconds(0.05f);
        }
        canSkipText = true;
     }

    void launchGame(){
        txtUI.transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene("preLoadGameComponents");
    }
}
