using UnityEngine;
using System.Collections;

public class BallForEnergyReload : MonoBehaviour
{
    private Transform energyBar;
    public float smoothTime = 5;
    public float delayToStopGravity = 0.2f;
    private bool startSmoothDamp = false;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;

    void Start()
    {
        energyBar = GameObject.FindGameObjectWithTag("energyBarPos").transform; 
        rb = GetComponent<Rigidbody2D>();
        float randX = Random.Range(-10,10); 
        rb.AddForce(new Vector2(randX,14),ForceMode2D.Impulse);   
        StartCoroutine(waitForDisableGravity());   
    }

    private void Update() {
        if(startSmoothDamp){            
            transform.position = Vector2.SmoothDamp(transform.position, Camera.main.ScreenToWorldPoint(energyBar.position), ref velocity, smoothTime);

            if(transform.position.x <= Camera.main.ScreenToWorldPoint(energyBar.position).x + 0.3f &&
            transform.position.x >= Camera.main.ScreenToWorldPoint(energyBar.position).x - 0.3f &&
            transform.position.y <= Camera.main.ScreenToWorldPoint(energyBar.position).y + 0.3f && 
            transform.position.y >= Camera.main.ScreenToWorldPoint(energyBar.position).y - 0.3f
            ){            
                PlayerStats.instance.reloadBullets(PlayerActions.instance.energieCostQuickTp); 
                Destroy(gameObject);
            }
        }
    }

    IEnumerator waitForDisableGravity(){
        yield return new WaitForSeconds(delayToStopGravity);
        rb.bodyType = RigidbodyType2D.Static;
        startSmoothDamp = true;
    }
        
    
}
