using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public GameObject firstBtnSelected;
    public Button loadBtn;
    public static bool needToLoad = false;
       
    private void Start(){        
        StartCoroutine(setPointerOnBtn());
        if(!File.Exists(SaveSystem.pathSaveLocation))
            loadBtn.GetComponent<Button>().interactable = false;
    }

    public void startGame(){        
        SceneManager.LoadScene("opening_intro");
    }
    public void loadGame(){  
        needToLoad = true;      
        SceneManager.LoadScene("preLoadGameComponents");
    }
    public void quitGame(){
        Application.Quit();
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    IEnumerator setPointerOnBtn(){
        yield return new WaitForSeconds(1);
        setPointerCursor(firstBtnSelected);
    }
}
