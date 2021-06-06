using System.Collections;
using UnityEngine;

public class IntroStage : MonoBehaviour
{
    public AudioSource request_control;
    public AudioSource bgm_stage;
    public GameObject goodPlatform;
    public GameObject fallingPlatform;
    private bool isPlatformFalling = false;
    public DialogueTrigger dialogNephtis;
    public BoxCollider2D triggerDecelratePlatforme;
    public BoxCollider2D triggerAcceleratePlatforme;
    public GameObject sparkLeft;
    public GameObject sparkRight;
    public GameObject uiControls;
    public GameObject uiAccelerateEffect;
    public GameObject darkSpawner;

    private void Start() {
        uiControls.SetActive(false);
        StageParameters.instance.stageName = "introStage";
        
        if(Dev_params.instance.enableTutos){
            StartCoroutine(startTutorial());
        }else{
            bgm_stage.Play();
        }
    }

    private void Update() {
        if(uiControls.activeSelf && Input.GetButtonDown("Fire2")){
            uiControls.SetActive(false); 
            PlayerMove.instance.moveEnable();
            bgm_stage.Play();
        }

        if(fallingPlatform != null){
            if(dialogNephtis.isDialogueFinished && !isPlatformFalling){
                isPlatformFalling = true;
                goodPlatform.SetActive(false);
                darkSpawner.SetActive(true);
                makeFallingPlatform();
            }

            if(triggerDecelratePlatforme.IsTouching(fallingPlatform.GetComponent<BoxCollider2D>()) && isPlatformFalling){
                fallingPlatform.GetComponent<Rigidbody2D>().gravityScale = 0.05f;
                uiAccelerateEffect.SetActive(true);
            }

            if(triggerAcceleratePlatforme.IsTouching(fallingPlatform.GetComponent<BoxCollider2D>()) && isPlatformFalling){
                fallingPlatform.GetComponent<Rigidbody2D>().gravityScale = 8;
                uiAccelerateEffect.SetActive(false);
                setSparkingPlatform(false);
                Destroy(fallingPlatform,1);
            }
        }
    }

    private void makeFallingPlatform(){
        setSparkingPlatform(true);
        fallingPlatform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void setSparkingPlatform(bool b){
        sparkLeft.SetActive(b);
        sparkRight.SetActive(b);
    }
   

    IEnumerator startTutorial(){  
        yield return new WaitForSeconds(2.0f);
        request_control.Play();
        yield return new WaitForSeconds(5.0f);
        uiControls.SetActive(true); 
        PlayerMove.instance.moveDisable();
    }
}
