using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
  public float currentHealth;
  public HealthBar healthBar;
  public GameObject stealthIcon;

  [HideInInspector]
  public bool isInvincible = false;

  [HideInInspector]
  public bool isHurted;

  [HideInInspector]
  public bool isDying;
  public float invinsibilityTime = 0.7f;
  private bool stealthMode = true;
  public AudioSource playerDeadSound; 
  public AudioSource stealthModeActivated; 
  public GameObject hurtAnimation;
  public static PlayerHealth instance;

  private void Awake() {
      if(instance != null){
          Debug.LogWarning("instance de player health detecté !!!");
          return;
      }
      instance = this;
  }

 private void Start() {     
     currentHealth = PlayerStats.instance.maxHealth;
     healthBar.setMaxHealth(currentHealth);
     healthBar.setHealth(currentHealth);
 } 

 public void healing(int value){
     currentHealth += value;
     isDying = false;      
     if(currentHealth >= PlayerStats.instance.maxHealth){
         currentHealth = PlayerStats.instance.maxHealth;
         isStealth(true);
     }
     healthBar.setHealth(currentHealth);
 }

 public void fullHealing(){
     isDying = false;
     isStealth();
     currentHealth = PlayerStats.instance.maxHealth;
     healthBar.setMaxHealth(currentHealth);     
     healthBar.setHealth(currentHealth);     
 }

    private bool useLifeTanker(){
        GameObject lifeTanker = ItemsListing.instance.getTankers()[1].gameObject;
        if(PlayerGainsObjects.instance.allBlackMarketComponents.Contains(lifeTanker)){
            Utility.PlayGfxAnimation("itemUsing/lifeTankerAnimation",transform.position);
            PlayerGainsObjects.instance.allBlackMarketComponents.Remove(lifeTanker);
            fullHealing();
            return true;
        }
        return false;
    }

  public void takeDamage(float damage = 0) { 
      if(isPlayerCanTakeDamge() && currentHealth > 0){
        //print("prend "+damage+" damage");
        StageParameters.instance.playerHasTakeDamage = true;
        isHurted = true;
        isInvincible = true;
        isNotStealth();
        GameObject hurtAnim =  Instantiate(hurtAnimation,transform.position,Quaternion.identity);
        hurtAnim.transform.parent = transform;

        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if(currentHealth <= 0 ){  
            if(useLifeTanker())return;
            PlayerMove.instance.rb2d.bodyType = RigidbodyType2D.Static; 
            isDying = true;
        } 
        RankingPanel.instance.totalDamageTaken += damage;
        StartCoroutine(isInvincibleAnimation());
      }  
  }

  public bool isPlayerCanTakeDamge(){
      if(!isInvincible && !PlayerMove.instance.isRolling){
          return true;
      }
      return false;
  }

  public void stopHurtedAnimation(){
      isHurted = false;
  }

  public void isNotStealth(){
      stealthModeActivated.Stop();
      stealthMode = false;
      stealthIcon.SetActive(false);
  }

  public void isStealth(bool playAudio = false){
      if(playAudio && SceneManager.GetActiveScene().name != "preLoadGameComponents" && !stealthMode){
        stealthModeActivated.Play();
      }
      stealthMode = true;
      stealthIcon.SetActive(true);
  }

  public bool getStealthMode(){
      return stealthMode;
  }


  private void OnTriggerEnter2D(Collider2D col) {
      if(col.CompareTag("deadLine")){
          currentHealth = 0;
          healthBar.setHealth(currentHealth);
          PlayerMove.instance.rb2d.bodyType = RigidbodyType2D.Static;
          isDying = true;
      }
   }

   IEnumerator isInvincibleAnimation(){  
        yield return new WaitForSeconds(invinsibilityTime);        
        isInvincible = false;
   }
   
    private void faillureSystemeDecrease(){
        if(SceneManager.GetActiveScene().name !="QG" && SceneManager.GetActiveScene().name !="introStage" ){            
            PlayerStats.instance.faillureSystemCurrentValue--;            
        }
    }

    public void die(){
        if(FailleConfig.instance != null){
            if(FailleConfig.instance.playerIsInFaille){
                dieInFaille();
                return;
            }
        }
        
        PlayerMove.instance.moveDisable();
        GameoverManager gom = GameoverManager.instance;
        playerDeadSound.Play();
        faillureSystemeDecrease();
        gom.stageNameLostCrystal = SceneManager.GetActiveScene().name;
        gom.qtLostCrystal = PlayerStats.instance.totalShards; 
        gom.lostCrystalPos = transform.position;
        CrystalsShardsCounter.instance.setCrystalShardsValue(0);
        PlayerMove.instance.playTeleportationAnimation();
        GameoverManager.instance.onPLayerDeath();        
    }

    public void dieInFaille(){
        PlayerMove.instance.moveDisable();
        isHurted = false;
        playerDeadSound.Play();        
        PlayerMove.instance.playTeleportationAnimation();
        Invoke("delayExitFaille",0.6f);
    }

    private void delayExitFaille(){
        FailleConfig.instance.exitFaille(false);
    }
   
}
