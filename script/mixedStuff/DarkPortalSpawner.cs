using UnityEngine;

public class DarkPortalSpawner : MonoBehaviour
{
    public GameObject darkPortalPref;
    private Vector2 spawnPos;

    private void Start() {
        BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
        
        float minXzone = transform.position.x + box.offset.x -(box.size.x/2);
        float maxXzone = transform.position.x + box.offset.x +(box.size.x/2);
        float minYzone = transform.position.y + box.offset.y -(box.size.y/2)+1;
        float maxYzone = transform.position.y + box.offset.y +(box.size.y/2);

        spawnPos = new Vector2(Random.Range(minXzone,maxXzone),Random.Range(minYzone,maxYzone));
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            int rand = Random.Range(0,100);
            int randValue = PlayerDropRate.darkPortalEasy;
            
            if(StageParameters.instance.stageDifficulty == "normal"){
                randValue = PlayerDropRate.darkPortalNormal;
            }else if(StageParameters.instance.stageDifficulty == "hard"){
                randValue = PlayerDropRate.darkPortalHard;
            }

            if(rand>randValue){
                Instantiate(darkPortalPref,spawnPos,Quaternion.identity);
                CameraFollow.instance.goTrackingPortal(spawnPos);
                NeuroHackBar.instance.startHacking();
            }
            Destroy(gameObject);
        }
    }

}
