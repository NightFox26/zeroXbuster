using UnityEngine;
using UnityEngine.UI;

public class SpherierPowerUp : MonoBehaviour
{
    private int indexSphere = 0;
    public string sphereType = "atk";
    public int quantityOfNeeded = 0;    
    public int maxNb = 5;  

    [HideInInspector]  
    public int nbObtain = 0;
    private int lastNbObtain;    
    public PowersUp.List powerup;
    public float powerupIncreasing = 1;

    void Start()
    {
        //updateInfosSphere();       
        setPowerUpAvailable(false);
    }

    public void updateInfosSphere(int i){
        gameObject.transform.Find("Image/Text").GetComponent<Text>().text = powerup.ToString() + " \n+"+powerupIncreasing;
        gameObject.transform.Find("Text").GetComponent<Text>().text = nbObtain + "/"+maxNb;
        lastNbObtain = nbObtain;
        indexSphere = i;
    }

    private void Update() {        
        if(getAllPtsPanel() >= quantityOfNeeded){
            setPowerUpAvailable(true);
        }

        if(nbObtain != lastNbObtain){
            gameObject.transform.Find("Text").GetComponent<Text>().text = nbObtain + "/"+maxNb;
            lastNbObtain = nbObtain;
        }

        if(nbObtain >= maxNb){
            gameObject.transform.Find("Text").GetComponent<Text>().text = maxNb + "/"+maxNb;
            gameObject.transform.Find("Text").GetComponent<Text>().color = Color.green;
        }
    }

    public void clickUpgrade(){
        if(nbObtain < maxNb && PlayerStats.instance.nbHunterPts > 0){
            nbObtain++;
            PlayerStats.instance.nbHunterPts--;
            PlayerStats.instance.increaseStats("global", powerup, powerupIncreasing);
            increasePtsPanel();    

            if(sphereType == "atk"){
                PlayerGainsObjects.instance.allSphereAtkPanel[indexSphere] = nbObtain;
            }else if(sphereType == "dext"){
                PlayerGainsObjects.instance.allSphereDextPanel[indexSphere] = nbObtain;
            }else if(sphereType == "surv"){
                PlayerGainsObjects.instance.allSphereSurvPanel[indexSphere] = nbObtain;
            }  

            SaveSystem.saveAllDatas();     
        }
    }

    private void setPowerUpAvailable(bool b){
        gameObject.transform.Find("Image").GetComponent<Button>().interactable = b;
    }

    private int getAllPtsPanel(){        
        return transform.parent.gameObject.GetComponent<SpherierPanel>().allPtsGained;
    }

    private void increasePtsPanel(){        
        transform.parent.gameObject.GetComponent<SpherierPanel>().allPtsGained++;
    }

}
