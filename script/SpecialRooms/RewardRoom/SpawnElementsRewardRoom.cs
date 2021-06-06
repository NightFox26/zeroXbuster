using UnityEngine;
using System.Collections;

public class SpawnElementsRewardRoom : MonoBehaviour
{
    public GameObject chest;
    public GameObject shop;
    public GameObject blackMarket;
    public GameObject challenge;
    public GameObject gift;
    public GameObject completedGift;

    private void Start() {
        chest.SetActive(false);
        shop.SetActive(false);
        blackMarket.SetActive(false);
        challenge.SetActive(false);
        gift.SetActive(false);
        completedGift.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            checkGlyphObtained();
            Destroy(gameObject,0.5f);
        }
    }

    private void checkGlyphObtained(){
        if(StageParameters.instance.glyphObtained.Contains("Chest")){
            foreach (Transform childChest in chest.transform){
                Utility.PlayGfxAnimation("spawn/magicalSpawn",childChest.transform.position + new Vector3(0,1.5f,0));
            }
            StartCoroutine(delayToActivate(chest));
        }

        if(StageParameters.instance.glyphObtained.Contains("ChallengeRoom")){
            Utility.PlayGfxAnimation("spawn/magicalSpawn",challenge.transform.Find("tpDrStrange").position);           
            StartCoroutine(delayToActivate(challenge));
        }

        if(StageParameters.instance.glyphObtained.Contains("Shop")){
            Utility.PlayGfxAnimation("spawn/magicalSpawn",shop.transform.Find("rico").position);           
            StartCoroutine(delayToActivate(shop));
        }

        if(StageParameters.instance.glyphObtained.Contains("BlackMarket")){
            Utility.PlayGfxAnimation("spawn/magicalSpawn",blackMarket.transform.Find("Vile").position);           
            StartCoroutine(delayToActivate(blackMarket));
        }

        if(StageParameters.instance.glyphObtained.Contains("SecretGift")){
            gift.SetActive(true);
        }

        if(StageParameters.instance.glyphObtained.Count >= System.Enum.GetNames(typeof(GlyphType.List)).Length){
            foreach (Transform childChest in completedGift.transform){
                Utility.PlayGfxAnimation("spawn/magicalSpawn",childChest.transform.position);
            }        
            StartCoroutine(delayToActivate(completedGift));
        }
    }

    IEnumerator delayToActivate(GameObject obj){
        yield return new WaitForSeconds(0.2f);
        obj.SetActive(true);
    }
}
