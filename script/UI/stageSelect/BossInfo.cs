using System.Collections.Generic;
using UnityEngine;

public class BossInfo : MonoBehaviour {
    public int id;
    public string type;     
    public string difficulty;
    public List<Stage> stagesGrid;
   
    public void hydrate(Boss boss){
        id = boss.id;
        type = boss.type;
        difficulty = boss.difficulty;
        stagesGrid = boss.arrayStageGrid;
    }
}