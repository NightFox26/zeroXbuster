using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CraftingUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    public GameObject firstBtnMenu;
    public GameObject firstBtnCrafting;
    public GameObject panelCrafting;
    public GameObject panelDestroyArmor;  
    public GameObject steelGearCounter;
    public GameObject goldGearCounter;
    public GameObject tiberiumGearCounter;
    public GameObject droneBlasterCounter;
    public GameObject droneEyesCounter;
    public GameObject droneSpineCounter;
    private bool isInPanel = false;
    public static CraftingUiManagement instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de CraftingUiManagement");
            return;
        }
        instance = this;
    }
    
    private void Update() {
        if(Input.GetButtonDown("Fire2") && isInPanel == false){
            closeMenu();
        }

        if(Input.GetButtonDown("Fire2") && isInPanel){
            setPointerCursor(firstBtnMenu);
            isInPanel = false;
        }
    }

    public void showCraftingPanel(){        
        hideAllPanel();
        countAllComponentsPossessed();
        panelCrafting.SetActive(true);
        setPointerCursor(firstBtnCrafting);
        isInPanel = true;
    }

    public void showDestroyPanel(){
        hideAllPanel();
        panelDestroyArmor.SetActive(true);
        ArmorDestroyManegement.instance.refreshAllList();
        isInPanel = true;
    }

    private void hideAllPanel(){
        panelCrafting.SetActive(false);
        panelDestroyArmor.SetActive(false);
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    public void countAllComponentsPossessed(){
        List<string> tempItemsList = new List<string>();  
        setQtComponentPossessedToZero();  

        foreach (GameObject item in PlayerGainsObjects.instance.allComponents)
        {
            if(!tempItemsList.Contains(item.name)){
                tempItemsList.Add(item.gameObject.name);                
                int nbItem = PlayerGainsObjects.instance.countItem(item,PlayerGainsObjects.instance.allComponents);
                if(item.name == "Steel gear"){
                    steelGearCounter.transform.Find("qt").GetComponent<Text>().text = ""+nbItem;
                }else if(item.name == "Golden gear"){
                    goldGearCounter.transform.Find("qt").GetComponent<Text>().text = ""+nbItem;
                }else if(item.name == "Tiberium gear"){
                    tiberiumGearCounter.transform.Find("qt").GetComponent<Text>().text = ""+nbItem;
                }else if(item.name == "Drone blaster"){
                    droneBlasterCounter.transform.Find("qt").GetComponent<Text>().text = ""+nbItem;
                }else if(item.name == "Drone eyes"){
                    droneEyesCounter.transform.Find("qt").GetComponent<Text>().text = ""+nbItem;
                }else if(item.name == "Drone spine"){
                    droneSpineCounter.transform.Find("qt").GetComponent<Text>().text = ""+nbItem;
                }
            }
        }
    }

    private void setQtComponentPossessedToZero(){
        steelGearCounter.transform.Find("qt").GetComponent<Text>().text = ""+0;
        goldGearCounter.transform.Find("qt").GetComponent<Text>().text = ""+0;
        tiberiumGearCounter.transform.Find("qt").GetComponent<Text>().text = ""+0;
        droneBlasterCounter.transform.Find("qt").GetComponent<Text>().text = ""+0;
        droneEyesCounter.transform.Find("qt").GetComponent<Text>().text = ""+0;
        droneSpineCounter.transform.Find("qt").GetComponent<Text>().text = ""+0;
    }

    public void closeMenu(){
        if(isMenuOpen){            
            isMenuOpen = false;
            gameObject.SetActive(false);
            PlayerMove.instance.moveEnable();
            PlayerActions.instance.actionsEnable();
            Time.timeScale = 1;
            SaveSystem.saveAllDatas();
        }
    }

    
}
