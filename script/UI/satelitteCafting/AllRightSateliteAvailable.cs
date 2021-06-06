using UnityEngine;

public class AllRightSateliteAvailable : MonoBehaviour
{
    public GameObject[] satelites;

    public static AllRightSateliteAvailable instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de AllRightSateliteAvailable");
            return;
        }
        instance = this;
    }
    
}
