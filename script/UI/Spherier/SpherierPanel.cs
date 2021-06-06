using UnityEngine;

public class SpherierPanel : MonoBehaviour
{
    public GameObject[] allSpheres;

    [HideInInspector]
    public int allPtsGained = 0;

    public void loadAllUpgradedSphere(string type) {                    
        setNbPts(SaveSystem.loadSpherier(type));       
    }

    private void setNbPts(int[] values){  
        allPtsGained = 0;      
        for (int i = 0; i < values.Length; i++)
        {
            if(i < allSpheres.Length){
                allSpheres[i].GetComponent<SpherierPowerUp>().nbObtain = values[i]; 
                allSpheres[i].GetComponent<SpherierPowerUp>().updateInfosSphere(i);
                allPtsGained += values[i];
            }
        }

    }

}
