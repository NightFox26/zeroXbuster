using UnityEngine;

public class Caisse : MonoBehaviour
{
    public float health = 10;
    public BoxConditions.List condition;  
    public int conditionAmount;
    public int ratioToCompo = 40;
    public int ratioToBooster = 20;
    public GameObject destructRef;
    public GameObject goldenMob;
    public int ratioSpawnGoldenMob = 5;
    private bool isBroken = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("sword")){
            if(checkCondition()){
                health -= PlayerStats.instance.damage;
            }
        }else if(other.CompareTag("bullet")){
            if(checkCondition()){
                health -= PlayerStats.instance.bulletDamage;
            }
        }

        if(health <= 0 && !isBroken){
            isBroken = true;
            getReward();
            Instantiate(destructRef,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void getReward(){
        float rand = Random.Range(0f,100f);        
        rand -= PlayerStats.instance.luck;
        if(rand <= ratioSpawnGoldenMob){
            Instantiate(goldenMob,transform.position,Quaternion.identity);
        }

        if(condition == BoxConditions.List.none){
            if(rand<30)
                spawnXshards(1);           
        }else{
            spawnXshards(3);
            if(PlayerStats.instance.getLootLuckResult(100) < ratioToCompo){
                Instantiate(ItemsListing.instance.getRandomComponent(),transform.position,Quaternion.identity);            
            }
            if(PlayerStats.instance.getLootLuckResult(100) < ratioToBooster){
                Instantiate(ItemsListing.instance.getRandomBooster(),transform.position,Quaternion.identity);            
            }
        }
    }

    private void spawnXshards(int i){
        while(i>0){
            Instantiate(ItemsListing.instance.getShards(),transform.position,Quaternion.identity);
            i--;
        }
    }

    private bool checkCondition(){
        if(condition == BoxConditions.List.none){
            return true;
        }else{
            Sentence txt = GetComponent<DialogueTrigger>().sentences[0];
            if(condition == BoxConditions.List.bounty){
                if(checkBountyCondition()) return true;
            }else if(condition == BoxConditions.List.kill){
                int nbDeltaEnemy = conditionAmount - (RankingPanel.instance.nbEnemiesKill + RankingPanel.instance.nbReploidsKill + RankingPanel.instance.nbMavericksKill);
                txt.message = "Cette boite necessite que tu tues "+nbDeltaEnemy+" ennemies de plus pour etre ouverte ...";
                if(checkKillCondition()) return true;
            }else if(condition == BoxConditions.List.time){
                int deltaTime = Mathf.FloorToInt(Time.timeSinceLevelLoad - conditionAmount);
                txt.message = "Tu es arrivé "+deltaTime+"s trop tard pour que le verrou de cette boite s'ouvre ...";
                if(checkTimeCondition()) return true;
            }else if(condition == BoxConditions.List.health){
                txt.message = "Cette boite nécessitait que tu ai subis moins de "+conditionAmount+"pts de degats pour etre ouverte ....";
                if(checkHealthCondition()) return true;
            }
            GetComponent<DialogueTrigger>().launchDialogue();
        }
        return false;
    }

    private bool checkBountyCondition(){
        return PlayerHealth.instance.getStealthMode();
    }

    private bool checkKillCondition(){
        return (RankingPanel.instance.nbEnemiesKill + RankingPanel.instance.nbReploidsKill + RankingPanel.instance.nbMavericksKill) >= conditionAmount;
    }

    private bool checkTimeCondition(){
        return RankingPanel.instance.timeLevel < conditionAmount;
    }

    private bool checkHealthCondition(){
        return RankingPanel.instance.totalDamageTaken < conditionAmount;
    }
}
