using UnityEngine;
using UnityEngine.UI;
public class GambleMachine : PnjQG
{
   public int cost = 150;
   public GameObject[] itemsPossibility;
   public GameObject spawnPoint;
   public float forceSpawn;
   public Vector3 spawnDirection;
   private GameObject ui;
   void Start()
   {
       ui = transform.Find("Canvas").gameObject;
       ui.transform.Find("Panel/Text").GetComponent<Text>().text = ""+cost;
   }

   private void Update() {
      if(playerOnPnj){
         showUi();
         if(Input.GetButtonDown("Fire1")){
            playGambleMachine();
         }
      }else{
         hideUi();
      }      
   }

   private void showUi()
   {
      ui.SetActive(true);
   }

   private void hideUi()
   {
      ui.SetActive(false);
   }

   private void playGambleMachine()
   {
      if(PlayerStats.instance.totalShards >= cost){
         CrystalsShardsCounter.instance.removeCrystalShardsValue(cost);
         makeSpawnItem();
      }
   }

    private void makeSpawnItem()
    {
        int itemId = Random.Range(0,itemsPossibility.Length);
        GameObject item = Instantiate(itemsPossibility[itemId],spawnPoint.transform.position,Quaternion.identity);
        item.GetComponent<Rigidbody2D>().velocity = spawnDirection;
    }

    private void OnDrawGizmos() {
      Gizmos.color = Color.blue;
      Gizmos.DrawLine(spawnPoint.transform.position,spawnPoint.transform.position + spawnDirection);
   }
}
