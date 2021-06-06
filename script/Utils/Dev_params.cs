using UnityEngine;

public class Dev_params : MonoBehaviour
{
    public bool enableLoading = false;
    public bool enableTutos = true;
    public static Dev_params instance;
    
    void Awake()
    {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de Dev_params");
            return;
        }
        instance = this;
    }
}
