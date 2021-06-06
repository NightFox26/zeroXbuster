using UnityEngine;

public class HealingItem : Item
{
    public int healingValue = 100;
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }    

    public new void OnTriggerEnter2D(Collider2D other) {     
        if(other.CompareTag("Player")){
            PlayerHealth.instance.healing(healingValue);
            GameObject healingAnim = null;            

            if(healingValue >= 100){
                healingAnim = Instantiate( Resources.Load("PREFABS/GFX/aura/aura_heal"),new Vector3( player.transform.position.x+0.15f, player.transform.position.y-2.2f,1), Quaternion.identity) as GameObject;
                healingAnim.GetComponent<Animator>().SetTrigger("bigHeal");
            }else if(healingValue >= 50){
                healingAnim = Instantiate( Resources.Load("PREFABS/GFX/aura/aura_heal"),new Vector3( player.transform.position.x+0.15f, player.transform.position.y-1.7f,1), Quaternion.identity) as GameObject;
                healingAnim.GetComponent<Animator>().SetTrigger("mediumHeal");
            }else{
                healingAnim = Instantiate( Resources.Load("PREFABS/GFX/aura/aura_heal"),new Vector3( player.transform.position.x+0.15f, player.transform.position.y-1.5f,1), Quaternion.identity) as GameObject;
                healingAnim.GetComponent<Animator>().SetTrigger("smallHeal");
            }
            healingAnim.transform.parent = player.transform;
        }
        base.OnTriggerEnter2D(other);   
    }
}
