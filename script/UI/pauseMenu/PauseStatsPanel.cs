using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseStatsPanel : MonoBehaviour
{
    public static PauseStatsPanel instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de PauseStatsPanel");
            return;
        }
        instance = this;
    }

    public void showStatsPlayer(){
        transform.Find("statsBox/level-value").GetComponent<Text>().text = ""+PlayerStats.instance.level;
        transform.Find("statsBox/exp-value").GetComponent<Text>().text = PlayerStats.instance.exp+"/"+ PlayerStats.instance.nextExpToLevelUp;

        transform.Find("statsBox/life-value").GetComponent<Text>().text = PlayerHealth.instance.currentHealth+"/"+ PlayerStats.instance.maxHealth;
        transform.Find("statsBox/energy-value").GetComponent<Text>().text = PlayerStats.instance.currentBulletsQt+"/"+ PlayerStats.instance.maxBulletQuantity;
        transform.Find("statsBox/dmg-s-value").GetComponent<Text>().text = ""+PlayerStats.instance.damage;
        transform.Find("statsBox/dmg-f-value").GetComponent<Text>().text = ""+PlayerStats.instance.bulletDamage;
        transform.Find("statsBox/armor-value").GetComponent<Text>().text = ""+PlayerStats.instance.defence;
        transform.Find("statsBox/speed-value").GetComponent<Text>().text = ""+PlayerStats.instance.velocity;
        transform.Find("statsBox/fire-rate-value").GetComponent<Text>().text = ""+PlayerStats.instance.fireRate;
        transform.Find("statsBox/dash-value").GetComponent<Text>().text = ""+PlayerStats.instance.dashVelocity;
        transform.Find("statsBox/jump-value").GetComponent<Text>().text = ""+PlayerStats.instance.jumpPower;
        transform.Find("statsBox/luck-value").GetComponent<Text>().text = ""+PlayerStats.instance.luck;
        transform.Find("statsBox/criticalDmg-value").GetComponent<Text>().text = PlayerStats.instance.criticalDmg+"%";
        transform.Find("statsBox/criticalRate-value").GetComponent<Text>().text = PlayerStats.instance.criticalFreguency+"%"; 
        transform.Find("statsBox/bountyBonus-value").GetComponent<Text>().text = PlayerStats.instance.bountyBonus.ToString();
    }

    public void showStatsBoostersAndEquip(){
        changeUiInfosValueColor(transform.Find("StatsInfos/maxHealthVal"),PlayerStats.instance.maxHealthBoosted+PlayerStats.instance.maxHealthEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/maxBulletQuantityVal"),PlayerStats.instance.maxBulletQuantityBoosted + PlayerStats.instance.maxBulletQuantityEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/damageVal"),PlayerStats.instance.damageBoosted+PlayerStats.instance.damageEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/bulletDamageVal"),PlayerStats.instance.bulletDamageBoosted+PlayerStats.instance.bulletDamageEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/DefenceVal"),PlayerStats.instance.defenceBoosted+PlayerStats.instance.defenceEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/velocityVal"),PlayerStats.instance.velocityBoosted+PlayerStats.instance.velocityEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/fireRateVal"),PlayerStats.instance.fireRateBoosted+PlayerStats.instance.fireRateEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/dashVelocityVal"),PlayerStats.instance.dashVelocityBoosted+PlayerStats.instance.dashVelocityEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/jumpPowerVal"),PlayerStats.instance.jumpPowerBoosted+PlayerStats.instance.jumpPowerEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/luckVal"),PlayerStats.instance.luckBoosted+PlayerStats.instance.luckEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/criticalVal"),PlayerStats.instance.criticalDmgBoosted+PlayerStats.instance.criticalDmgEquiped);
        changeUiInfosValueColor(transform.Find("StatsInfos/criticalRateVal"),PlayerStats.instance.criticalFreguencyBoosted+PlayerStats.instance.criticalFreguencyEquiped);
    }
    
    private string writePostiveStat(float val){
        if(val>=0){
            return " +"+val;
        }else{
            return " "+val;
        }
    }
    private void changeUiInfosValueColor(Transform textValue, float val){
        if(val < 0){
            textValue.GetComponent<TextMeshProUGUI>().color = Color.red;
        }else{
            textValue.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        textValue.GetComponent<TextMeshProUGUI>().text = writePostiveStat(val);
    }
}
