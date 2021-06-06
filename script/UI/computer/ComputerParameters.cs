using UnityEngine;

public class ComputerParameters : MonoBehaviour
{    
    public int qtMaxPurificationAllowed = 1;  
    public static ComputerParameters instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de ComputerParameters");
            return;
        }
        instance = this;       
    }  


    public void loadComputerParams(PlayerDatas datas){
        qtMaxPurificationAllowed = datas.qtMaxPurificationAllowed;
    } 
}
