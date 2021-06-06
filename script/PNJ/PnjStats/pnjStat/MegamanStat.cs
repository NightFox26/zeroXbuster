using UnityEngine;

public class MegamanStat : PnjStat
{    public static MegamanStat instance;

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
