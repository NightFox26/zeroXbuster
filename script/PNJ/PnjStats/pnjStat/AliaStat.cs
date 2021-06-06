using UnityEngine;

[System.Serializable]
public class AliaStat : PnjStat
{
    public static AliaStat instance;    

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
