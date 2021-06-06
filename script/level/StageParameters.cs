using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class StageParameters : MonoBehaviour
{
    [HideInInspector]
    public string itemBonusName;

    [HideInInspector]
    public string stageName;

    [HideInInspector]
    public string stageDifficulty;
    
    [HideInInspector]    
    public bool isBossLevel;

    [HideInInspector]
    public Tilemap tilemap;

    [HideInInspector]
    public List<string> glyphObtained;

    [HideInInspector]
    public int nbMobSpawn = 0;

    [HideInInspector]
    public bool playerHasTakeDamage = false;

    public GameObject readyAnimation;

    public static StageParameters instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de StageParameters");
            return;
        }
        instance = this;             
    }

    public void resetStageParameters(){        
        nbMobSpawn = 0;
        playerHasTakeDamage = false;

        if(SceneManager.GetActiveScene().name != "QG" && SceneManager.GetActiveScene().name != "introStage"){            
            RankingPanel.instance.resetRanking(); 
            generateGlyph();            
        }

        if(LevelConfig.instance.isLevelNeuroHack){
            NeuroHackBar.instance.initHackBar();
        }else{
            NeuroHackBar.instance.stopHackBar();
        }
    }

    private void generateGlyph(){
        glyphObtained = new List<string>();

        try
        {
            tilemap = GameObject.Find("Grid/Glyphs-Spots").GetComponent<Tilemap>();
        }
        catch (System.Exception)
        {
            tilemap = null;
        }

        if(tilemap){ 
            List<Vector3> posList = new List<Vector3>();
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                if (!tilemap.HasTile(position)) {
                    continue;
                }
                Vector3 tilePosition = tilemap.CellToWorld(position);     
                posList.Add(tilePosition+new Vector3(0.5f,0.5f,0));          
                
            }

            for (int i = 0; i < System.Enum.GetNames(typeof(GlyphType.List)).Length; i++)
            {
                if(posList.Count > 0){
                    int randPos = Random.Range(0,posList.Count);
                    GameObject glyph = (GameObject)Resources.Load("PREFABS/decorsElements/Glyphs/glyph"+System.Enum.GetNames(typeof(GlyphType.List)).GetValue(i));
                    Instantiate(glyph,posList[randPos],Quaternion.identity);
                    posList.RemoveAt(randPos);
                }
            }
        }
    } 
    public void showReadyAnimation(){
        if(SceneManager.GetActiveScene().name != "QG"){
            readyAnimation.SetActive(true);
        }
    } 

}
