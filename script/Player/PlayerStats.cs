using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level = 1;
    public int exp = 0;

    [HideInInspector]
    public int nextExpToLevelUp = 10;
    public float maxHealth;
    public float defence;
    public float damage;
    public float bulletDamage;
    public float maxBulletQuantity;
    public float jumpPower;
    public float velocity;
    public float dashVelocity;
    public float fireRate = 0.4f;
    public float luck = 0;
    public float bountyBonus = 1.2f;
    public float criticalDmg = 1.5f;
    public float criticalFreguency = 5.0f;

    //--------------------------------------------//
    public int nbHunterPts = 50;
    
    [HideInInspector]
    public float currentBulletsQt;
    public BulletsBar bulletsBar;
    public HealthBar healthBar;
    public int totalShards = 0;
    public int totalmedals1 = 0;
    public int totalmedals2 = 0;
    public int totalmedalsGold = 0;
    public float faillureSystemMaxValue = 3;
    public float faillureSystemCurrentValue = 3;

    //-------------------boosted stats-------------------------//     
    [HideInInspector]
    public float maxHealthBoosted = 0;
    [HideInInspector]
    public float maxBulletQuantityBoosted = 0;
    [HideInInspector]
    public float damageBoosted = 0;
    [HideInInspector]
    public float bulletDamageBoosted = 0;
    [HideInInspector]
    public float defenceBoosted = 0;
    [HideInInspector]
    public float velocityBoosted = 0;
    [HideInInspector]
    public float fireRateBoosted = 0;
    [HideInInspector]
    public float dashVelocityBoosted = 0;
    [HideInInspector]
    public float jumpPowerBoosted = 0;
    [HideInInspector]
    public float luckBoosted = 0;
    [HideInInspector]
    public float criticalDmgBoosted = 0;
    [HideInInspector]
    public float criticalFreguencyBoosted = 0;
    //---------------------------------------------------------//

    //-------------------equipement stats-------------------------//     
    [HideInInspector]
    public float maxHealthEquiped = 0;
    [HideInInspector]
    public float maxBulletQuantityEquiped = 0;
    [HideInInspector]
    public float damageEquiped = 0;
    [HideInInspector]
    public float bulletDamageEquiped = 0;
    [HideInInspector]
    public float defenceEquiped = 0;
    [HideInInspector]
    public float velocityEquiped = 0;
    [HideInInspector]
    public float fireRateEquiped = 0;
    [HideInInspector]
    public float dashVelocityEquiped = 0;
    [HideInInspector]
    public float jumpPowerEquiped = 0;
    [HideInInspector]
    public float luckEquiped = 0;
    [HideInInspector]
    public float criticalDmgEquiped = 0;
    [HideInInspector]
    public float criticalFreguencyEquiped = 0;
    //---------------------------------------------------------//

    private GameObject levelUpTxt;


    public static PlayerStats instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de PLayerstats");
            return;
        }
        instance = this;
        currentBulletsQt = PlayerStats.instance.maxBulletQuantity;
        bulletsBar.SetMaxBullets(currentBulletsQt);
        bulletsBar.SetBullets(currentBulletsQt);        
        levelUpTxt = transform.Find("Canvas/levelUpText").gameObject;          
    }
    private void Start() {      
        CrystalsShardsCounter.instance.setCrystalShardsValue(totalShards);    
    } 

    public void restoreFaillingSystem(){
        faillureSystemCurrentValue = faillureSystemMaxValue;
    }

    public void reloadBullets(float value){
        currentBulletsQt += value;
        if(currentBulletsQt > maxBulletQuantity){
            currentBulletsQt = maxBulletQuantity;
        }
        bulletsBar.SetBullets(currentBulletsQt);
    }
    public void looseBullets(float value){        
        currentBulletsQt -= value;
        bulletsBar.SetBullets(currentBulletsQt);        
    }

    public void fullReloadBullets(){
        reloadBullets(maxBulletQuantity);
        bulletsBar.SetMaxBullets(maxBulletQuantity);
    }

    public void fullRestore(){
        PlayerHealth.instance.fullHealing();
        fullReloadBullets();
    }

    public void gainExp(float gain){
        exp += Mathf.CeilToInt(gain);
        while(exp >= nextExpToLevelUp){ 
            PlayerAnimationUi.instance.playAnimLevelUp();
            int diffExp = exp - nextExpToLevelUp;         
            level++;
            exp = diffExp;
            nextExpToLevelUp *= 2;
            gainMaxHealth(5);
            gainMaxBulletQuantity(3);
            if(level % 5 == 0){
                gainluck(1);
            }  
            if(level % 3 == 0){
                gainBountyBonus(0.1f);
            }  
        }
    } 

    public void gainMaxHealth(int gain){
        maxHealth += gain;
    }
    public void gainMaxBulletQuantity(int gain){
        maxBulletQuantity += gain;
    }
   
    public void gainluck(int gain){
        luck += gain;
    } 

    public void gainBountyBonus(float gain){
        bountyBonus += gain;
    }
         
    public void increaseStats(string typeItem,PowersUp.List stat,float val){
        switch (stat){
            case PowersUp.List.VieMax:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    maxHealthBoosted += Mathf.Round(val);
                if(typeItem == "equipement")
                    maxHealthEquiped += Mathf.Round(val);

                maxHealth += Mathf.Round(val);
                healthBar.setMaxHealth(maxHealth);
                PlayerHealth.instance.healing((int) Mathf.Round(val));
                break;
            case PowersUp.List.BulletQt:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    maxBulletQuantityBoosted += Mathf.Round(val);
                if(typeItem == "equipement")
                    maxBulletQuantityEquiped += Mathf.Round(val);

                maxBulletQuantity += Mathf.Round(val);
                bulletsBar.SetMaxBullets(maxBulletQuantity);
                PlayerStats.instance.reloadBullets((int) Mathf.Round(val));
                break;
            case PowersUp.List.Chance:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    luckBoosted += val;
                if(typeItem == "equipement")
                    luckEquiped += val;

                luck += val;
                break;
            case PowersUp.List.Dmg:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    damageBoosted += val;
                if(typeItem == "equipement")
                    damageEquiped += val;

                damage += val;
                break;
            case PowersUp.List.DmgTir:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    bulletDamageBoosted += val;
                if(typeItem == "equipement")
                    bulletDamageEquiped += val;

                bulletDamage += val;
                break;
            case PowersUp.List.Defence:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    defenceBoosted += val;
                if(typeItem == "equipement")
                    defenceEquiped += val;

                defence += val;
                break;
            case PowersUp.List.Accel:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    velocityBoosted += val;
                if(typeItem == "equipement")
                    velocityEquiped += val;

                velocity += val;
                break;
            case PowersUp.List.AccelTir:  
                if(typeItem == "booster" || typeItem == "neuroHack")              
                    fireRateBoosted += val;
                if(typeItem == "equipement")
                    fireRateEquiped += val;

                fireRate += val;
                break;
            case PowersUp.List.HtSaut:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    jumpPowerBoosted += val;
                if(typeItem == "equipement")
                    jumpPowerEquiped += val;

                jumpPower += val;
                break;
            case PowersUp.List.Dash:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    dashVelocityBoosted += val;
                if(typeItem == "equipement")
                    dashVelocityEquiped += val;

                dashVelocity += val;
                break;
            case PowersUp.List.criticalDmg:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    criticalDmgBoosted += val;
                if(typeItem == "equipement")
                    criticalDmgEquiped += val;

                criticalDmg += val;
                break;
            case PowersUp.List.criticalFreguency:
                if(typeItem == "booster" || typeItem == "neuroHack")
                    criticalFreguencyBoosted += val;
                if(typeItem == "equipement")
                    criticalFreguencyEquiped += val;

                criticalFreguency += val;
                break;
        }
    } 

    public float getLootLuckResult(int diceFaces){
        return Random.Range(0,diceFaces) - luck;
    }


    public void loadStats(PlayerDatas datas){
        level               = datas.level;
        exp                 = datas.exp;    
        nextExpToLevelUp    = datas.nextExpToLevelUp;
        maxHealth           = datas.maxHealth;
        defence             = datas.defence;
        damage              = datas.damage;
        bulletDamage        = datas.bulletDamage;
        maxBulletQuantity   = datas.maxBulletQuantity;
        jumpPower           = datas.jumpPower;
        velocity            = datas.velocity;
        dashVelocity        = datas.dashVelocity;
        fireRate            = datas.fireRate;
        luck                = datas.luck;
        criticalDmg         = datas.criticalDmg;
        criticalFreguency   = datas.criticalFreguency;
        nbHunterPts         = datas.nbHunterPts;
        bountyBonus         = datas.bountyBonus;

        CrystalsShardsCounter.instance.setCrystalShardsValue(datas.shards);
        faillureSystemMaxValue      = datas.maxFaillureQt;
        faillureSystemCurrentValue  = datas.currentFaillureQt;
        totalmedals1                = datas.totalmedals1;
        totalmedals2                = datas.totalmedals2;
        totalmedalsGold             = datas.totalmedalsGold;

        increaseStats("equipement",PowersUp.List.VieMax,datas.maxHealthEquiped);
        increaseStats("equipement",PowersUp.List.BulletQt,datas.maxBulletQuantityEquiped);
        increaseStats("equipement",PowersUp.List.Dmg,datas.damageEquiped);
        increaseStats("equipement",PowersUp.List.DmgTir,datas.bulletDamageEquiped);
        increaseStats("equipement",PowersUp.List.Defence,datas.defenceEquiped);
        increaseStats("equipement",PowersUp.List.Accel,datas.velocityEquiped);
        increaseStats("equipement",PowersUp.List.AccelTir,datas.fireRateEquiped);
        increaseStats("equipement",PowersUp.List.Dash,datas.dashVelocityEquiped);
        increaseStats("equipement",PowersUp.List.HtSaut,datas.jumpPowerEquiped);
        increaseStats("equipement",PowersUp.List.Chance,datas.luckEquiped);
        increaseStats("equipement",PowersUp.List.criticalDmg,datas.criticalDmgEquiped);
        increaseStats("equipement",PowersUp.List.criticalFreguency,datas.criticalFreguencyEquiped);
    }



}

