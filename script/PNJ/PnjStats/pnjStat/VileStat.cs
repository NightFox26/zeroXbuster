using UnityEngine;

public class VileStat : PnjStat
{
    public static VileStat instance;

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
