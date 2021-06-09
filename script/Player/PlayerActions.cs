using UnityEngine;
using System.Collections;
public class PlayerActions : MonoBehaviour
{
    public Animator animator;

    [HideInInspector]
    public GameObject ballForZchainPref;

    [HideInInspector]
    public GameObject ballForEnergyPref;

    [HideInInspector]
    public int nbOfAttackingClick = 0;
    private float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;
    public float impulseForceQuickTp = 8;
    public float energieCostQuickTp = 5;

    
    [HideInInspector]
    public bool isShooting;
    [HideInInspector]
    public bool isShootingAllowed;
    [HideInInspector]
    public bool isAttacking;

    [HideInInspector]
    public bool isQuickTp;
    [HideInInspector]
    public bool canQuickTpAgain = true;

    [HideInInspector]
    public bool isThirdHit = false;

    public GameObject quickTpEffectPrefab;
    public GameObject bulletPrefab;
    public GameObject chargedBulletPrefab;
    public Transform bulletPos;
    private float lastTimeShoot = 0;   

    public bool isHoldingShootBtn;
    private float timeHoldingShotBtn;

    [HideInInspector]
    public bool chargingShot;
    [HideInInspector]
    public bool chargedShot;
    [HideInInspector]
    public bool isJumpSwordRolling;
    [HideInInspector]
    public bool isDragonPunchSword;
    [HideInInspector]
    public bool isEarthQuake;


    public bool isBtnJumpPressed;
    public bool isBtnRollingPressed;
    
    [HideInInspector]
    public bool actionsEnabled = true;
    public static PlayerActions instance;  

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("Il y a deja une instance de playerActions");
            return;
        }
        instance = this;  
        isShootingAllowed = true;      
    }

    private void Start() {        
        ballForZchainPref = (GameObject)Resources.Load("PREFABS/lights/ballForZchain"); 
        ballForEnergyPref = (GameObject)Resources.Load("PREFABS/lights/ballForEnergyReload");    
    }
   
    void Update()
    {
        if(!actionsEnabled)
        return;


        //gestion attack et combo
        if(Time.time - lastClickedTime > maxComboDelay){
            nbOfAttackingClick = 0;
        }

        if(Input.GetButtonDown("Fire1")){                        
            lastClickedTime = Time.time;
            isAttacking = true;
            nbOfAttackingClick++;
            nbOfAttackingClick = Mathf.Clamp(nbOfAttackingClick,0,3);
            //gestion saut rolling sword (saut roulade + sword)
            if(PlayerMove.instance.isRolling && PlayerNewMovements.instance.obtain_jumpRollingSword){
                isJumpSwordRolling = true;
            }
            if(PlayerMove.instance.isDashing && PlayerNewMovements.instance.obtain_dragonPunchSword){
                isDragonPunchSword = true;
            }            
        }

        //gestion du earthquake (roulate + tir)
        if(PlayerMove.instance.isRolling && PlayerNewMovements.instance.obtain_earthquake && !isEarthQuake){
            if(Input.GetButtonDown("Fire3") && !PlayerMove.instance.isJumping && !PlayerMove.instance.isFalling){
                lastTimeShoot = Time.time;            
                isEarthQuake = true;            
            }
        }

        //gestion du tir chargé 
        if(Input.GetButton("Fire3") && isShootingAllowed && !chargedShot){
            isHoldingShootBtn = true;
            timeHoldingShotBtn += Time.deltaTime;
            if(PlayerNewMovements.instance.obtain_chargedShot && isHoldingShootBtn && !chargingShot && timeHoldingShotBtn > 1){
                timeHoldingShotBtn = 0;               
                bulletPos.Find("chargingShootLigthning").gameObject.SetActive(true);
                StartCoroutine(doChargingShot());               
            } 
        }

        if(Input.GetButtonUp("Fire3")){
            isHoldingShootBtn = false;
            timeHoldingShotBtn = 0;
        }

        //gestion du tir
        if(Input.GetButtonDown("Fire3") && isShootingAllowed){
            if(Time.time-lastTimeShoot > (10-PlayerStats.instance.fireRate)/10){     
                isShooting = true;                   
                if(PlayerStats.instance.currentBulletsQt>0 && !chargedShot){ 
                    shootBullet(bulletPrefab);
                }else if(PlayerStats.instance.currentBulletsQt>0 && chargedShot){                                   
                    shootBullet(chargedBulletPrefab);  
                }else{
                    useEnergieTanker();
                }
                chargedShot = false;   
            }
        }  

        //gestion du QuickTp
        if(!PlayerMove.instance.isGrounded && !PlayerMove.instance.isTouchingWall){
            if(isBtnRollingPressed && isBtnJumpPressed && !isQuickTp && canQuickTpAgain){
                doQuickTp();                
            }
            if(Input.GetButtonDown("Jump")){
                isBtnJumpPressed = true;
                Invoke("unPressedBtnJump",0.1f);
            }
            if(Input.GetButtonDown("Fire2")){
                isBtnRollingPressed = true;
                Invoke("unPressedBtnRolling",0.1f);
            }
        }else{
            isQuickTp = false;
        }      

        //ouverture du pause menu
        if(Input.GetButtonDown("Menu")){
            PauseMenuManager.instance.showMenu();
        }
    }

    private void doQuickTp(){
        if(PlayerStats.instance.currentBulletsQt>=energieCostQuickTp){
            AudioManager.Instance.Play(PlayerSounds.instance.diveSound);
            PlayerStats.instance.looseBullets(energieCostQuickTp);
            Transform playerInitPos = transform;
            isQuickTp = true;
            PlayerMove.instance.stopGravity();
            PlayerMove.instance.passingThroughtEnemy();
            canQuickTpAgain = false;
            StartCoroutine(stopQuickTp(playerInitPos));
        }
    }
    
    IEnumerator stopQuickTp(Transform initPos){
        float yDir = PlayerMove.instance.verticalMovement;
        float xDir = Mathf.Clamp(PlayerMove.instance.horizontalMovement,0.01f,1f);
        float directionDivide = yDir / xDir;
        float angle = Mathf.Rad2Deg*Mathf.Atan(directionDivide);
        if(xDir < 0){
            angle = 180 + angle;
        }       
        
        Instantiate(quickTpEffectPrefab,transform.position,Quaternion.Euler(0,0, angle));        
        
        yield return new WaitForSeconds(0.33f);
        PlayerMove.instance.reGravity();
        PlayerMove.instance.stopPassingThroughtEnemy();
        isQuickTp = false;
    }    

    private void unPressedBtnJump(){
        isBtnJumpPressed = false;
    }

    private void unPressedBtnRolling(){
        isBtnRollingPressed = false;
    }

    private void shootBullet(GameObject bulletPref)
    {        
        lastTimeShoot = Time.time;
        float bulletCostEnergy = bulletPref.GetComponent<Bullet>().energyCost;
        if(PlayerStats.instance.currentBulletsQt>=bulletCostEnergy){
            GameObject bullet = null;
            if(PlayerMove.instance.facingRight){
                bullet = (GameObject)Instantiate(bulletPref,new Vector3(bulletPos.position.x, bulletPos.position.y, 0),Quaternion.Euler(0,0,180));           
            }else{
                bullet = (GameObject)Instantiate(bulletPref,new Vector3(bulletPos.position.x, bulletPos.position.y, 0),Quaternion.Euler(0,0,0));
                bullet.GetComponent<Bullet>().speed = -bullet.GetComponent<Bullet>().speed;
            }

            bullet.GetComponent<Bullet>().isPlayerBullet = true;
            PlayerStats.instance.looseBullets(bulletCostEnergy);
        }                 
    }

    public void stopShootingAnim(){
        isShooting = false;
    }

    private IEnumerator doChargingShot(){
        chargingShot = true;
        yield return new WaitForSeconds(1f);
        chargingShot = false;
        chargedShot = true;
    }

    public void combo1(){
        if(nbOfAttackingClick<2){  
            isAttacking = false;
            nbOfAttackingClick = 0;                
        }
    }

    public void combo2(){
        if(nbOfAttackingClick < 3){            
            isAttacking = false;
            nbOfAttackingClick = 0;                
        }
    }

    public void stopCombo(){
        isThirdHit = false;
        isAttacking = false;
        nbOfAttackingClick = 0;
    }      

    public void stopDragonPunch(){
        stopCombo();
        isDragonPunchSword = false;
    }

    public void stopEarthQuake(){
        isEarthQuake = false;
    }    
 
    private void useEnergieTanker(){
        GameObject energieTanker = ItemsListing.instance.getTankers()[0].gameObject;
        if(PlayerGainsObjects.instance.allBlackMarketComponents.Contains(energieTanker)){
            Utility.PlayGfxAnimation("itemUsing/energieTankerAnimation",transform.position);
            PlayerGainsObjects.instance.allBlackMarketComponents.Remove(energieTanker);
            PlayerStats.instance.fullReloadBullets();
        }
    }  

    public void actionsDisable(){
        actionsEnabled = false;
    }

    public void actionsEnable(){
        actionsEnabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(isQuickTp){
            if(other.CompareTag("enemy")){
                other.GetComponent<Enemy>().takeDamage(PlayerStats.instance.damage, true, false);
                if(other.GetComponent<Enemy>().currentHealth <= 0){
                    PlayerMove.instance.isJumpAllowed = true;
                    canQuickTpAgain = true;
                    Instantiate(ballForEnergyPref,new Vector3(other.gameObject.transform.position.x,other.gameObject.transform.position.y,0),Quaternion.identity); 
                }
            }
        }
    }

}
