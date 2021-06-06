using UnityEngine;
using TMPro;
using System.Collections;

public class Block : MonoBehaviour
{
    public int damagePercent = 20;
    private int nbHitsNeeded;
    private bool canHit = true;
    private TextMeshProUGUI compteurText;
    private void Start() {
        nbHitsNeeded = Random.Range(2,5);
        compteurText = transform.Find("Canvas/compteurTxt").gameObject.GetComponent<TextMeshProUGUI>();
        compteurText.text = nbHitsNeeded.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("sword") && canHit){
            canHit = false;
            PlayerHealth.instance.takeDamage(PlayerStats.instance.maxHealth*damagePercent/100);
            nbHitsNeeded--;
            compteurText.text = nbHitsNeeded.ToString();
            compteurText.enabled = true;
            StartCoroutine(delayToHit());
        }

        if(nbHitsNeeded<=0){
            Destroy(gameObject);
        }
    }

    IEnumerator delayToHit(){
        yield return new WaitForSeconds(0.8f);
        canHit = true;
    }
}
