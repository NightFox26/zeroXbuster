using UnityEngine;

public class GoldenMob : MonoBehaviour
{
    public GameObject[] loots;
    public float lifeTime;
    private Animator animator;
    private Rigidbody2D rb;
    public float velocity = 5;   

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifeTime);
        animator.Play("running");
    }

    private void Update() {
        if(PlayerMove.instance.transform.position.x > transform.position.x){            
            transform.localScale = new Vector3(0.5f,0.5f,1);
            velocity = -Mathf.Abs(velocity);
        }else{            
            transform.localScale = new Vector3(-0.5f,0.5f,1);
            velocity = Mathf.Abs(velocity);
        }        
    }

    private void FixedUpdate() { 
        rb.velocity = new Vector2(velocity,rb.velocity.y);
    }
  
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("sword") || other.CompareTag("bullet")){
            int i = Random.Range(0,loots.Length);
            Instantiate(loots[i],transform.position + new Vector3(0,1,0),Quaternion.identity);
        }
    }
}
