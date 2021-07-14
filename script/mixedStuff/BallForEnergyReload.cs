using UnityEngine;
using System.Collections;

public class BallForEnergyReload : MonoBehaviour
{
    private Transform energyBar;
    public float smoothTime = 0.4f;
    public float delayToStopGravity = 0.2f;
    private bool startSmoothDamp = false;
    private Rigidbody2D rb;

    void Start()
    {
        energyBar = GameObject.FindGameObjectWithTag("energyBarPos").transform; 
        rb = GetComponent<Rigidbody2D>();
        float randX = Random.Range(-10,10); 
        rb.AddForce(new Vector2(randX,14),ForceMode2D.Impulse);   
        StartCoroutine(waitForDisableGravity());   
    }

    private void FixedUpdate(){
        if(startSmoothDamp){ 
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(energyBar.position), smoothTime);

            if(transform.position.x <= Camera.main.ScreenToWorldPoint(energyBar.position).x + 0.1f &&
            transform.position.x >= Camera.main.ScreenToWorldPoint(energyBar.position).x - 0.1f &&
            transform.position.y <= Camera.main.ScreenToWorldPoint(energyBar.position).y + 0.1f && 
            transform.position.y >= Camera.main.ScreenToWorldPoint(energyBar.position).y - 0.1f
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
