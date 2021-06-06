using UnityEngine;

public class PnjQG : MonoBehaviour
{
    public GameObject menu;  

    [HideInInspector]
    public bool playerOnPnj = false;
    protected DialogueTrigger dialogue = null;
    void Start()
    {
        gameObject.SetActive(false);
        if(!checkIfLocked()){
            gameObject.SetActive(true);
            dialogue = GetComponent<DialogueTrigger>();
        }
    }

    protected bool checkIfLocked(){
        if(GetComponent<Locker>()){
            if(GetComponent<Locker>().isUnlocked()) return false;

            return true;
        }
        return false;
    }
    
    protected void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerOnPnj = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerOnPnj = false;
        }
    }
}
