using UnityEngine;
using System.Collections;

[System.Serializable]
public class DomeAlarm : MonoBehaviour
{
    public float timeDome = 30;
    private float widthSpawnBox = 10;
    private float heightSpawnBox = 4;
    private float minXzone;
    private float maxXzone;
    private float minYzone;
    private float maxYzone;
    private GameObject[] enemiesPossibility;
    private int nbEnemiesSpawn;
    private Animator animator;
    private void Start() {
        animator = GetComponent<Animator>();
        minXzone = transform.position.x-(widthSpawnBox/2);
        maxXzone = transform.position.x+(widthSpawnBox/2);
        minYzone = transform.position.y-(heightSpawnBox/2)-2;
        maxYzone = transform.position.y+(heightSpawnBox/2)-2;
        makeSpawnEnemies();
        Invoke("playAnimationDestroy",timeDome);
    }

    private void makeSpawnEnemies(){
        randNbEnemies();
        for (int i = 0; i < nbEnemiesSpawn; i++){
            Invoke("instantiateRandMob",1.5f+i);
        }
    }

    private void randNbEnemies(){
        if(StageParameters.instance.stageDifficulty == "" || StageParameters.instance.stageDifficulty == "easy"){
            nbEnemiesSpawn = Random.Range(1,LevelConfig.instance.nbSpawnEasy+1); 
            enemiesPossibility = LevelConfig.instance.enmiesEasy;     
        }else if(StageParameters.instance.stageDifficulty == "normal"){
            nbEnemiesSpawn = Random.Range(1,LevelConfig.instance.nbSpawnNormal+1);  
            enemiesPossibility = LevelConfig.instance.enmiesNormal;          
        }else if(StageParameters.instance.stageDifficulty == "hard"){
            nbEnemiesSpawn = Random.Range(1,LevelConfig.instance.nbSpawnHard+1); 
            enemiesPossibility = LevelConfig.instance.enmiesHard;           
        }
    }

    private void instantiateRandMob(){        
        int mobRand = Random.Range(0,enemiesPossibility.Length);
        float spawnX = Random.Range(minXzone,maxXzone);
        float spawnY = Random.Range(minYzone,maxYzone);
        GameObject radarAnim = Instantiate(LevelConfig.instance.radarAnimation,new Vector3(spawnX,spawnY-0.5f,-1),Quaternion.identity);      
        StartCoroutine(makeSpawnEnemyDelay(enemiesPossibility[mobRand],radarAnim,new Vector3(spawnX,spawnY,0)));
       
    }

    IEnumerator makeSpawnEnemyDelay(GameObject enemy, GameObject radarAnim, Vector3 position){
        yield return new WaitForSeconds(1.5f);
        Instantiate(enemy,position,Quaternion.identity);
        Destroy(radarAnim);
    }

    private void playAnimationDestroy(){
        animator.SetTrigger("destroy");
    }

    public void destroyDome(){
        Destroy(gameObject);
    }
}
