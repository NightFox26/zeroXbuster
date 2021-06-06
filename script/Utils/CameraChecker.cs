using UnityEngine;

public class CameraChecker : MonoBehaviour
{
    public bool isRightCheck;
    public bool isLeftCheck;
    public bool isTopCheck;
    public bool isBottomCheck;

    private float widthCamera;
    private float heightCamera;
    private CameraFollow cam;

    private void Start() {
        heightCamera = 2f*Camera.main.orthographicSize;
        widthCamera = heightCamera*Camera.main.aspect;
        cam = transform.parent.GetComponent<CameraFollow>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PrisonForCamera") ){
            float boxPrisonWidth = other.GetComponent<BoxCollider2D>().size.x;
            float boxPrisonHeight = other.GetComponent<BoxCollider2D>().size.y;
            float boxPrisonOffsetX = other.GetComponent<BoxCollider2D>().offset.x;
            float boxPrisonOffsetY = other.GetComponent<BoxCollider2D>().offset.y;            
            
            if(isRightCheck){
                cam.blockingCameraPosXmax = other.transform.position.x + boxPrisonOffsetX + boxPrisonWidth/2 -widthCamera/2;
            }

            if(isLeftCheck){
                cam.blockingCameraPosXmin = other.transform.position.x + boxPrisonOffsetX - boxPrisonWidth/2 + widthCamera/2;
            }

            if(isTopCheck){
                cam.blockingCameraPosYmax = other.transform.position.y + boxPrisonOffsetY + boxPrisonHeight/2 - heightCamera/2;
            }

            if(isBottomCheck){
                cam.blockingCameraPosYmin = other.transform.position.y + boxPrisonOffsetY - boxPrisonHeight/2 + heightCamera/2;
            }
        }
    }
}
