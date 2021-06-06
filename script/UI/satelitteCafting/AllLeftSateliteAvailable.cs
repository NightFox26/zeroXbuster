using UnityEngine;

public class AllLeftSateliteAvailable : MonoBehaviour
{
    public GameObject[] satelites;

    public static AllLeftSateliteAvailable instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de AllLeftSateliteAvailable");
            return;
        }
        instance = this;
    }
    
}
