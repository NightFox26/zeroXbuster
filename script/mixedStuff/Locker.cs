using UnityEngine;
using UnityEngine.UI;

public class Locker : MonoBehaviour
{
    public pnjName.list lockerType;
    public int levelToUnlock;
    public bool lockerIsVisible = true;
    private GameObject locker;
    private void Start() {        
        showLocker();
    }
    private void Update() {
        if(isUnlocked() && locker != null && lockerIsVisible){
            Destroy(locker);
        }
    }
    private void showLocker(){
        if(!isUnlocked() && lockerIsVisible){
            GameObject lockerItem = (GameObject)Resources.Load("PREFABS/UI/locker/"+lockerType+"Locker");
            locker = Instantiate(lockerItem,transform.position,Quaternion.identity);
            locker.transform.Find("Canvas/lvl").GetComponent<Text>().text = levelToUnlock+"";
        }
    }
    public bool isUnlocked(){
        if(lockerType == pnjName.list.alia){
            if(AliaStat.instance.getLevel()>= levelToUnlock)
                return true;
        }else if(lockerType == pnjName.list.axl){
            if(AxlStat.instance.getLevel()>= levelToUnlock)
                return true;
        }else if(lockerType == pnjName.list.dynamo){
            if(DynamoStat.instance.getLevel()>= levelToUnlock)
                return true;
        }else if(lockerType == pnjName.list.iris){
            if(IrisStat.instance.getLevel()>= levelToUnlock)
                return true;
        }else if(lockerType == pnjName.list.megaman){
            if(MegamanStat.instance.getLevel()>= levelToUnlock)
                return true;
        }else if(lockerType == pnjName.list.sigma){
            if(SigmaStat.instance.getLevel()>= levelToUnlock)
                return true;
        }else if(lockerType == pnjName.list.vile){
            if(VileStat.instance.getLevel()>= levelToUnlock)
                return true;
        }
        return false;
    }
}
