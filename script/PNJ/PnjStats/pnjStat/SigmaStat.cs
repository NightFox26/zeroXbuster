using UnityEngine;

public class SigmaStat : PnjStat
{    public static SigmaStat instance;

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
