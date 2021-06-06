using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameoverManager : MonoBehaviour
{
    public GameObject gameoverScreen;
    public GameObject retryBtn;
    public GameObject QgBtn;
    public GameObject panelFaillureSystem;


    public string stageNameLostCrystal;
    public int qtLostCrystal;
    public Vector3 lostCrystalPos;

    [HideInInspector]
    public Dictionary<string, object> armorToDestroy;

    public static GameoverManager instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y'a plusieurs instance de gameover manager");
            return;
        }
        instance = this; 
        armorToDestroy = new Dictionary<string, object>();       
    }

    public void onPLayerDeath(){
        NeuroHackBar.instance.stopHackBar();
        StartCoroutine(displayScreen());
        if(SceneManager.GetActiveScene().name == "introStage"){
            QgBtn.GetComponent<Button>().interactable = false;
            panelFaillureSystem.SetActive(false);
        }else{
            panelFaillureSystem.SetActive(true);
            panelFaillureSystem.transform.Find("faillureValue").GetComponent<Text>().text = PlayerStats.instance.faillureSystemCurrentValue+"/"+PlayerStats.instance.faillureSystemMaxValue;                      
            panelFaillureSystem.transform.Find("shardsValue").GetComponent<Text>().text = qtLostCrystal.ToString(); 
        }

        retryBtn.GetComponent<Button>().interactable = true;
        breakArmor();
        if(PlayerStats.instance.faillureSystemCurrentValue <= 0){
            retryBtn.GetComponent<Button>().interactable = false;
        }
    }

    private void breakArmor()
    {
        int isBreacking = Random.Range(0,100);

        if(isBreacking >= PlayerDropRate.breakArmorRate && armorToDestroy.Count>0){
            panelFaillureSystem.transform.Find("noArmorDestroyed").gameObject.SetActive(false);
            panelFaillureSystem.transform.Find("IconObj").gameObject.SetActive(true);
            fillArmorToDestroyIconUi(armorToDestroy);
            PlayerEquipments.instance.removeEquipement(armorToDestroy);
            PlayerGainsObjects.instance.deleteEquipement(armorToDestroy);
        }else{
            panelFaillureSystem.transform.Find("noArmorDestroyed").gameObject.SetActive(true);
            panelFaillureSystem.transform.Find("IconObj").gameObject.SetActive(false);
        }
    }

    private void fillArmorToDestroyIconUi(Dictionary<string, object> item){
        string path = "";
        if( (EquipementType.List)item["equipementType"] == EquipementType.List.satelite1){
             path = "satelite1";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.satelite2){
             path = "satelite2";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.helmet){
             path = "helmet";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.body){
             path = "body";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.gun){
             path = "gun";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.arm){
             path = "arm";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.sword){
             path = "sword";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.leg){
             path = "leg";
        }else if((EquipementType.List)item["equipementType"] == EquipementType.List.booster){
             path = "booster";
        }

        GameObject itemPrefab = (GameObject) Resources.Load("PREFABS/itemsEquipements/"+path+"/"+item["itemName"]);
        GameObject uiItemsBox = panelFaillureSystem.transform.Find("IconObj").gameObject;
        uiItemsBox.transform.Find("rarity").GetComponent<Image>().color = Utility.findItemColor(item);
        uiItemsBox.transform.Find("icon").GetComponent<Image>().sprite = itemPrefab.transform.Find("itemIcon").GetComponent<SpriteRenderer>().sprite;
    }

    public void retryStage(){
        Time.timeScale = 1;
        TeleportScript.instance.teleportPlayer(StageParameters.instance.stageName, false, false);
        gameoverScreen.SetActive(false);       
        PlayerStats.instance.fullRestore();
    }

    public void resturnQG(){
        Time.timeScale = 1;
        TeleportScript.instance.teleportPlayer("QG", false, false);
        gameoverScreen.SetActive(false);       
        PlayerStats.instance.fullRestore();
    }

    public void returnMainMenu(){  
        Time.timeScale = 1;      
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame(){
        Application.Quit();
    }

    private void setPointerCursor(GameObject btn){
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btn);
    }

    public void resetLostCrystals(){
        stageNameLostCrystal = "";
        qtLostCrystal = 0;
    }

    IEnumerator displayScreen(){
        yield return new WaitForSeconds(1.5f);
        gameoverScreen.SetActive(true);

        if(PlayerStats.instance.faillureSystemCurrentValue > 0){  
            setPointerCursor(retryBtn); 
        }else{
            setPointerCursor(QgBtn); 
        }
        Time.timeScale = 0;    
    }
}
