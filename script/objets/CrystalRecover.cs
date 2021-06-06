using UnityEngine;

public class CrystalRecover : MonoBehaviour
{
    [HideInInspector]
    public int crystalQt;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            CrystalsShardsCounter.instance.addCrystalShardsValue(crystalQt);
            GameoverManager.instance.resetLostCrystals();
            Destroy(gameObject);
        }
    }
}
