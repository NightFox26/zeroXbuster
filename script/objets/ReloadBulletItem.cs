using UnityEngine;

public class ReloadBulletItem : Item
{
    public int reloadValue = 100;
    private GameObject player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }   

    public new void OnTriggerEnter2D(Collider2D other) {     
        if(other.CompareTag("Player")){
            PlayerStats.instance.reloadBullets(reloadValue);

            GameObject reloadAnim = null;            

            if(reloadValue >= 100){
                reloadAnim = Instantiate(Resources.Load("PREFABS/GFX/aura/aura_reload"),new Vector3( player.transform.position.x+0.15f, player.transform.position.y-0.8f,1), Quaternion.identity) as GameObject;
                reloadAnim.GetComponent<Animator>().SetTrigger("bigReload");
            }else if(reloadValue >= 50){
                reloadAnim = Instantiate(Resources.Load("PREFABS/GFX/aura/aura_reload"),new Vector3( player.transform.position.x+0.15f, player.transform.position.y-1f,1), Quaternion.identity) as GameObject;
                reloadAnim.GetComponent<Animator>().SetTrigger("mediumReload");
            }else{
                reloadAnim = Instantiate( Resources.Load("PREFABS/GFX/aura/aura_reload"),new Vector3( player.transform.position.x+0.15f, player.transform.position.y-1f,1), Quaternion.identity) as GameObject;
                reloadAnim.GetComponent<Animator>().SetTrigger("smallReload");
            }
            reloadAnim.transform.parent = player.transform;

        }
        base.OnTriggerEnter2D(other);   
    }
}
