using UnityEngine;

public class DarkPortalTpJenova : MonoBehaviour
{
    public GameObject jenovaPref;
    public AudioSource bgm;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            LevelConfig.instance.stopBgmStage();
            bgm.Play();
            GameObject failleGameObj = Instantiate(jenovaPref,LevelConfig.instance.failleSpawnPos.position,Quaternion.identity);
            Vector3 playerSpawnInFaille = failleGameObj.transform.Find("spawnPlayerFaille").gameObject.transform.position;
            PlayerMove.instance.facingRight = false;
            TeleportScript.instance.teleportOnaSpot(playerSpawnInFaille);
        }
    }
}
