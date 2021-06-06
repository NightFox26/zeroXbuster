using UnityEngine;
using UnityEngine.UI;

public class CrystalsShardsCounter : MonoBehaviour
{    
    public Text crystalsShardsCounter;
    public static CrystalsShardsCounter instance;   

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de CrystalsShardsCounter");
            return;
        }
        instance = this;
    }
    public void addCrystalShardsValue(int value){     
        GetComponent<AudioSource>().Play();   
        PlayerStats.instance.totalShards += value;
        crystalsShardsCounter.text = ""+PlayerStats.instance.totalShards;
    }

    public void removeCrystalShardsValue(int value){
        PlayerStats.instance.totalShards -= value;
        crystalsShardsCounter.text = ""+PlayerStats.instance.totalShards;
    }

    public void setCrystalShardsValue(int value){
        PlayerStats.instance.totalShards = value;
        crystalsShardsCounter.text = ""+PlayerStats.instance.totalShards;
    }
}
