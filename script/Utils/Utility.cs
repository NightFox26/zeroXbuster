using UnityEngine;
using System;
using System.Collections.Generic;
public class Utility : MonoBehaviour{   
    
    public static void DumpToConsole(object obj)
    {
        var output = JsonUtility.ToJson(obj, true);
        Debug.Log(output);
    }

    public static void PlayGfxAnimation(string path, Vector3 pos){
        GameObject anim = (GameObject)Resources.Load("PREFABS/GFX/"+path);
        Instantiate(anim,pos,Quaternion.identity);
    }

    public static int getTimeOfTheDAY(){
        DateTime now = DateTime.Now;
        return now.Hour;
    }

    public static DateTime getDateOfTheDay(){
        return DateTime.Now;
    }

    public static Color findItemColor(Dictionary<string,object> item){ 
        Rarity.List rarity = (Rarity.List)item["rarity"];        
        if(rarity == Rarity.List.green){
            return ItemColor.green();
        }else if(rarity == Rarity.List.blue){
            return ItemColor.blue();
        }else if(rarity == Rarity.List.purple){
            return ItemColor.purple();
        }else{
            return ItemColor.gray();
        }
    }
}