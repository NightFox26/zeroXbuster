
using UnityEngine;
using UnityEngine.UI;

public class LockerButton : MonoBehaviour
{
    public pnjName.list lockerType;
    public int levelToUnlock;
    private Button btn;

    private void Start() {
        btn = GetComponent<Button>();
        if(!isUnlocked()){
            btn.interactable = false;
            btn.transform.Find("Text").GetComponent<Text>().text = "?????";


            Sprite lockerSprite = Resources.Load<Sprite>("PNJ/lockerSystem/"+lockerType+"Locked");
            btn.transform.Find("ImageLocker").GetComponent<Image>().sprite = lockerSprite;
            btn.transform.Find("ImageLocker/lvlLock").GetComponent<Text>().text = levelToUnlock.ToString();
        }else{
            btn.transform.Find("ImageLocker").gameObject.SetActive(false);
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
