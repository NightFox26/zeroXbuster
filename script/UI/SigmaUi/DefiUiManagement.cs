using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefiUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    private bool isInNav = true;
    public GameObject firstBtnSelected;
    public GameObject panelAchievements;
    public GameObject panelDefi;
    public GameObject panelRenforcement;

    public static DefiUiManagement instance;
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de SpherierUiManagement");
            return;
        }
        instance = this;
    }

    private void Update() {
        if(Input.GetButtonDown("Fire2") && isInNav){
            closeMenu();
        }else if(Input.GetButtonDown("Fire2") && !isInNav){
            isInNav = true;
            setPointerCursor(firstBtnSelected);
        }
    }

    public void showMainPanel(){
        isInNav = false;
        closeAllPanels();
        panelAchievements.SetActive(true);
        panelAchievements.GetComponent<AchievmentsManagement>().showListUi();
        setPointerCursor(firstBtnSelected);
    }

    public void showDefiPanel(Button btn){
        isInNav = false;
        closeAllPanels();
        panelDefi.SetActive(true);
        setPointerCursor(btn.gameObject);
    }

    public void showRenforcementPanel(Button btn){
        isInNav = false;
        closeAllPanels();
        panelRenforcement.SetActive(true);
        setPointerCursor(btn.gameObject);
    }

    private void closeAllPanels(){
        panelAchievements.SetActive(false);
        panelDefi.SetActive(false);
        panelRenforcement.SetActive(false);
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
