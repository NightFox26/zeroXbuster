using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;
    public static DontDestroyOnLoadScene instance;

    private void Awake() { 
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de dontdestroyonloadscene");
            return;
        }
        instance = this;

        foreach(GameObject objet in objects){            
            DontDestroyOnLoad(objet);
        }
    }
    public void destroyAll(){
        foreach(GameObject objet in objects){
            Destroy(objet);
        }
    }
}
