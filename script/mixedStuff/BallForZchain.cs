using UnityEngine;
using System.Collections;

public class BallForZchain : MonoBehaviour
{
    private Transform Zchain;
    public float smoothTime = 5;
    public float delayToStopGravity = 0.2f;
    private bool startSmoothDamp = false;
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;

    void Start()
    {
        Zchain = GameObject.FindGameObjectWithTag("ZcounterPos").transform; 
        rb = GetComponent<Rigidbody2D>();
        float randX = Random.Range(-10,10); 
        rb.AddForce(new Vector2(randX,14),ForceMode2D.Impulse);   
        StartCoroutine(waitForDisableGravity());   
    }

    private void Update() {
        if(startSmoothDamp){            
            transform.position = Vector2.SmoothDamp(transform.position, Camera.main.ScreenToWorldPoint(Zchain.position), ref velocity, smoothTime);

            if(transform.position.x <= Camera.main.ScreenToWorldPoint(Zchain.position).x + 0.3f &&
            transform.position.x >= Camera.main.ScreenToWorldPoint(Zchain.position).x - 0.3f &&
            transform.position.y <= Camera.main.ScreenToWorldPoint(Zchain.position).y + 0.3f && 
            transform.position.y >= Camera.main.ScreenToWorldPoint(Zchain.position).y - 0.3f
            ){            
                PlayerCombo.instance.comboUp(); 
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
