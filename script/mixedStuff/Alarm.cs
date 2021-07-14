using UnityEngine;

public class Alarm : MonoBehaviour
{
    public float speed = 1;
    private bool alarmOn = false;
    private Animator anim;
    private Rigidbody2D rb;
    public AudioClip alarmSound;
    public DomeAlarm DomePref;

    private void Start() {
        anim = transform.parent.gameObject.GetComponent<Animator>();
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(!alarmOn)
            transform.Rotate(new Vector3(0,0,speed));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !PlayerHealth.instance.isInvincible && !alarmOn){
            alarmOn = true;
            AudioManager.Instance.PlayInfosEffectVoice(alarmSound);            
            rb.bodyType = RigidbodyType2D.Static;
            PlayerHealth.instance.isNotStealth();

            try
            {
                EnemyPatrol patrol = transform.parent.gameObject.GetComponent<EnemyPatrol>();
                patrol.enabled = false;
            }
            catch (System.Exception){ Debug.LogWarning("pas de EnemyPatrol dans cette enemie avec alarm");}
            
            anim.Play("alarmOn");
            Instantiate(DomePref,transform.position+(Vector3.up*4),Quaternion.identity);
            Destroy(transform.parent.gameObject,1.7f);
        }
    }
}
