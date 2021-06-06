using UnityEngine;
using UnityEngine.EventSystems;

public class ComputerUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    
    public GameObject firstBtnSelected;
    
    private void Update() {        
        if(Input.GetButtonDown("Fire2")){
            closeMenu();
        }
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
