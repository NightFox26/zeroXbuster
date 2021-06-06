using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class chestManager : MonoBehaviour
{
    public Item[] loots;
    public int maxHitsToOpen = 4;
    public bool randLoot = true;
    public int lootQuantity = 2;
    private int hits = 0;
    public bool isGoldenChest = false;
    public int costToOpen = 0;
    private Animator animator;
    private const float TIME_FRAME_ANIM = 0.1f;
    private const float SHAKE_FRAME_ANIM = 0.1f;
    private GameObject costPanel;

    private void Start() {
        if(isGoldenChest || costToOpen > 0){
            costPanel = transform.Find("Canvas/CostPanel").gameObject;
            costPanel.SetActive(false);
            loots = ItemsListing.instance.getBoosterList();
            if(costToOpen > 0){
                costPanel.SetActive(true);
                costPanel.transform.Find("cost").GetComponent<Text>().text = ""+costToOpen;
            }
        }else{
            animator = GetComponent<Animator>();
        }

    }

    public void destroyChest(){ 
        Instantiate(Resources.Load("Prefabs/GFX/hit/hitDestroy"),transform.position,Quaternion.identity);       
        lootChest();
        Destroy(gameObject);
    }

    private void lootChest(){
        if(randLoot){
            while(lootQuantity >0){
                int randId = Random.Range(0,loots.Length);
                lootQuantity--;
                Instantiate(loots[randId],gameObject.transform.position,Quaternion.identity);
            }            
        }else{

        }
    }

    IEnumerator shakeAnim(){
        transform.position = new Vector3(transform.position.x+SHAKE_FRAME_ANIM,transform.position.y,transform.position.z);
        yield return new WaitForSeconds(TIME_FRAME_ANIM);
        transform.position = new Vector3(transform.position.x-SHAKE_FRAME_ANIM,transform.position.y,transform.position.z);
        yield return new WaitForSeconds(TIME_FRAME_ANIM);
        transform.position = new Vector3(transform.position.x+SHAKE_FRAME_ANIM,transform.position.y,transform.position.z);
        yield return new WaitForSeconds(TIME_FRAME_ANIM);        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("sword") || other.CompareTag("bullet")){
            hits++;
            Instantiate(Resources.Load("Prefabs/GFX/saber/SaberStrike1"),transform.position,Quaternion.identity);  

            if(costToOpen > 0 && PlayerStats.instance.totalShards >= costToOpen){
                CrystalsShardsCounter.instance.removeCrystalShardsValue(costToOpen);
                destroyChest();
            }else if(costToOpen == 0){
                if(hits >= maxHitsToOpen){
                    if(isGoldenChest){
                        destroyChest();
                    }else{                
                        animator.SetBool("destroyed",true);
                    }
                }else{
                    if(isGoldenChest){
                        StartCoroutine(shakeAnim());
                    }else{
                        animator.SetTrigger("shake");
                    }
                }
            }
        }
    }
}
