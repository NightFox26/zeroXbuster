using UnityEngine;
using UnityEngine.EventSystems;

public class PauseNavigation : MonoBehaviour
{
    public GameObject firstBtnSelected;
    public GameObject boosterPanel;
    public GameObject equimentsPanel;
    public GameObject componentsPanel;
    public GameObject statsPanel;
    public static PauseNavigation instance;
    private float percentCostTpHq = 0.70f;

    [HideInInspector]
    public bool isInNavidation;
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de PauseNavigation");
            return;
        }
        instance = this;
    }
  
    void Start()
    {
        closeAllPanel();
        setPointerCursor(firstBtnSelected);   
        isInNavidation = true;     
    }

    private void Update() {
        if(Input.GetButtonDown("Fire2") && isInNavidation == false){
            setPointerCursor(firstBtnSelected);
            isInNavidation = true; 
        }
    }

    public void openBoosterPanel(){
        closeAllPanel();        
        boosterPanel.SetActive(true);  
        isInNavidation = false;      
        PauseBoosterPanel.instance.updateListBooster();
        PauseBoosterPanel.instance.updateListNeuroHack();
    }
    public void openEquipmentsPanel(){
        closeAllPanel();
        equimentsPanel.SetActive(true);
        PauseEquipementPanel.instance.openMenuEquipement();
        isInNavidation = false;
    }
    public void openComponentsPanel(){
        closeAllPanel();
        componentsPanel.SetActive(true);
        isInNavidation = false;
        PauseComponentsPanel.instance.updateListCompo();
    }
    public void openStatsPanel(){
        closeAllPanel();
        statsPanel.SetActive(true);
        PauseStatsPanel.instance.showStatsPlayer();
        PauseStatsPanel.instance.showStatsBoostersAndEquip();
    }

    public void returnHQ(){
        closeAllPanel();
        int amoutLost = Mathf.RoundToInt(PlayerStats.instance.totalShards * percentCostTpHq);        
        CrystalsShardsCounter.instance.removeCrystalShardsValue(amoutLost);
        isInNavidation = true;
        PauseMenuManager.instance.quitMenu();
        TeleportScript.instance.teleportPlayer("QG", false, false);
    }

    public void closeAllPanel(){
        boosterPanel.SetActive(false);
        equimentsPanel.SetActive(false);
        componentsPanel.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }
}
