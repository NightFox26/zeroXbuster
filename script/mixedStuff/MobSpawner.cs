﻿using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour
{
    public EnemieType.List type;
    public GameObject[] enemies; 
    public int maxSpawnEnemies = 4;
    public bool enemyCanBeNull = false;

    public bool isBoxDetecting = true;
    public bool isDiscreteSpawning = false;
    public bool randomPositionSpawn = true;
    public float widthDetection = 26;
    public float heightDetection = 10;

    private BoxCollider2D spawnZone;
    private float minXzone;
    private float maxXzone;
    private float minYzone;
    private float maxYzone;
    private int randNbToSpawn = -1;
    private int spawnedEnemies = 0;

    private void Start() {
        spawnZone = gameObject.GetComponent<BoxCollider2D>();
        spawnZone.size = new Vector2(widthDetection, heightDetection);
        minXzone = spawnZone.transform.position.x-(spawnZone.size.x/2);
        maxXzone = spawnZone.transform.position.x+(spawnZone.size.x/2);
        minYzone = spawnZone.transform.position.y-(spawnZone.size.y/2)+1;
        maxYzone = spawnZone.transform.position.y+(spawnZone.size.y/2);

        if(enemies.Length == 0){
            if(StageParameters.instance.stageDifficulty == "easy"){
                getEnemiesPoolByDifficulty(LevelConfig.instance.enmiesEasy);
            }else if(StageParameters.instance.stageDifficulty == "normal"){
                getEnemiesPoolByDifficulty(LevelConfig.instance.enmiesNormal);
            }else if(StageParameters.instance.stageDifficulty == "hard"){
                getEnemiesPoolByDifficulty(LevelConfig.instance.enmiesHard);
            }else{
                getEnemiesPoolByDifficulty(LevelConfig.instance.enmiesEasy);
            }
        }

        if(!isBoxDetecting){
            launchMobSpawning();
        }
    }    
    private void Update() {
        if(randNbToSpawn == 0){
            Destroy(gameObject,2f);
        }

        if(randNbToSpawn>0){
            if(spawnedEnemies >= randNbToSpawn){
                Destroy(gameObject,2f);
            }
        }
    }

    private void getEnemiesPoolByDifficulty(GameObject[] enemiesPool){
        if(enemiesPool.Length > 0){
            enemies = enemiesPool;
        }else{
            Debug.LogWarning("La pool n'enemies n'est pas configuré pour ce niveau de difficulté ... dans LevelConfig");
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            if(randNbToSpawn>0) return;

            launchMobSpawning(); 
        }
    }

    private void launchMobSpawning(){
        int minQtMob = 1;
        if(enemyCanBeNull) minQtMob = 0;

        randNbToSpawn = Random.Range(minQtMob,maxSpawnEnemies+1);   
        for (int i = 0; i < randNbToSpawn; i++)
        {
            Invoke("instantiateRandMob",1.5f+i);
        }
    }

    private void instantiateRandMob(){
        int mobRand = Random.Range(0,enemies.Length);
        if(enemies.Length == 0) return;

        GameObject radarAnim;
        if(randomPositionSpawn){
            float spawnX = Random.Range(minXzone,maxXzone);
            float spawnY = Random.Range(minYzone,maxYzone);
            radarAnim = Instantiate(LevelConfig.instance.radarAnimation,new Vector3(spawnX,spawnY-0.5f,-1),Quaternion.identity);      
            StartCoroutine(makeSpawnEnemyDelay(enemies[mobRand],radarAnim,new Vector3(spawnX,spawnY,0)));
        }else{            
            radarAnim = Instantiate(LevelConfig.instance.radarAnimation,new Vector3(transform.position.x,transform.position.y-0.5f,-1),Quaternion.identity);                      
            StartCoroutine(makeSpawnEnemyDelay(enemies[mobRand],radarAnim,transform.position));
        }

        if(isDiscreteSpawning){
            radarAnim.GetComponent<AudioSource>().enabled = false;
            Destroy(radarAnim);
        }
    }


    IEnumerator makeSpawnEnemyDelay(GameObject enemy, GameObject radarAnim, Vector3 position){
        if(isDiscreteSpawning){
            yield return new WaitForSeconds(0.5f);
        }else{
            yield return new WaitForSeconds(1.5f);
        }
        Instantiate(enemy,position,Quaternion.identity);
        Destroy(radarAnim);
        spawnedEnemies++;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(widthDetection,heightDetection,1));
    }
}
