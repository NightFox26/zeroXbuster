using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombo : MonoBehaviour
{    
    [HideInInspector]
    public int comboCounter = 0;
    public float comboTimerDelay = 0.1f;

    public GameObject comboPanel;
    public Slider sliderTimer;
    public Text comboTextValue;

    private int maxCombo = 0;

    public static PlayerCombo instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de player combo");
            return;
        }
        instance = this;
    }


    public void comboUp(){        
        comboPanel.SetActive(true);
        comboCounter++;
        updateZchainAchivement();
        setMaxCombo(comboCounter);
        PlayerStats.instance.luck++;
        comboTextValue.text = ""+comboCounter;
        sliderTimer.value = 10;
        StopAllCoroutines();
        StartCoroutine(timerComboActivate());
    }

    private void updateZchainAchivement(){
        if(PlayerAchievements.instance.maxZchain<comboCounter){
            PlayerAchievements.instance.maxZchain = comboCounter;
        }
    }

    public void comboCancel(){
        PlayerStats.instance.luck -= comboCounter;
        if(PlayerStats.instance.luck < 0){
            PlayerStats.instance.luck = 0;
        }
        comboCounter = 0;
        comboPanel.SetActive(false);
    }

    private void setMaxCombo(int zChain){
        if(zChain > maxCombo){
            maxCombo = zChain;
            RankingPanel.instance.zChainMax = maxCombo;
        }
    }

    IEnumerator timerComboActivate(){
        while(sliderTimer.value >= 0){
            if(sliderTimer.value == 0) comboCancel();

            yield return new WaitForSeconds(comboTimerDelay);
            sliderTimer.value--;
        }
    }

    


}
