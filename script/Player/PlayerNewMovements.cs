using UnityEngine;

public class PlayerNewMovements : MonoBehaviour
{
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
    public static PlayerNewMovements instance;
    private void Awake() {
        if(instance != null){            
            return;
        }
        instance = this;
    }

    public void loadNewMovementsObtained(PlayerDatas data){
        obtain_chargedShot      = data.obtain_chargedShot;
        obtain_counterDash      = data.obtain_counterDash;
        obtain_jumpRollingSword = data.obtain_jumpRollingSword;
        obtain_dragonPunchSword = data.obtain_dragonPunchSword;
        obtain_earthquake       = data.obtain_earthquake;
        obtain_fallingSword     = data.obtain_fallingSword;
        obtain_amethysStrike    = data.obtain_amethysStrike;
        obtain_glissade         = data.obtain_glissade;
        obtain_chargedSword     = data.obtain_chargedSword;
        obtain_jumpChargedSword = data.obtain_jumpChargedSword;
        obtain_jumpChargedShot  = data.obtain_jumpChargedShot;
        obtain_tatsumaki        = data.obtain_tatsumaki;
        obtain_shoryuken        = data.obtain_shoryuken;
        obtain_furyBlanche      = data.obtain_furyBlanche;
    }

}
