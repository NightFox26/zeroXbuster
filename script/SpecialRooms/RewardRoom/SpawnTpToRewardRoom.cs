using UnityEngine;
using System.Collections;

public class SpawnTpToRewardRoom : MonoBehaviour
{
    public GameObject elementToActivate;

    private void Start() {
        elementToActivate.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            NeuroHackBar.instance.stopHackBar();
            if(StageParameters.instance.glyphObtained.Count>0){                
                Utility.PlayGfxAnimation("spawn/magicalSpawn",elementToActivate.transform.position + new Vector3(0,1.5f,0));
                StartCoroutine(delayToActivate());
            }
        }
    }

    IEnumerator delayToActivate(){
        yield return new WaitForSeconds(0.2f);
        elementToActivate.SetActive(true);
    }
}
