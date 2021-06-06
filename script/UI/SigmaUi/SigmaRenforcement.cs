using UnityEngine;
using UnityEngine.UI;

public class SigmaRenforcement : MonoBehaviour
{
    public Renforcements.List renforcement;
    public int costShards;
    public int costMedals1;
    public int costMedals2;
    public int costMedalsGold;

    [TextArea]
    public string textInfo;
    private Animator anim;
    private GameObject locker;
    public Transform infosBox;
    private Text infosBoxPrice;
    private Text infosBoxcostMedal1;
    private Text infosBoxcostMedal2;
    private Text infosBoxcostMedalgold;
    private Text infosBoxText;
    private void Awake() {
        locker = transform.Find("lock").gameObject;
        anim = transform.Find("renforcementAnim").GetComponent<Animator>();        
        anim.Play(renforcement.ToString());        
        infosBoxPrice = infosBox.Find("PanelGold/price").GetComponent<Text>();
        infosBoxcostMedal1 = infosBox.Find("PanelMedal1/price").GetComponent<Text>();
        infosBoxcostMedal2 = infosBox.Find("PanelMedal2/price").GetComponent<Text>();
        infosBoxcostMedalgold = infosBox.Find("PanelMedalGold/price").GetComponent<Text>();
        infosBoxText = infosBox.Find("PanelText/Text").GetComponent<Text>(); 
         
        if(renforcement != Renforcements.List.chargedShot)
        Invoke("stopAnimation",0.05f);            
    }

    public void stopAnimation(){
        anim.enabled = false;        
    }

    public void updateInfosBox(){
        int gotMedal1    = PlayerStats.instance.totalmedals1;
        int gotMedal2    = PlayerStats.instance.totalmedals2;
        int gotMedalGold = PlayerStats.instance.totalmedalsGold;

        anim.enabled = true;
        anim.Play(renforcement.ToString());
        infosBoxPrice.text = ""+costShards;
        infosBoxcostMedal1.text = ""+gotMedal1+"/"+costMedals1;
        infosBoxcostMedal2.text = ""+gotMedal2+"/"+costMedals2;
        infosBoxcostMedalgold.text = ""+gotMedalGold+"/"+costMedalsGold;
        infosBoxText.text = textInfo;
    }

    private void Update() {        
        checkUnlocks();  
    }

    public void buyRenforcement(){
        if(!locker.activeSelf){
            print("on a deja ce coup");
            return;
        }

        if(PlayerStats.instance.totalShards >= costShards &&
        PlayerStats.instance.totalmedals1 >= costMedals1 && 
        PlayerStats.instance.totalmedals2 >= costMedals2){
            CrystalsShardsCounter.instance.removeCrystalShardsValue(costShards);

            if(renforcement == Renforcements.List.chargedShot){
                PlayerNewMovements.instance.obtain_chargedShot = true;
            }else if(renforcement == Renforcements.List.counterDash){
                PlayerNewMovements.instance.obtain_counterDash = true;
            }else if(renforcement == Renforcements.List.jumpRollingSword){
                PlayerNewMovements.instance.obtain_jumpRollingSword = true;
            }else if(renforcement == Renforcements.List.dragonPunchSword){
                PlayerNewMovements.instance.obtain_dragonPunchSword = true;
            }else if(renforcement == Renforcements.List.earthquake){
                PlayerNewMovements.instance.obtain_earthquake = true;
            }else if(renforcement == Renforcements.List.fallingSword){
                PlayerNewMovements.instance.obtain_fallingSword = true;
            }else if(renforcement == Renforcements.List.amethysStrike){
                PlayerNewMovements.instance.obtain_amethysStrike = true;
            }else if(renforcement == Renforcements.List.glissade){
                PlayerNewMovements.instance.obtain_glissade = true;
            }else if(renforcement == Renforcements.List.chargedSword){
                PlayerNewMovements.instance.obtain_chargedSword = true;
            }else if(renforcement == Renforcements.List.jumpChargedSword){
                PlayerNewMovements.instance.obtain_jumpChargedSword = true;
            }else if(renforcement == Renforcements.List.jumpChargedShot){
                PlayerNewMovements.instance.obtain_jumpChargedShot = true;
            }else if(renforcement == Renforcements.List.tatsumaki){
                PlayerNewMovements.instance.obtain_tatsumaki = true;
            }else if(renforcement == Renforcements.List.shoryuken){
                PlayerNewMovements.instance.obtain_shoryuken = true;
            }else if(renforcement == Renforcements.List.furyBlanche){
                PlayerNewMovements.instance.obtain_furyBlanche = true;
            }

            locker.SetActive(false);
        }
    }
    
    private void checkUnlocks()
    {
        locker.SetActive(true);

        if(renforcement == Renforcements.List.chargedShot && PlayerNewMovements.instance.obtain_chargedShot){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.counterDash && PlayerNewMovements.instance.obtain_counterDash){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.jumpRollingSword && PlayerNewMovements.instance.obtain_jumpRollingSword){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.dragonPunchSword && PlayerNewMovements.instance.obtain_dragonPunchSword){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.earthquake && PlayerNewMovements.instance.obtain_earthquake){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.fallingSword && PlayerNewMovements.instance.obtain_fallingSword){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.amethysStrike && PlayerNewMovements.instance.obtain_amethysStrike){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.glissade && PlayerNewMovements.instance.obtain_glissade){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.chargedSword && PlayerNewMovements.instance.obtain_chargedSword){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.jumpChargedSword && PlayerNewMovements.instance.obtain_jumpChargedSword){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.jumpChargedShot && PlayerNewMovements.instance.obtain_jumpChargedShot){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.tatsumaki && PlayerNewMovements.instance.obtain_tatsumaki){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.shoryuken && PlayerNewMovements.instance.obtain_shoryuken){
            locker.SetActive(false);
        }else if(renforcement == Renforcements.List.furyBlanche && PlayerNewMovements.instance.obtain_furyBlanche){
            locker.SetActive(false);
        }
    }
}
