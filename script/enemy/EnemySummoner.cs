using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    private Transform spawnPosition;
    
    void Start()
    {
        spawnPosition = transform.Find("bullet0Pos");
    }

    public void summonEnemie(int i){
        Instantiate(enemiesToSpawn[i],spawnPosition.position,Quaternion.identity);
        GetComponent<EnemyShooter>().isShooting = false;
        GetComponent<EnemyShooter>().animator.Play("idle");
    }

}
