using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class destroyAllStuff : MonoBehaviour
{
    private void Start() {        
        if(DontDestroyOnLoadScene.instance != null){            
            DontDestroyOnLoadScene.instance.destroyAll();            
        }
    }
}
