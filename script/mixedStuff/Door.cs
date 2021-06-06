using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isStopingNeuroHack = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GetComponent<Animator>().Play("door");
            if(isStopingNeuroHack){
                NeuroHackBar.instance.stopHackBar();
            }
        }
    }
}
