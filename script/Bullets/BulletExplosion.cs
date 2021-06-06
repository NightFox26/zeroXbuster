using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float damageExplosion = 0;
    public Vector2 souffleExplosion = new Vector2(-500,50);
    private bool exploded = false;
    public float radiusExplosion;
    public LayerMask[] layersToExplode;
    
    void FixedUpdate() { 
        if(exploded){   
            foreach (LayerMask layerMask in layersToExplode)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusExplosion,layerMask); 
                foreach (Collider2D hit in colliders) {             
                    if (hit.GetComponent<Rigidbody2D>()) { 
                        if(hit.transform.position.x>transform.position.x){
                            souffleExplosion.x = Mathf.Abs(souffleExplosion.x);
                        }
                        hit.GetComponent<Rigidbody2D>().AddForce(souffleExplosion); 
                    } 
                }                 
            }      
        } 
    } 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            exploded = true;
            PlayerHealth.instance.takeDamage(damageExplosion);
        }
    }

    public void destroyAnimation(){
        Destroy(gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radiusExplosion);
    }
}
