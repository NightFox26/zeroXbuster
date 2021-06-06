using System.Collections;
using UnityEngine;

public class ChallengeRoom : MonoBehaviour
{
    private bool challengeStarted = false;
    private bool challengeLost = false;
    [HideInInspector]
    public bool challengeWin = false;
    public GameObject challengeTp;
    public float timeChallenge = 60;
    [HideInInspector]
    public int lifePlayer = 3;
    [HideInInspector]
    public int killEnemies = 0;
    private int pallier = 0;
    public EnemyChallenge[] enemies;
    public GameObject[] spawnPoints;
    public float delayToSpawnEnemy = 2;
    private float timePassedForSpawning = 0;
    private AudioSource bgm;    
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        bgm = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !challengeStarted){
            Sprite enemySprite = enemies[0].GetComponent<SpriteRenderer>().sprite;
            ChallengeUI.instance.initChallengeUi(timeChallenge,lifePlayer, enemySprite);
            bgm.Play();
            StartCoroutine(delayToPLay());
        }        
    }

    private void Update() {
        if(challengeWin)
            return;

        if(challengeStarted && !challengeLost && !challengeWin){
            checkPallier();
            timePassedForSpawning += Time.deltaTime;
            if(timePassedForSpawning >= delayToSpawnEnemy){
                timePassedForSpawning = 0;
                delayToSpawnEnemy -= 0.05f;
                makeSpawnEnemy();
            }

            if( (lifePlayer <= 0 || timeChallenge <= 0) && pallier == 0){
                challengeLost = true;
                challengeFinish();
            }
        }

        if((timeChallenge <= 0 && pallier > 0) || killEnemies >= 20 || (lifePlayer == 0 && pallier>0)){
            lifePlayer = 100;            
            challengeWin = true;
            claimReward();
        }
    }

    private void makeSpawnEnemy(){
        int randSpot = Random.Range(0,spawnPoints.Length);
        int randEnemyId = Random.Range(0,enemies.Length);
        GameObject enemy = Instantiate(enemies[randEnemyId],spawnPoints[randSpot].transform.position,Quaternion.identity).gameObject;
        enemy.GetComponent<EnemyChallenge>().atKSpeed -= pallier*0.05f;
        enemy.GetComponent<EnemyChallenge>().challengeConfig = this;
    }

    private void decreaseTimer(){
        if(!challengeWin){
            timeChallenge--;
            ChallengeUI.instance.updateUiTimer(timeChallenge);
        }
    }

    private void checkPallier(){
        if(killEnemies >= 20){
            pallier = 4;
        }else if(killEnemies >= 15){
            pallier = 3;
        }else if(killEnemies >= 10){
            pallier = 2;
        }else if(killEnemies >= 5){
            pallier = 1;
        }else{
            pallier = 0;
        }
    }

    private void claimReward(){
        PlayerMove.instance.moveDisable();
        ChallengeUI.instance.showReward();        
        ChallengeUI.instance.updateRewardPanel(pallier, killEnemies);
        StartCoroutine(delayCloseReward());
    }

    private void challengeFinish(){        
        Vector3 pos = new Vector3(challengeTp.transform.position.x,challengeTp.transform.position.y,player.transform.position.z);
        PlayerMove.instance.teleportToPosition(pos);
        ChallengeUI.instance.hideUi();
        bgm.Stop();
        Destroy(challengeTp);
    }

    IEnumerator delayToPLay(){        
        PlayerMove.instance.moveDisable();
        Vector3 centerScreen = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,player.transform.position.z);
        Utility.PlayGfxAnimation("Text/GO",centerScreen);      
        yield return new WaitForSeconds(4f); 
        PlayerMove.instance.moveEnable(); 
        challengeStarted = true;
        InvokeRepeating("decreaseTimer", 0f, 1.0f);
    }

    IEnumerator delayCloseReward(){
        yield return new WaitForSeconds(5);
        challengeFinish();
    }
}
