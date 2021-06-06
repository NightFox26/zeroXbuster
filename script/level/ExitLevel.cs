using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private bool needToValidate;
    private bool panelRanking;

    private void Start() {
        needToValidate = true;
        panelRanking = false;
        if(StageParameters.instance.isBossLevel){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {       
        if(other.CompareTag("Player")){ 
            if(LevelConfig.instance.isLevelWithRanking){
                RankingPanel.instance.showRankingPanel();  
                panelRanking = true; 
            }else{
                PlayerMove.instance.playTeleportationAnimation();
                RunCompletion.instance.goToLevelSelection();
            }  
        }
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1") && needToValidate && panelRanking){           
            needToValidate = false;
            panelRanking = false; 
            PlayerMove.instance.playTeleportationAnimation();
            RunCompletion.instance.goToLevelSelection();
        }
    }

    


}
