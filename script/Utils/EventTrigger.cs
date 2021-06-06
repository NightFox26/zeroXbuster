using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject eventTriggered;
    public bool isLootingShip = false;  
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && isLootingShip){  
            Vector3 playerPosition = other.gameObject.transform.position;

            if(eventTriggered.GetComponent<SpaceShipLootBox>().randLootShip){
                GameObject newShip =  Instantiate(eventTriggered,new Vector3(playerPosition.x + 7, playerPosition.y+5,playerPosition.z),Quaternion.identity);
                newShip.GetComponent<SpaceShipLootBox>().eventStarted = true;
            }else{
                eventTriggered.GetComponent<SpaceShipLootBox>().eventStarted = true;
            }
            Destroy(gameObject);
        }
    }
}
