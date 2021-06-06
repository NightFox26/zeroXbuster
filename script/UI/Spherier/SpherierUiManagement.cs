using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpherierUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    public GameObject firstBtnSelected;

    public GameObject panelSpherierAtk;
    public GameObject panelSpherierDext;
    public GameObject panelSpherierSurv;
    public Text nbHunterPts;

    public static SpherierUiManagement instance;
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de SpherierUiManagement");
            return;
        }
        instance = this;
    }

    private void Update() {
        nbHunterPts.text = PlayerStats.instance.nbHunterPts +"";
        if(Input.GetButtonDown("Fire2")){
            closeMenu();
        }
    }

    public void showPanelAtk(){
        closeAllPanels();
        panelSpherierAtk.SetActive(true);
        panelSpherierAtk.GetComponent<SpherierPanel>().loadAllUpgradedSphere("atk");
        setPointerCursor(firstBtnSelected);
    }

    public void showPanelDext(){
        closeAllPanels();
        panelSpherierDext.SetActive(true);
        panelSpherierDext.GetComponent<SpherierPanel>().loadAllUpgradedSphere("dext");
    }

    public void showPanelSurv(){
        closeAllPanels();
        panelSpherierSurv.SetActive(true);
        panelSpherierSurv.GetComponent<SpherierPanel>().loadAllUpgradedSphere("surv");
    }

    private void closeAllPanels(){        
        panelSpherierAtk.SetActive(false);
        panelSpherierDext.SetActive(false);
        panelSpherierSurv.SetActive(false);
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
