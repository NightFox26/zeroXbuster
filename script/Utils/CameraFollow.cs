using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 posOffset;
    private Vector3 velocity;

    [HideInInspector]
    public float blockingCameraPosXmax;
    [HideInInspector]
    public float blockingCameraPosXmin;
    [HideInInspector]
    public float blockingCameraPosYmax;
    [HideInInspector]
    public float blockingCameraPosYmin;
    private bool isLookingForPlayerMode = true;

    [HideInInspector]
    public bool isTrackingPortal = false;
    private Vector2 newSpawnPortalPos;

    public static CameraFollow instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja un CameraFollow");
        }
        instance = this;
    }

    private void Update() {        
        if(PlayerMove.instance.facingRight == false){
            if(posOffset.x > 0){
                posOffset.x = -posOffset.x+3;
            }
        }else{
            if(posOffset.x < 0){
                posOffset.x = Mathf.Abs(posOffset.x)+3;
            }
        }

        if(isTrackingPortal){
            transform.position = Vector3.SmoothDamp(transform.position,new Vector3(newSpawnPortalPos.x,newSpawnPortalPos.y,transform.position.z),ref velocity,0.5f);
        }else{
            freeMoveCameraToPlayerPos();
        }
    }

    private void freeMoveCameraToPlayerPos(){
        if(!isLookingForPlayerMode){
            transform.position = Vector3.SmoothDamp(transform.position,new Vector3(player.transform.position.x+posOffset.x,player.transform.position.y+posOffset.y,transform.position.z),ref velocity,timeOffset);

            if(transform.position.x >= blockingCameraPosXmax){
                transform.position = new Vector3(blockingCameraPosXmax,transform.position.y,posOffset.z);
            }

            if(transform.position.x <= blockingCameraPosXmin){
                transform.position = new Vector3(blockingCameraPosXmin,transform.position.y,posOffset.z);
            }

            if(transform.position.y >= blockingCameraPosYmax){
                transform.position = new Vector3(transform.position.x,blockingCameraPosYmax,posOffset.z);
            }

            if(transform.position.y <= blockingCameraPosYmin){
                transform.position = new Vector3(transform.position.x,blockingCameraPosYmin,posOffset.z);
            }
        }
    }

    public void goFindPlayer(){
        isLookingForPlayerMode = true;
        // blockingCameraPosXmin = player.transform.position.x - 10;
        // blockingCameraPosXmax = player.transform.position.x + 10;
        // blockingCameraPosYmax = player.transform.position.y + 6;
        // blockingCameraPosYmin = player.transform.position.y - 6;  
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,transform.position.z);      
        Invoke("delayStopTrackingPLayer",0.5f);
    }

    private void delayStopTrackingPLayer(){
        isLookingForPlayerMode = false;
    }

    public void goTrackingPortal(Vector2 portalPos){
        isTrackingPortal = true;
        newSpawnPortalPos = portalPos;
        Invoke("stopTrackingPortal",1);
    }
    private void stopTrackingPortal(){
        isTrackingPortal = false;
    }
}
