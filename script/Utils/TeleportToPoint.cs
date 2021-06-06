using UnityEngine;
public class TeleportToPoint : MonoBehaviour{    
    private Vector3 destinationPoint;
    
    private void Start() {
        if(GetComponent<Locker>()){
            if(!GetComponent<Locker>().isUnlocked()){
                gameObject.SetActive(false);
            }
        }    
        destinationPoint = transform.Find("destinationPoint").gameObject.transform.position;  
    }
    
    private void OnTriggerEnter2D(Collider2D other) {        
        if(other.CompareTag("Player")){ 
            TeleportScript.instance.teleportOnaSpot(destinationPoint);    
        }
    }

}