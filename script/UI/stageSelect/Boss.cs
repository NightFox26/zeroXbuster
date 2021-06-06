using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss
{    
    public int id;
    private static string[] LIST_STAGES_POSSIBILITY = {"forest","city","cave","space","snow","airport"};
    private static string[] LIST_STAGES_BONUS = {"bonus_megaman","bonus_alia","bonus_vile","bonus_axl","bonus_iris","bonus_sigma","bonus_dynamo"};
    public string type; 
    
    public string difficulty;    

    [HideInInspector]
    public int positionOnMap;

    [HideInInspector]
    public List<Stage> arrayStageGrid;

    public Boss(int _id,string _type, string _difficulty, int _positionOnMap){
        id= _id;
        type = _type;
        difficulty = _difficulty;
        positionOnMap = _positionOnMap;        
        buildGridStages();
    }

    public void hydrate(Boss boss){
        id = boss.id;
        type = boss.type;
        difficulty = boss.difficulty;
    }

    private void buildGridStages(){
        if(difficulty == "easy"){
            buiderByDifficulty(2);
        }else if(difficulty == "normal"){
            buiderByDifficulty(4);
        }else if(difficulty == "hard"){
            buiderByDifficulty(6);
        }
    }

    private void buiderByDifficulty(int floorMax){  
        arrayStageGrid = new List<Stage>();      
        int stageId;
        Stage stage;
        for (int i = 1; i < floorMax; i++){
            int randNbRoom = Random.Range(1,4);
            for (int j = 1; j <= randNbRoom; j++){ 
                int isRandomStage = Random.Range(1,6);
                if(isRandomStage == 5){
                    stageId = Random.Range(0,LIST_STAGES_BONUS.Length);
                    stage = new Stage(LIST_STAGES_BONUS[stageId],i,j,false,difficulty,true);
                }else{
                    stageId = Random.Range(0,LIST_STAGES_POSSIBILITY.Length);
                    stage = new Stage(LIST_STAGES_POSSIBILITY[stageId],i,j,false,difficulty,false);
                }
                arrayStageGrid.Add(stage);
            } 
        }
        stageId = Random.Range(0,LIST_STAGES_POSSIBILITY.Length);
        stage = new Stage(LIST_STAGES_POSSIBILITY[stageId],floorMax,2,true,difficulty,false);
        arrayStageGrid.Add(stage);
    }




}