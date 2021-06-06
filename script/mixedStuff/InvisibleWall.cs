using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
    }
}
