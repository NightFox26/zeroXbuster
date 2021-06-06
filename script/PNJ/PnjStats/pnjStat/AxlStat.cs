using UnityEngine;

public class AxlStat : PnjStat
{   
    public static AxlStat instance;

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
