using UnityEngine;

public class DrawGizmoCamera : MonoBehaviour
{
    public float width;
    public float height;

    private void OnDrawGizmos() {
        Vector3 position = gameObject.transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(position,new Vector3(width,height,1));    
    }
}
