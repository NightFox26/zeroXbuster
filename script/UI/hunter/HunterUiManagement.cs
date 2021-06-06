using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HunterUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    
    public GameObject firstBtnSelected;
    public GameObject panelEnemies;
    public GameObject panelReploids;
    public GameObject panelMavericks;
    private void Update() {        
        if(Input.GetButtonDown("Fire2")){
            closeMenu();
        }
    }

    public void showPanelEnmies(){
        closeAllPanels();
        panelEnemies.SetActive(true);
        setPointerCursor(firstBtnSelected);
    }

    public void showPanelReploids(){
        closeAllPanels();
        panelReploids.SetActive(true);
    }

    public void showPanelMavericks(){
        closeAllPanels();
        panelMavericks.SetActive(true);
    }

    private void closeAllPanels(){        
        panelEnemies.SetActive(false);
        panelReploids.SetActive(false);
        panelMavericks.SetActive(false);
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    public void closeMenu(){
        if(isMenuOpen){            
            isMenuOpen = false;
            gameObject.SetActive(false);
            PlayerMove.instance.moveEnable();
            PlayerActions.instance.actionsEnable();
            SaveSystem.saveAllDatas();
        }
    }

}
