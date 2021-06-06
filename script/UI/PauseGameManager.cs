using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseGameManager : MonoBehaviour
{
    public GameObject pauseGameMenu;
    private bool isPaused = false; 
    private RawImage BgVideo;
    private GameObject firstBtn;

    private void Awake() {
        BgVideo = pauseGameMenu.transform.Find("Fond-video").GetComponent<RawImage>();
        firstBtn = pauseGameMenu.transform.Find("Panel/ButtonGoBackMenu").gameObject;
    }

    void Update()
    {
        if(Input.GetButtonDown("Pause") && !isPaused){            
            showPausePanel();
        }else if(Input.GetButtonDown("Pause") && isPaused){
            hidePausePanel();
        }
    }

    private void showPausePanel(){
        pauseGameMenu.SetActive(true);
        StartCoroutine(waitFondu());
        BgVideo.color= new Color(0,0.489f,1,0.1f);
        BgVideo.CrossFadeAlpha(255f, 40, true );
        isPaused = true;
        Time.timeScale = 0;
    }

    private void hidePausePanel(){
        isPaused = false;
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator waitFondu(){
        yield return new WaitForSecondsRealtime(1f);
        BgVideo.color= new Color(1,1,1,1);
        pauseGameMenu.transform.Find("Fond-video").GetComponent<StreamVideo>().runVideo();
        setPointerCursor(firstBtn);
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

}
