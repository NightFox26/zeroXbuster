using UnityEngine;

public class DarkSpawner : MonoBehaviour
{
    public GameObject boss;
    public GameObject showInactiveboss;
    private Transform spawnPos;

    private void Start() {
        spawnPos = transform.Find("bossSpawnPos");
    }

    public void instantiateBoss(){
        Instantiate(boss,spawnPos.position,Quaternion.identity);
    }

    public void showBoss(){
        showInactiveboss.SetActive(true);
    }
}
