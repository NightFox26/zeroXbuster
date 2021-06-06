using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageBullet = 10f;
    public float vieBullet = 0;
    public float energyCost = 5;
    public float speed = 10f;
    public float lifeTimeBullet = 8;
    public AudioSource audioBullet;
    public GameObject explosionAnimation;   
    public bool destoyOnTagGround = true;
    public bool isHorizontalShooting = true;
    public bool isHomming = false;
    public bool isRealHomming = false;
    public bool isPerforating = false;
    public bool isPoisoning = false;
    public bool isParticulesBullets = false;
    public bool isFriendlyFire = false;

    [HideInInspector]
    public bool isPlayerBullet;
    private Transform playerTransform;
    private Rigidbody2D rb;    
    private Vector2 shootingDirection;
    private Animator animator;

    private void Start(){         
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        animator = GetComponent<Animator>();

        if(audioBullet){
            audioBullet.Play();
        }
    }

    protected void FixedUpdate() {
         if(isParticulesBullets){
            return;
        }

        if(isHomming){
            if(!isPlayerBullet){                
                float step =  Mathf.Abs(speed) * Time.deltaTime; 
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, step);

                if (Vector3.Distance(transform.position, playerTransform.position) < 0.001f){                   
                    destroyBullet(true);             
                }
            }
        }else if(isRealHomming){
            if(!isPlayerBullet){     
                Vector2 direction = (Vector2)playerTransform.position - rb.position;  
                direction.Normalize(); 
                float rotateAmount = Vector3.Cross(direction, transform.up).z;   
                rb.angularVelocity = rotateAmount * 200;  
                rb.velocity = -transform.up * speed;
            }            
        }else{
            if(isHorizontalShooting){
                rb.velocity = new Vector2(speed,0);
            }else{
                shootInDirection();
            }
        }

        lifeTimeBullet -= Time.deltaTime;
        if(lifeTimeBullet < 0){
            destroyBullet(true);
        }
    }

    private void shootInDirection(){
        if(shootingDirection.x == 0 && shootingDirection.y == 0){
            rb.velocity = new Vector2(speed,0);
        }else{
            rb.velocity = shootingDirection;
        }
    }

    public void setShootingDirection(float velX,float velY){
        shootingDirection = new Vector2(velX,velY);
    }

    void OnTriggerEnter2D(Collider2D other){        
        managementOfBulletContact(other.gameObject);
    }

    private void OnParticleCollision(GameObject other) {
        print("collide2");
        managementOfBulletContact(other);
    }

    private void bulletTakeDamage(float dmg){
        vieBullet -= dmg;
        if(vieBullet <= 0){
            destroyBullet(true);
        }
    }

    private void managementOfBulletContact(GameObject other){
        if(other.CompareTag("Player") && (!isPlayerBullet || isFriendlyFire)){ 
            if(explosionAnimation == null){   
                PlayerHealth.instance.takeDamage(damageBullet);
            }        
            destroyBullet();
        }

        if(destoyOnTagGround &&
        other.gameObject.layer == LayerMask.NameToLayer("ground")){            
            destroyBullet(true);
        }

        if(other.CompareTag("bullet") && vieBullet > 0){
            bulletTakeDamage(other.GetComponent<Bullet>().damageBullet);
        }

        if(other.CompareTag("enemy")){   
            if(!other.gameObject.GetComponent<Enemy>().isDecoring){
                destroyBullet();
            }         
        }
    }

    private void destroyBullet(bool forcedDestroy = false){        
        if(!isPerforating || forcedDestroy){
            if(explosionAnimation != null){
                explodeBullet(transform.position);
                Destroy(gameObject);
            }else if(hasImpactParameter("impact")){
                animator.SetTrigger("impact");                
                Destroy(gameObject,0.7f);                
            }else{
                Destroy(gameObject);   
            }
        }
    }

    protected void explodeBullet(Vector3 pos){
        if(explosionAnimation != null){
            GameObject explode = Instantiate(explosionAnimation,pos,Quaternion.identity);
            explode.GetComponent<BulletExplosion>().damageExplosion = damageBullet;
        }
    }

    private bool hasImpactParameter(string paramName){
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName)
                return true;
        }
        return false;
    }


}
