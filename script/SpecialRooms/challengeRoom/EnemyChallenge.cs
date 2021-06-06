using UnityEngine;

public class EnemyChallenge : MonoBehaviour
{
    public float lifeTimeEnemy = 4;
    public Bullet bullet;
    public ParticleSystem bulletParticles;
    public float atKSpeed = 1.7f;
    private GameObject player;    
    private float timePassedForShooting = 0;

    private Object particuleExplode;
    private bool isDead = false;

    [HideInInspector]
    public ChallengeRoom challengeConfig;
    private Animator animator;
    private GameObject warningAtk;
    
    void Start()
    { 
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        Destroy(gameObject, lifeTimeEnemy);
        particuleExplode = Resources.Load("PREFABS/ParticulesExplosion");
        warningAtk = transform.Find("warningAtk").gameObject;
    }

    void Update()
    {
        if(challengeConfig.challengeWin){
            enemyDie(false); 
        }

        if(transform.position.x < player.transform.position.x){
            faceRight();
        }else{
            faceLeft();
        }
        
        timePassedForShooting += Time.deltaTime;
        if(timePassedForShooting>=atKSpeed){
            timePassedForShooting = 0;
            attack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(isDead)
            return;

        if(other.CompareTag("Player") && !PlayerHealth.instance.isInvincible){
            PlayerHealth.instance.takeDamage(0); 
            challengeConfig.lifePlayer--;
            ChallengeUI.instance.updateUiLife(challengeConfig.lifePlayer);           
        }

        if(other.CompareTag("sword")){
            enemyDie(true); 
        }else if(other.CompareTag("bullet")){ 
            bool isDamageFromPlayer = other.GetComponent<Bullet>().isPlayerBullet;
            if(isDamageFromPlayer){
                enemyDie(true);
            }             
        } 
    }
    private void faceRight(){
        if(bulletParticles){
            flipParticulesSystem(false);
        }
        transform.localScale = new Vector3(-1,1,1);
    }
    private void faceLeft(){
        if(bulletParticles){
           flipParticulesSystem(true);
        }
        transform.localScale = new Vector3(1,1,1);
    }

    private void flipParticulesSystem(bool _flip){
        var sh = bulletParticles.GetComponent<ParticleSystem>().shape;
        ParticleSystemRenderer render = bulletParticles.GetComponent<ParticleSystemRenderer>();
        if(_flip){
            sh.scale = new Vector3(-1f, 1f, 1f);
            render.flip = new Vector3(1,0,0);
        }else{
            sh.scale = new Vector3(1f, 1f, 1f);
            render.flip = new Vector3(0,0,0);
        }
    }

    private void enemyDie(bool playerKill){
        isDead = true;
        if(playerKill){
            challengeConfig.killEnemies++;
            ChallengeUI.instance.updateUiEnemyKill(challengeConfig.killEnemies);
        }
        GameObject explode = (GameObject)Instantiate(particuleExplode, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }

    private void attack(){
        warningAtk.GetComponent<Animator>().SetTrigger("warningAtk"); 
        if(hasImpactParameter("atk")){
            animator.SetTrigger("atk");
        }

        if(bullet != null){
            Transform bulletSpawnPos = transform.Find("spawnBulletPoint");
            GameObject b;
            if(bulletSpawnPos){
                b = Instantiate(bullet,bulletSpawnPos.position,Quaternion.identity).gameObject;
            }else{
                b = Instantiate(bullet,transform.position,Quaternion.identity).gameObject;
            }
            b.GetComponent<BulletChallenge>().challengeConfig = challengeConfig;
        }else if(bulletParticles != null){
            GameObject b = Instantiate(bulletParticles,transform.position,Quaternion.identity).gameObject;
            b.GetComponent<BulletChallenge>().challengeConfig = challengeConfig;
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
