using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class PlayerDatas{
    public int level;
    public int shards;
    public int exp;
    public int nextExpToLevelUp;
    public float maxHealth;
    public float defence;
    public float damage;
    public float bulletDamage;
    public float maxBulletQuantity;
    public float jumpPower;
    public float velocity;
    public float dashVelocity;
    public float fireRate;
    public float luck;
    public float criticalDmg;
    public float criticalFreguency;
    public int nbHunterPts;
    public float bountyBonus;

    public int totalmedals1;
    public int totalmedals2;
    public int totalmedalsGold;
    public float maxFaillureQt;
    public float currentFaillureQt;

    //stats equipement//
    public float maxHealthEquiped;
    public float maxBulletQuantityEquiped;
    public float damageEquiped;    
    public float bulletDamageEquiped;
    public float defenceEquiped;
    public float velocityEquiped;
    public float fireRateEquiped;
    public float dashVelocityEquiped;
    public float jumpPowerEquiped;
    public float luckEquiped;
    public float criticalDmgEquiped;
    public float criticalFreguencyEquiped;

    // list des objects
    public List<string> allComponents = new List<string>();
    public List<string> allLootedBGMusics = new List<string>();
    public List<string> allBoughtBGMusics = new List<string>();
    public List<string> allLootGalleryImage = new List<string>();
    public List<string> allBoughtGalleryImage = new List<string>();

    //list des upgrade SPHERIER    
    public int[] allSphereAtkPanel = new int[10];
    public int[] allSphereDextPanel = new int[10];
    public int[] allSphereSurvPanel = new int[10];

    //list des enemies tué
    public Dictionary<string,int> listEnemies = new Dictionary<string,int>();
    public List<string> listEnemiesGetReward = new List<string>();

    //list des equipements
    public List<Dictionary<string,object>> allEquipementsSatelite1 = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsSatelite2 = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsHelmet = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsBody = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsGun = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsArm = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsLeg = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsBooster = new List<Dictionary<string, object>>();
    public List<Dictionary<string,object>> allEquipementsSword = new List<Dictionary<string, object>>();

    // //list des equipements equipé
    public Dictionary<string, object> slotSatelite1 = new Dictionary<string, object>();
    public Dictionary<string, object> slotSatelite2 = new Dictionary<string, object>();
    public Dictionary<string, object> slotHead = new Dictionary<string, object>();
    public Dictionary<string, object> slotBody = new Dictionary<string, object>();
    public Dictionary<string, object> slotGun = new Dictionary<string, object>();
    public Dictionary<string, object> slotArm = new Dictionary<string, object>();
    public Dictionary<string, object> slotLeg = new Dictionary<string, object>();
    public Dictionary<string, object> slotBooster = new Dictionary<string, object>();    
    public Dictionary<string, object> slotSword = new Dictionary<string, object>();

    //list de la pool de boss
    public List<Boss> bossSpawned = new List<Boss>();
    public DateTime dateSpawningBoss;
    public int totalNbBossSpawned;
    public int nbPurifDoneToday;

    //list des parametre du computer
    public int qtMaxPurificationAllowed;

    //list des stats PNJ
    public int[] aliaStats;
    public int[] axlStats;
    public int[] dynamoStats;
    public int[] irisStats;
    public int[] megamanStats;
    public int[] sigmaStats;
    public int[] vileStats;

    //list playerAchievements
    public int nbKillEnemies;
    public int nbKillReploids;
    public int nbKillBoss;
    public int nbKillDarkBoss;
    public int maxZchain;

    //list PlayerNewMovements
    public bool obtain_chargedShot;
    public bool obtain_counterDash;
    public bool obtain_jumpRollingSword;
    public bool obtain_dragonPunchSword;
    public bool obtain_earthquake;
    public bool obtain_fallingSword;
    public bool obtain_amethysStrike;
    public bool obtain_glissade;
    public bool obtain_chargedSword;
    public bool obtain_jumpChargedSword;
    public bool obtain_jumpChargedShot;
    public bool obtain_tatsumaki;
    public bool obtain_shoryuken;
    public bool obtain_furyBlanche;


    public PlayerDatas(){
        maxHealthEquiped            = PlayerStats.instance.maxHealthEquiped;
        maxBulletQuantityEquiped    = PlayerStats.instance.maxBulletQuantityEquiped;
        damageEquiped               = PlayerStats.instance.damageEquiped;
        bulletDamageEquiped         = PlayerStats.instance.bulletDamageEquiped;
        defenceEquiped              = PlayerStats.instance.defenceEquiped;
        velocityEquiped             = PlayerStats.instance.velocityEquiped;
        fireRateEquiped             = PlayerStats.instance.fireRateEquiped;
        dashVelocityEquiped         = PlayerStats.instance.dashVelocityEquiped;
        jumpPowerEquiped            = PlayerStats.instance.jumpPowerEquiped;
        luckEquiped                 = PlayerStats.instance.luckEquiped;
        criticalDmgEquiped          = PlayerStats.instance.criticalDmgEquiped;
        criticalFreguencyEquiped    = PlayerStats.instance.criticalFreguencyEquiped;

        level               = PlayerStats.instance.level;
        shards              = PlayerStats.instance.totalShards;
        exp                 = PlayerStats.instance.exp;
        nextExpToLevelUp    = PlayerStats.instance.nextExpToLevelUp;
        maxHealth           = PlayerStats.instance.maxHealth - maxHealthEquiped;
        defence             = PlayerStats.instance.defence - defenceEquiped;
        damage              = PlayerStats.instance.damage - damageEquiped;
        bulletDamage        = PlayerStats.instance.bulletDamage - bulletDamageEquiped;
        maxBulletQuantity   = PlayerStats.instance.maxBulletQuantity - maxBulletQuantityEquiped;
        jumpPower           = PlayerStats.instance.jumpPower - jumpPowerEquiped;
        velocity            = PlayerStats.instance.velocity - velocityEquiped;
        dashVelocity        = PlayerStats.instance.dashVelocity - dashVelocityEquiped;
        fireRate            = PlayerStats.instance.fireRate - fireRateEquiped;
        luck                = PlayerStats.instance.luck - luckEquiped;
        criticalDmg         = PlayerStats.instance.criticalDmg - criticalDmgEquiped;
        criticalFreguency   = PlayerStats.instance.criticalFreguency - criticalFreguencyEquiped;
        nbHunterPts         = PlayerStats.instance.nbHunterPts;
        bountyBonus         = PlayerStats.instance.bountyBonus;
        maxFaillureQt       = PlayerStats.instance.faillureSystemMaxValue;
        currentFaillureQt   = PlayerStats.instance.faillureSystemCurrentValue; 
        totalmedals1        = PlayerStats.instance.totalmedals1;
        totalmedals2        = PlayerStats.instance.totalmedals2;
        totalmedalsGold     = PlayerStats.instance.totalmedalsGold;

        
        foreach(GameObject compo in PlayerGainsObjects.instance.allComponents){
            allComponents.Add(compo.name);
        }

        foreach(GameObject lootedBgm in PlayerGainsObjects.instance.allLootedBGMusics){
            allLootedBGMusics.Add(lootedBgm.name);
        }

        foreach(GameObject boughtBgm in PlayerGainsObjects.instance.allBoughtBGMusics){
            allBoughtBGMusics.Add(boughtBgm.name);
        }

        foreach(ImageGallery lootedImage in PlayerGainsObjects.instance.allLootGalleryImage){
            allLootGalleryImage.Add(lootedImage.gameObject.name);
        }

        foreach(ImageGallery boughtImage in PlayerGainsObjects.instance.allBoughtGalleryImage){            
            allBoughtGalleryImage.Add(boughtImage.gameObject.name);
        }
        
        allSphereAtkPanel= PlayerGainsObjects.instance.allSphereAtkPanel;
        allSphereDextPanel= PlayerGainsObjects.instance.allSphereDextPanel;
        allSphereSurvPanel= PlayerGainsObjects.instance.allSphereSurvPanel;

        listEnemies             = playerEnemyKillCounter.instance.getListEnemyKill();
        listEnemiesGetReward    = playerEnemyKillCounter.instance.listEnemiesGetReward;

        allEquipementsSatelite1 = PlayerGainsObjects.instance.allEquipementsSatelite1;
        allEquipementsSatelite2 = PlayerGainsObjects.instance.allEquipementsSatelite2;
        allEquipementsHelmet    = PlayerGainsObjects.instance.allEquipementsHelmet;
        allEquipementsBody      = PlayerGainsObjects.instance.allEquipementsBody;
        allEquipementsGun       = PlayerGainsObjects.instance.allEquipementsGun;
        allEquipementsArm       = PlayerGainsObjects.instance.allEquipementsArm;
        allEquipementsLeg       = PlayerGainsObjects.instance.allEquipementsLeg;
        allEquipementsBooster   = PlayerGainsObjects.instance.allEquipementsBooster;
        allEquipementsSword     = PlayerGainsObjects.instance.allEquipementsSword;

        slotSatelite1           = PlayerEquipments.instance.slotSatelite1;
        slotSatelite2           = PlayerEquipments.instance.slotSatelite2;
        slotHead                = PlayerEquipments.instance.slotHead;
        slotBody                = PlayerEquipments.instance.slotBody;
        slotGun                 = PlayerEquipments.instance.slotGun;
        slotArm                 = PlayerEquipments.instance.slotArm;
        slotLeg                 = PlayerEquipments.instance.slotLeg;
        slotBooster             = PlayerEquipments.instance.slotBooster;
        slotSword               = PlayerEquipments.instance.slotSword;

        bossSpawned         = BossSpawner.instance.bossSpawned;
        dateSpawningBoss    = BossSpawner.instance.dateSpawning;
        totalNbBossSpawned  = BossSpawner.instance.totalNbBossSpawned;

        nbPurifDoneToday      = BossSpawner.instance.nbPurifDoneToday;
        qtMaxPurificationAllowed = ComputerParameters.instance.qtMaxPurificationAllowed;


        aliaStats       = AliaStat.instance.saveStats();
        axlStats        = AxlStat.instance.saveStats();
        dynamoStats     = DynamoStat.instance.saveStats();
        irisStats       = IrisStat.instance.saveStats();
        megamanStats    = MegamanStat.instance.saveStats();
        sigmaStats      = SigmaStat.instance.saveStats();
        vileStats       = VileStat.instance.saveStats();

        nbKillEnemies   = PlayerAchievements.instance.nbKillEnemies;
        nbKillReploids  = PlayerAchievements.instance.nbKillReploids;
        nbKillBoss      = PlayerAchievements.instance.nbKillBoss;
        nbKillDarkBoss  = PlayerAchievements.instance.nbKillDarkBoss;
        maxZchain       = PlayerAchievements.instance.maxZchain;

        obtain_chargedShot      = PlayerNewMovements.instance.obtain_chargedShot;
        obtain_counterDash      = PlayerNewMovements.instance.obtain_counterDash;
        obtain_jumpRollingSword = PlayerNewMovements.instance.obtain_jumpRollingSword;
        obtain_dragonPunchSword = PlayerNewMovements.instance.obtain_dragonPunchSword;
        obtain_earthquake       = PlayerNewMovements.instance.obtain_earthquake;
        obtain_fallingSword     = PlayerNewMovements.instance.obtain_fallingSword;
        obtain_amethysStrike    = PlayerNewMovements.instance.obtain_amethysStrike;
        obtain_glissade         = PlayerNewMovements.instance.obtain_glissade;
        obtain_chargedSword     = PlayerNewMovements.instance.obtain_chargedSword;
        obtain_jumpChargedSword = PlayerNewMovements.instance.obtain_jumpChargedSword;
        obtain_jumpChargedShot  = PlayerNewMovements.instance.obtain_jumpChargedShot;
        obtain_tatsumaki        = PlayerNewMovements.instance.obtain_tatsumaki;
        obtain_shoryuken        = PlayerNewMovements.instance.obtain_shoryuken;
        obtain_furyBlanche      = PlayerNewMovements.instance.obtain_furyBlanche;

    }
}