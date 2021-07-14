using UnityEngine;
using System.Collections.Generic;
public class ItemNeuroHack : Item
{
    private string itemName;
    public PowersUp.List powerUp1;
    public int diviseur = 4;
    private float value1 = 0;
    private bool startSmoothDamp;
    private Transform player;
    private float smoothTime = 0.4f;
    private Animator animator;

    void Start()
    {
        itemName = gameObject.name;  
        GameObject colorItem = transform.Find("cadreColor").gameObject;
        GameObject lightItem = transform.Find("light").gameObject;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        colorItem.GetComponent<SpriteRenderer>().color = ItemColor.red();
        lightItem.GetComponent<Light>().color = ItemColor.red();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        if(startSmoothDamp){ 
            transform.position = Vector3.MoveTowards(transform.position, player.position, smoothTime);

            if(transform.position.x <= player.position.x + 0.1f &&
            transform.position.x >= player.position.x - 0.1f &&
            transform.position.y <= player.position.y + 0.1f && 
            transform.position.y >= player.position.y - 0.1f
            ){ 
                hackPlayer();
                Destroy(gameObject);
            }
        }
    }

    public void startTrackingPlayer(){
        startSmoothDamp = true;
    }

    public void hackPlayer(){
        startSmoothDamp = false;
        value1 = calcValue(powerUp1);
        Dictionary<string,object> itemDatas = new Dictionary<string,object>();
        itemDatas = serialisationDataItem();

        PlayerGainsObjects.instance.allNeuroHacks.Add(itemDatas);
        PlayerStats.instance.increaseStats("neuroHack",powerUp1,value1);        
    }

    public void destroyHack(){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(0,60),ForceMode2D.Impulse);        
        Invoke("animDestroyHack",0.5f);
    }

    private void animDestroyHack(){
        Utility.PlayGfxAnimation("electric/electric_body",transform.position);
        animator.SetTrigger("destroy");
    }

    public void destroyGameObjectHack(){
        Destroy(gameObject);
    }

    private float calcValue(PowersUp.List stat){
        float value = 0;
        switch (stat){
            case PowersUp.List.VieMax:
                value = (PlayerStats.instance.maxHealth - PlayerStats.instance.maxHealthEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.BulletQt:
                value = (PlayerStats.instance.maxBulletQuantity - PlayerStats.instance.maxBulletQuantityEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.Chance:
                value = (PlayerStats.instance.luck - PlayerStats.instance.luckEquiped - PlayerCombo.instance.comboCounter)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.Dmg:
                value = (PlayerStats.instance.damage - PlayerStats.instance.damageEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.DmgTir:
                value = (PlayerStats.instance.bulletDamage - PlayerStats.instance.bulletDamageEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.Defence:
                value = (PlayerStats.instance.defence - PlayerStats.instance.defenceEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.Accel:
                value = (PlayerStats.instance.velocity - PlayerStats.instance.velocityEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.AccelTir:  
                value = (PlayerStats.instance.fireRate - PlayerStats.instance.fireRateEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.HtSaut:
                value = (PlayerStats.instance.jumpPower - PlayerStats.instance.jumpPowerEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.Dash:
                value = (PlayerStats.instance.dashVelocity - PlayerStats.instance.dashVelocityEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.criticalDmg:
                value = (PlayerStats.instance.criticalDmg - PlayerStats.instance.criticalDmgEquiped)/diviseur;
                return -Mathf.Round(value);
            case PowersUp.List.criticalFreguency:
                value = (PlayerStats.instance.criticalFreguency - PlayerStats.instance.criticalFreguencyEquiped)/diviseur;
                return -Mathf.Round(value);
        }
        return value;
    }


    private Dictionary<string,object> serialisationDataItem(){
        Dictionary<string,object> datas = new Dictionary<string, object>();
        datas.Add("itemName",itemName.Replace("(Clone)",""));
        datas.Add("powerUp1",powerUp1);
        datas.Add("value1",value1);
        datas.Add("price",price);
        return datas;
    }
}
