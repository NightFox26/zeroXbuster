using UnityEngine;

public class CrystalScript : Item
{
    private int value;
    public int value_1;
    public int value_2;
    public int value_3;
    public int value_4;
    public int rarete = 1;

    public Color color_2;
    public Color color_3;
    public Color color_4;

    public int lootChance_2 = 100;
    public int lootChance_3 = 130;
    public int lootChance_4 = 145;

    private void Start(){
        float randDrop = Random.Range(0,150);
        randDrop = (float)System.Math.Round(randDrop,1);
        randDrop += PlayerStats.instance.luck; 
         
        value = value_1;
        if(rarete == 2 || (randDrop>=lootChance_2 && randDrop<lootChance_3)){
            gameObject.GetComponent<SpriteRenderer>().color = color_2;   
            value = value_2;        
        }else if(rarete==3 || (randDrop>=lootChance_3 && randDrop<lootChance_4)){
            gameObject.GetComponent<SpriteRenderer>().color = color_3; 
            value = value_3;             
        }else if(rarete==4 || (randDrop>=lootChance_4)){
            gameObject.GetComponent<SpriteRenderer>().color = color_4;
            value = value_4; 
        }
    }

    private new void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")){
            CrystalsShardsCounter.instance.addCrystalShardsValue(value);
            
        }
        base.OnTriggerEnter2D(col);
    }
}
