using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HunterEnemyVignet : MonoBehaviour
{
    public GameObject enemy;
    public int currentKillValue = 0;
    public int maxKillValue = 70;
    public GameObject rewardItem;
    public int qtRewardItem;
    public GameObject panelReward;

    private void Start() {  
        getNbKill();

        if(currentKillValue == 0){
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(true);
        }
        gameObject.transform.Find("Image").GetComponent<Image>().sprite = enemy.transform.Find("enemy").GetComponent<SpriteRenderer>().sprite;

        Button btnReward = transform.Find("reward").GetComponent<Button>();
        btnReward.onClick.AddListener(delegate{getReward(btnReward);});

        btnReward.gameObject.SetActive(false);
        if(currentKillValue>=maxKillValue){
            currentKillValue = maxKillValue;
            if(!playerEnemyKillCounter.instance.listEnemiesGetReward.Contains(enemy.name)){
                btnReward.gameObject.SetActive(true);
            }
        }

        gameObject.transform.Find("counter").GetComponent<Text>().text = currentKillValue+"/"+maxKillValue;

        gameObject.transform.Find("Slider").GetComponent<Slider>().maxValue = maxKillValue;
        gameObject.transform.Find("Slider").GetComponent<Slider>().value = currentKillValue;
    }

    private void getNbKill(){
        currentKillValue = playerEnemyKillCounter.instance.getNbEnemyKill(enemy);  
    }

    private void getReward(Button btnReward){
        playerEnemyKillCounter.instance.listEnemiesGetReward.Add(enemy.name);
        btnReward.gameObject.SetActive(false);
        panelReward.SetActive(true);
        panelReward.transform.Find("ImageReward").GetComponent<Image>().sprite = rewardItem.GetComponent<SpriteRenderer>().sprite;
        panelReward.transform.Find("TextQt").GetComponent<Text>().text = " x "+qtRewardItem.ToString();

        int i=0;
        if(rewardItem.name == "crystal"){
            CrystalsShardsCounter.instance.addCrystalShardsValue(qtRewardItem);
        }else if(rewardItem.GetComponent<Item>().type == ItemType.List.composants){
            while(i<qtRewardItem){
                PlayerGainsObjects.instance.allComponents.Add(rewardItem);
                i++;
            }
        }

        StartCoroutine(hidePanelReward());
    }

    IEnumerator hidePanelReward(){
        yield return new WaitForSeconds(2);
        panelReward.SetActive(false);
    }

}
