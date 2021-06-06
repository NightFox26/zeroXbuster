using UnityEngine;

public class DynamoStat : PnjStat
{
    public static DynamoStat instance;

    private void Awake() {
        if(instance!=null){
            Debug.LogWarning("il y a deja instance de Alia");
            return;
        }
        instance = this;
    }
}
