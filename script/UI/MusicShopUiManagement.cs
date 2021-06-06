using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicShopUiManagement : MonoBehaviour
{
    [HideInInspector]
    public bool isMenuOpen = false;
    public Sprite spriteUnknowCd;
    public GameObject firstBtnSelected;
    public GameObject firstBtnOrginal;
    public GameObject firstBtnExtra;
    public GameObject panelMusicOriginal;
    public GameObject panelMusicExtra;
    public bool isFocusMenu = true;
    public Text totalCristals;
    private void Update() {
        totalCristals.text = PlayerStats.instance.totalShards +"";
        if(Input.GetButtonDown("Fire2")){
            if(isFocusMenu){
                closeMenu();
            }else{
                setPointerCursor(firstBtnSelected);
                isFocusMenu = true;
            }
        }
    }

    public void showPanelOriginal(){
        closeAllPanels();
        panelMusicOriginal.SetActive(true);
        isFocusMenu = false;
        setListCd("normal");
        setPointerCursor(firstBtnOrginal);
    }

    public void showPanelExtra(){
        closeAllPanels();
        panelMusicExtra.SetActive(true);
        isFocusMenu = false;
        setListCd("extra");
        setPointerCursor(firstBtnExtra);
    }

    private void closeAllPanels(){        
        panelMusicOriginal.SetActive(false);
        panelMusicExtra.SetActive(false);
    }

    private void setListCd(string categorie){
        turnOffAllCd(categorie);
        int i =0;
        GameObject panelSelected;
        if(categorie == "extra"){
            panelSelected = panelMusicExtra;
        }else{
            panelSelected = panelMusicOriginal;
        }

        foreach (GameObject music in PlayerGainsObjects.instance.allLootedBGMusics)
        {         
            Transform cdBoxPanel;  
            if(music.GetComponent<BGMusic>().extraCategorie && categorie == "extra"){                
                cdBoxPanel = panelSelected.transform.GetChild(i);                
                if(PlayerGainsObjects.instance.allBoughtBGMusics.Contains(music)){
                    setInfosBoughtMusic(cdBoxPanel, music);
                }else{
                    setInfosMusic(cdBoxPanel, music);
                }
                i++;
            }else if(!music.GetComponent<BGMusic>().extraCategorie && categorie == "normal"){                 
                cdBoxPanel = panelSelected.transform.GetChild(i);               
                if(PlayerGainsObjects.instance.allBoughtBGMusics.Contains(music)){
                    setInfosBoughtMusic(cdBoxPanel, music);
                }else{
                    setInfosMusic(cdBoxPanel, music);
                }
                i++;
            }
        }

    }
    private void turnOffAllCd(string cat){
        Transform[] allCdPlace;
        if(cat == "extra"){
            allCdPlace = panelMusicExtra.transform.GetComponentsInChildren<Transform>();
        }else{
            allCdPlace = panelMusicOriginal.transform.GetComponentsInChildren<Transform>();
        }

        foreach (Transform child in allCdPlace){
            if (child.name == "CD"){                
                setNoInfosMusic(child);
            }
        }
    }

    private void setNoInfosMusic(Transform cdBoxPanel){        
        cdBoxPanel.gameObject.transform.Find("ImageCd").gameObject.GetComponent<Image>().sprite = spriteUnknowCd;
        cdBoxPanel.gameObject.transform.Find("costCristaux").gameObject.GetComponent<Text>().text = "???";        
        Destroy(cdBoxPanel.gameObject.GetComponent<BGMusic>());
    }

    private void setInfosMusic(Transform cdBoxPanel,GameObject music){
        cdBoxPanel.gameObject.transform.Find("ImageCd").gameObject.GetComponent<Image>().sprite = music.GetComponent<BGMusic>().image;       
        cdBoxPanel.gameObject.transform.Find("costCristaux").gameObject.GetComponent<Text>().text = music.GetComponent<BGMusic>().price+"";        
        cdBoxPanel.gameObject.AddComponent<BGMusic>().hydrate(music);          
    }

    private void setInfosBoughtMusic(Transform cdBoxPanel,GameObject music){        
        cdBoxPanel.gameObject.transform.Find("ImageCd").gameObject.GetComponent<Image>().sprite = music.GetComponent<BGMusic>().image;       
        cdBoxPanel.gameObject.transform.Find("costCristaux").gameObject.GetComponent<Text>().text = "Done";        
        cdBoxPanel.gameObject.transform.Find("costCristaux").gameObject.GetComponent<Text>().color = Color.green;    
        music.GetComponent<BGMusic>().hasBeenBuy = true;    
        cdBoxPanel.gameObject.AddComponent<BGMusic>().hydrate(music);
    }


    public void buyCd(GameObject cdBoxPanel){
        BGMusic music = cdBoxPanel.GetComponent<BGMusic>();
        if(music == null){
            print("no cd here");
            return;
        }
        if(PlayerStats.instance.totalShards >= music.price && music.hasBeenBuy == false){            
            cdBoxPanel.gameObject.transform.Find("costCristaux").gameObject.GetComponent<Text>().text = "Done";            
            CrystalsShardsCounter.instance.removeCrystalShardsValue(music.price);
            PlayerGainsObjects.instance.buyBgm(Resources.Load("PREFABS/itemsBGM/"+music.prefabName, typeof(GameObject)) as GameObject);
            SaveSystem.saveAllDatas();
        }else if(music.hasBeenBuy == true){
            print("musique deja a moi !!! "+music.title);
        }else{
            print("trop cher cd "+music.title);
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
