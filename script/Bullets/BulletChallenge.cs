using UnityEngine;

public class BulletChallenge : Bullet
{
    [HideInInspector]
    public ChallengeRoom challengeConfig;    

    private void Update() {
        if(challengeConfig.challengeWin){
            Destroy(gameObject); 
        }
    }
    private void OnTriggerEnter2D(Collider2D other) { 
        if(other.CompareTag("Player") && PlayerHealth.instance.isPlayerCanTakeDamge()){       
            playerIsHit(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other) {  
        if(other.CompareTag("Player") && PlayerHealth.instance.isPlayerCanTakeDamge()){      
            playerIsHit(other); 
            base.explodeBullet(other.transform.position);
        }
    }

    private void playerIsHit(GameObject other){ 
        PlayerHealth.instance.takeDamage(0); 
        challengeConfig.lifePlayer--;
        ChallengeUI.instance.updateUiLife(challengeConfig.lifePlayer);              
    }
}
