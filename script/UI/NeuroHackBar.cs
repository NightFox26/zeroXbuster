using UnityEngine;
using UnityEngine.UI;

public class NeuroHackBar : MonoBehaviour
{
    private bool isActive;
    public GameObject hackBarUi;
    private Transform spawnPos;
    public float totalTimeBar = 100;
    public float amoutDecreaseBar = 20;
    private float realAmoutDecreaseBar;
    private float timeInterval = 1f;
    private float spentTime = 0;
    private float nextTime = 0;
    public float delayTimeBar = 20f;
    private int nbHackActivated = 0;
    public Slider hackBarSlider;
    public static NeuroHackBar instance;
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de NeuroHackBar");
            return;
        }
        instance = this;
        spawnPos = GameObject.FindGameObjectWithTag("neuroHackBarPos").transform;
    }

    private void Update() {
        if(isActive && !PauseMenuManager.instance.isMenuOpen){
            spentTime += Time.deltaTime;
            if (spentTime >= nextTime) {    
                hackBarSlider.value -= realAmoutDecreaseBar;
                nextTime += timeInterval; 
    
                if(hackBarSlider.value <= 0){
                    hackBarSlider.value = totalTimeBar;
                    invokeNeuroHack();
                }
            }
        }
    }

    private void invokeNeuroHack()
    {
        GameObject randNeuroHack = ItemsListing.instance.getRandomNeuroHack().gameObject;
        GameObject hackInst =  Instantiate(randNeuroHack,new Vector3(Camera.main.ScreenToWorldPoint(spawnPos.position).x,Camera.main.ScreenToWorldPoint(spawnPos.position).y,1),Quaternion.identity);
        hackInst.GetComponent<ItemNeuroHack>().startTrackingPlayer();
    }

    public void initHackBar(){
        isActive = false;
        hackBarUi.SetActive(false);
        spentTime = 0;
        nextTime = 0;
        nbHackActivated = 0;
        hackBarSlider.maxValue = totalTimeBar;        
        hackBarSlider.value = totalTimeBar;        
    }

    public void startHacking(){
        isActive = true;
        nbHackActivated ++;
        realAmoutDecreaseBar = nbHackActivated * amoutDecreaseBar;
        hackBarUi.SetActive(true);      
    }

    public void decreaseNbHack(){
        if(nbHackActivated>0){
            nbHackActivated--;
            realAmoutDecreaseBar = nbHackActivated * amoutDecreaseBar;
        }

        if(nbHackActivated == 0){
            stopHackBar();
        }
    }

    public void delayHackBar(){
        hackBarSlider.value += delayTimeBar;  
    }        

    public void stopHackBar(){
        isActive = false;
        hackBarUi.SetActive(false);
    }


}
