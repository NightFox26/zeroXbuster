using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUi;
    public Animator menu_transition;
    public bool isMenuOpen = false;
    public static PauseMenuManager instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y'a plusieurs instance de PauseMenuManager");
            return;
        }
        instance = this;
    }
    
    void Update()
    {               
        if(Input.GetButtonDown("Menu") && pauseMenuUi.gameObject.activeSelf){   
            quitMenu();                      
        }
    }

    public void showMenu(){
        if(!isMenuOpen && SceneManager.GetActiveScene().name != "introStage"){
            menu_transition.SetTrigger("transition");            
            StartCoroutine(delayDisplayMenu());
        }
    }

    public void quitMenu(){
        if(isMenuOpen){                      
            menu_transition.SetTrigger("transition");
            StartCoroutine(delayCloseMenu()); 
        }
    }

    IEnumerator delayDisplayMenu(){
        PlayerMove.instance.moveDisable();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
        pauseMenuUi.SetActive(true); 
        PauseNavigation.instance.setPointerCursor(PauseNavigation.instance.firstBtnSelected);
        PauseNavigation.instance.isInNavidation = true;    
        isMenuOpen = true;
        yield return new WaitForSeconds(0.8f);
    }

    IEnumerator delayCloseMenu(){
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        PauseNavigation.instance.closeAllPanel();  
        pauseMenuUi.SetActive(false);        
        isMenuOpen = false;
        PlayerMove.instance.moveEnable();
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }
}
