using UnityEngine;

public class DarkSpawner : MonoBehaviour
{
    public GameObject boss;
    public GameObject showGameObject;
    public GameObject hideGameObject;
    private Transform spawnPos;
    private Animator animator;

    private void Start() {
        spawnPos = transform.Find("bossSpawnPos");
        animator = GetComponent<Animator>();
        animator.SetBool("isHidingAnimation",false);

        if(hideGameObject != null){
            animator.SetBool("isHidingAnimation",true);
        }
    }

    public void instantiateBoss(){
        Instantiate(boss,spawnPos.position,Quaternion.identity);
    }

    public void showBoss(){
        showGameObject.SetActive(true);
    }

    public void hideBoss(){
        hideGameObject.SetActive(false);
    }
}
