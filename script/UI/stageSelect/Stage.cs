using UnityEngine;

[System.Serializable]
public class Stage
{    
    private string[] ITEM_BONUS_POSSIBILITY = {"heal","bullet","crystal","heal","bullet","crystal","components","booster"};
    public string name;
    public int floor;
    public int room;
    public string item_bonus = "";
    public bool hasBoss;
    public bool isFinished = false;
    public string difficulty;
    public bool isBonusStage;

    public Stage(string _name, int _floor, int _room, bool _hasBoss, string _difficulty, bool _isBonusStage){
        name = _name;
        floor = _floor;
        room = _room;
        hasBoss = _hasBoss;  
        difficulty = _difficulty; 
        isBonusStage = _isBonusStage;  
        if(!isBonusStage){            
            item_bonus = randomBonus();   
        }
    }

    private string randomBonus(){
        int randItemBonus = Random.Range(0,ITEM_BONUS_POSSIBILITY.Length);
        return ITEM_BONUS_POSSIBILITY[randItemBonus];
    }
}