using System.Collections;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public float timeLifeBarVisible = 1f;
    private GameObject healthBar;
    private Transform healthBarjauge;

    private float maxScaleJauge;
    private Enemy enemy;
    private EnemyPatrol enemyPatrol;

    private void Start() {               
        enemy = GetComponent<Enemy>(); 
        enemyPatrol = GetComponent<EnemyPatrol>(); 
        healthBar = transform.Find("LifeBar").gameObject;
        healthBarjauge = transform.Find("LifeBar/health");
        maxScaleJauge =healthBarjauge.localScale.x;
        hideBar();        
    }
    public void showBar(){ 
        healthBar.SetActive(true);        
        healthBar.transform.eulerAngles = new Vector3(0, 0, 0);        
        StartCoroutine(delayShowLifeBar());
    }

    public void hideBar(){        
        healthBar.SetActive(false);
    }

    public void jaugeUpdate(float life){
        showBar();   
        if(maxScaleJauge*life/enemy.maxHealth <= 0){
            healthBarjauge.localScale = new Vector3(0,healthBarjauge.localScale.y,healthBarjauge.localScale.z);
            return;
        }     
        healthBarjauge.localScale = new Vector3(maxScaleJauge*life/enemy.maxHealth,healthBarjauge.localScale.y,healthBarjauge.localScale.z);
    }

    IEnumerator delayShowLifeBar(){
        yield return new WaitForSeconds(timeLifeBarVisible);
        hideBar();
    }
}
