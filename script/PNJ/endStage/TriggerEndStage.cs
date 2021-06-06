using UnityEngine;

public class TriggerEndStage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            MegamanEndStage.instance.checkCondition();
            SigmaEndStage.instance.checkCondition();
            AliaEndStage.instance.checkCondition();
        }
    }
}
