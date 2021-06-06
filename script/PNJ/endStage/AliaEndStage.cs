using UnityEngine;

public class AliaEndStage : PnjEndStage
{
    public static AliaEndStage instance;
    public Item[] itemsPossiblity;
    public int[] itemsLootRate;
    private bool itemSpawned;
    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de AliaEndStage");
            return;
        }
        instance = this;
    }

    new void Start()
    {
        base.Start();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !itemSpawned){
            float rand = Random.Range(0,100);                      
            rand -= PlayerStats.instance.luck;
            for (int i = itemsPossiblity.Length - 1; i >= 0; i--)
            {
                if(rand < itemsLootRate[i]){
                    GameObject item = Instantiate(itemsPossiblity[i],lootPos.position,Quaternion.identity).gameObject;
                    item.name = itemsPossiblity[i].name;
                    itemSpawned = true;
                    break;
                }
            }
        }
    }

}
