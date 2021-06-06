using UnityEngine;

public class IrisStat : PnjStat
{
    public static IrisStat instance;

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
