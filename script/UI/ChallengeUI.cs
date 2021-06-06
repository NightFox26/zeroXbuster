using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChallengeUI : MonoBehaviour
{
    public GameObject uiPanel;
    public GameObject uiTimerText;
    public GameObject uiLifePanel;
    public GameObject uiEnemyPanel;
    public GameObject rewardPanel;
    public GameObject rewardPalierNb;
    public GameObject rewardKillNb;
    public GameObject rewardRewardBox;
    public static ChallengeUI instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de ChallengeUI");
            return;
        }
        instance = this;
    }

    private void Start() {        
        hideUi();
    }

    public void initChallengeUi(float timer,int life,Sprite enemy){   
        showUi();     
        uiTimerText.GetComponent<Text>().text = timer.ToString();
        showNbLife(life);

        GameObject counter = uiEnemyPanel.transform.Find("Text").gameObject;
        counter.GetComponent<Text>().text = "0 / 20";

        uiEnemyPanel.transform.Find("enemie img").GetComponent<Image>().sprite = enemy;
    }

    public void updateUiTimer(float timer){
        uiTimerText.GetComponent<Text>().text = timer.ToString();     
    }

    public void updateUiEnemyKill(int nbKill){
        GameObject counter = uiEnemyPanel.transform.Find("Text").gameObject;
        counter.GetComponent<Text>().text = nbKill+" / 20";     
    }

    public void updateUiLife(int life){
        hideAllLife();
        showNbLife(life);
    }   

    public void updateRewardPanel(int palier, int killEnemies){
        rewardPalierNb.GetComponent<Text>().text = palier.ToString();
        rewardKillNb.GetComponent<Text>().text = killEnemies.ToString();
        updateRewardLoot(palier);
    }

    public void updateRewardLoot(int palier){
        Item randBooster = ItemsListing.instance.getRandomBooster();
        switch (palier)
        {
            case(4):
                rewardRewardBox.GetComponent<Image>().color = ItemColor.purple();
                randBooster.GetComponent<ItemBooster>().rarity = Rarity.List.purple;
                break;
            case(3):
                rewardRewardBox.GetComponent<Image>().color = ItemColor.blue();
                randBooster.GetComponent<ItemBooster>().rarity = Rarity.List.blue;
                break;
            case(2):
                rewardRewardBox.GetComponent<Image>().color = ItemColor.green();
                randBooster.GetComponent<ItemBooster>().rarity = Rarity.List.green;
                break;
            case(1):
                rewardRewardBox.GetComponent<Image>().color = ItemColor.gray();
                randBooster.GetComponent<ItemBooster>().rarity = Rarity.List.normal;
                break;
        }
        
        rewardRewardBox.transform.Find("Image").GetComponent<Image>().sprite = randBooster.gameObject.GetComponent<SpriteRenderer>().sprite;


        GameObject cloneLoot = Instantiate(randBooster,new Vector3(0,0,1),Quaternion.identity).gameObject;
        cloneLoot.name = randBooster.name;
        StartCoroutine(gainbooster(cloneLoot));
    }

    IEnumerator gainbooster(GameObject booster){
        yield return new WaitForSeconds(0.5f);
        booster.GetComponent<ItemBooster>().playerGainBooster();  
    }

    private void hideAllLife(){
        foreach (Transform lifeIcon in uiLifePanel.transform)
        {
            lifeIcon.gameObject.SetActive(false);
        }
    }

    private void showNbLife(int nb){
        int i=0;
        foreach (Transform lifeIcon in uiLifePanel.transform)
        {
            if(i < nb){
                lifeIcon.gameObject.SetActive(true);
            }
            i++;
        }
    }

    public void hideUi(){
        uiPanel.SetActive(false);
        rewardPanel.SetActive(false);
    }

    public void showUi(){
        uiPanel.SetActive(true);
    }
    public void showReward(){
        rewardPanel.SetActive(true);
    }
}
