using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public new string name = "";
    public int exp = 1;
    public EnemieType.List enemyType;
    public bool isDecoring = false;
    public float maxHealth = 10;

    [HideInInspector]
    public float currentHealth;
    public float damage = 5;
    public bool randomIdle = true;
    protected Rigidbody2D rb;

    [HideInInspector]
    public Animator animator;

    private EnemyHealthBar enemyHealthBar;
    
    public GameObject loot1;
    public float loot1DropRate = 10f;
    public int loot1MaxNb = 5;
    public GameObject loot2;
    public float loot2DropRate = 5f;
    public int loot2MaxNb = 1;
    public GameObject loot3;
    public float loot3DropRate = 1f;
    public int loot3MaxNb = 1;

    public float scalingDyingFactorX = 1.1f;
    public float scalingDyingFactorY = -3.2f;

    private bool isDied = false;
    private Color colorRender;
   

    protected void Start() {
        if(enemyType == EnemieType.List.ennemy && exp >0){
            StageParameters.instance.nbMobSpawn ++;
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyHealthBar = GetComponent<EnemyHealthBar>();
        currentHealth = maxHealth;

        if(randomIdle && animator.GetCurrentAnimatorStateInfo(0).IsName("idle")){
            animator.Play("idle", 0, Random.Range(0.0f, 1.0f));
        } 
        colorRender = GetComponent<SpriteRenderer>().color;       
    }

    void Update() {
        if(isDied){            
           transform.localScale = new Vector3(transform.localScale.x+transform.localScale.x*scalingDyingFactorX*Time.deltaTime, transform.localScale.y+transform.localScale.y*scalingDyingFactorY * Time.deltaTime, transform.localScale.z+transform.localScale.z*scalingDyingFactorX * Time.deltaTime);
        }
    }

    public void takeDamage(float dmg, bool isDamageFromPlayer,bool isForcedField){   
        bool isCriticalDie = false;
        if(PlayerActions.instance.isThirdHit)  
            dmg *= 1.5f;

        if(isCriticalDmg() && isDamageFromPlayer && !isForcedField){
            float criticaldmg = dmg + dmg * (PlayerStats.instance.criticalDmg/100);            
            playAnimOnEnemy("/Text/criticalText",transform.position,false);
            isCriticalDie = true;
            print("CRITICAL : " + dmg + " => "+criticaldmg);
        }                     
        currentHealth -= dmg;   
        enemyHealthBar.jaugeUpdate(currentHealth);           
        StartCoroutine(hurtingAnimation());

        if(currentHealth <=0 && !isDied){                          
            die(isCriticalDie);
        }
    }

    private bool isCriticalDmg(){
        int randCritc = Random.Range(0,101);
        //print("critic rate rand = "+randCritc);        
        if(randCritc<=PlayerStats.instance.criticalFreguency)
            return true;
        return false;
    }

    public void OnTriggerEnter2D(Collider2D col) {  
        if(isDecoring == false && !isDied){
            if(col.CompareTag("Player")){ 
                PlayerHealth.instance.takeDamage(damage);
                PlayerCombo.instance.comboCancel();
            } 

            if(col.CompareTag("sword")){  
                Instantiate(playerSword.instance.hitAnimation,transform.position,Quaternion.identity);  
                takeDamage(PlayerStats.instance.damage, true, false);
            }else if(col.CompareTag("bullet")){ 
                bool isDamageFromPlayer = col.GetComponent<Bullet>().isPlayerBullet;
                if(isDamageFromPlayer || col.GetComponent<Bullet>().isFriendlyFire){
                    takeDamage(col.GetComponent<Bullet>().damageBullet,isDamageFromPlayer, false);
                }             
            } 

            if(col.CompareTag("deadLine")){
                die(false);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("forceField")){
            takeDamage(other.GetComponent<ForceField>().damageField,true, true);
        }
    }
    IEnumerator hurtingAnimation(){
        int i = 3;
        while(i>0){            
            GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = colorRender;
            yield return new WaitForSeconds(0.1f);
            i--;
        }
    }

    protected virtual void die(bool isCriticalDie){  
        if(!isDied){ 
            isDied = true; 
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            foreach(Collider2D col in GetComponents<Collider2D>()){
                col.enabled = false;
            }
            currentHealth = 0;        

            if(isCriticalDie && exp > 0){
                playAnimOnEnemy("/hit/criticalDestroyHit",transform.position,true);
            }else{
                playAnimOnEnemy("/hit/hitDestroy",transform.position,true);
            }

            playAnimOnEnemy("/explosions/small_dust_explosion",transform.position,true);
            if(enemyType == EnemieType.List.darkMaverick){
                RankingPanel.instance.nbMavericksKill++;
                PlayerAchievements.instance.nbKillDarkBoss++;
            }else if(enemyType == EnemieType.List.maverick){
                RankingPanel.instance.nbMavericksKill++;
                PlayerAchievements.instance.nbKillBoss++;        
            }else if(enemyType == EnemieType.List.reploid){
                RankingPanel.instance.nbReploidsKill++;
                PlayerAchievements.instance.nbKillReploids++;
            }else if(enemyType == EnemieType.List.ennemy){
                if(exp>0){
                    RankingPanel.instance.nbEnemiesKill++;
                    PlayerAchievements.instance.nbKillEnemies++;
                }
            }
            
            playerEnemyKillCounter.instance.addKillEnemyCounter(gameObject);        

            float killingBonus = checkBountyKill();
            PlayerStats.instance.gainExp(exp*killingBonus);
            Destroy(gameObject,0.4f);        
            
            if(exp>0){
                loot(killingBonus);
                NeuroHackBar.instance.delayHackBar();
                Instantiate(PlayerActions.instance.ballForZchainPref,new Vector3(transform.position.x,transform.position.y+2,0),Quaternion.identity); 
            }

            if(enemyType == EnemieType.List.maverick){
                int randExit = Random.Range(0,100);
                if(randExit >= PlayerDropRate.darkPortalJenova && StageParameters.instance.stageDifficulty == "hard"){
                    LevelConfig.instance.makeSpawnJenovaTp();
                }else{
                    LevelConfig.instance.makeSpawnExitTp();
                }
            }
        }           
    }

    private void playAnimOnEnemy(string path, Vector3 pos,bool facingOrientation){
        Quaternion rot = Quaternion.identity;
        if(!PlayerMove.instance.facingRight && facingOrientation) {
            rot = Quaternion.Euler(0,180,0);
        }
        Instantiate(Resources.Load("Prefabs/GFX"+path),pos,rot);
    }

    private float checkBountyKill(){
        if(PlayerHealth.instance.getStealthMode() == true){
            return PlayerStats.instance.bountyBonus;
        }
        return 1;
    }

    private void loot(float bountyBonus){
        float rand = Random.Range(0f,100f);        
        rand -= (PlayerStats.instance.luck * bountyBonus);        
        
        if(rand <= loot1DropRate && loot1 != null){            
            int nb = Random.Range(1,loot1MaxNb);            
            for(int i =0; i<nb;i++){
                Instantiate(loot1, new Vector3(transform.position.x, transform.position.y+2f, transform.position.z), Quaternion.identity);
            }
        }

        if(rand <= loot2DropRate && loot2 != null){
            int nb = Random.Range(1,loot2MaxNb);            
            for(int i =0; i<nb;i++){
                Instantiate(loot2, new Vector3(transform.position.x, transform.position.y+2f, transform.position.z), Quaternion.identity);
            }
        }

        if(rand <= loot3DropRate && loot3 != null){
            int nb = Random.Range(1,loot3MaxNb);            
            for(int i =0; i<nb;i++){
                Instantiate(loot3, new Vector3(transform.position.x, transform.position.y+2f, transform.position.z), Quaternion.identity);
            }
        }
    }
    
}
