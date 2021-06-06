using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject firstBtnSelected;
    public TMPro.TMP_Dropdown dropDownMusic;
    public Image button_A_icon;
    private bool isShowing;
    private bool isDropdownOpened = false;
    public static LoadingScreenManager instance;

    
    void Awake()
    {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de LoadingScreenManager");
            return;
        }
        instance = this;
        dropDownMusic.ClearOptions(); 
        dropDownMusic.options.Add(new TMPro.TMP_Dropdown.OptionData() {text="Aleatoir"});        
    }
    
    public void showLoading(){  
        if(SceneManager.GetActiveScene().name == "introStage" || SceneManager.GetActiveScene().name == "QG"){
            isShowing = false;
            dropDownMusic.gameObject.SetActive(false);
            button_A_icon.GetComponent<Image>().enabled = false;
            return;
        }  
        setAllAvailableMusic();
        isShowing = true;
        setPointerCursor(firstBtnSelected);
    }

    public void stopLoading(){                
        isShowing = false;
        gameObject.transform.Find("Panel").gameObject.SetActive(false);
        if(SceneManager.GetActiveScene().name != "introStage" && SceneManager.GetActiveScene().name != "QG"){
            MusicSelector.instance.StartBgm();
        }
    }
    
    void Update()
    {            
        if(Input.GetButtonDown("Fire1") && isShowing){
            if(isDropdownOpened == false){                
                isDropdownOpened = true;
                Time.timeScale = 0;               
            }else{                
                isDropdownOpened = false;
                Time.timeScale = 1;
            }
        }

        if(Input.GetButtonDown("Fire2") && isShowing){            
            isDropdownOpened = false;
            Time.timeScale = 1;
        }

        if(dropDownMusic.options[dropDownMusic.value].text == "Aleatoir"){
            PlayerPrefs.SetString("BGMselected","Aleatoir");
        }
    }

    public void musicChanged(){
        print(dropDownMusic.options[dropDownMusic.value].text);
        PlayerPrefs.SetString("BGMselected",dropDownMusic.options[dropDownMusic.value].text);
    }

    public void setAllAvailableMusic(){
        button_A_icon.GetComponent<Image>().enabled = enabled;
        dropDownMusic.gameObject.SetActive(true);
        dropDownMusic.ClearOptions();
        dropDownMusic.options.Add(new TMPro.TMP_Dropdown.OptionData() {text="Aleatoir"});
        foreach (GameObject music in PlayerGainsObjects.instance.allBoughtBGMusics)
        {
            setMusicsInDropDown(music);
        }
    }

    public void setMusicsInDropDown(GameObject music){        
        dropDownMusic.options.Add(new TMPro.TMP_Dropdown.OptionData() {text=music.GetComponent<BGMusic>().title});        
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }
}
