using UnityEngine;
using System.IO;
using System;

public class DarkPortalTp : MonoBehaviour
{
    private GameObject faillePref;
    public AudioSource bgm;
    private BoxCollider2D col;
    private Animator animator;
    private GameObject failleGameObj;

    void Start()
    {
        int nbFailles = countPrefabFailles();
        int idFaille = UnityEngine.Random.Range(1,nbFailles+1);
        faillePref = (GameObject)Resources.Load("PREFABS/rooms/failles/faille"+idFaille);
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            failleGameObj = Instantiate(faillePref,LevelConfig.instance.failleSpawnPos.position,Quaternion.identity);
            FailleConfig.instance.darkPortal = gameObject; 
            Vector3 playerSpawnInFaille = failleGameObj.transform.Find("spawnPlayerFaille").gameObject.transform.position;
            TeleportScript.instance.teleportOnaSpot(playerSpawnInFaille);
            Invoke("startFaille",0.5f);
        }
    }

    private void startFaille(){
        FailleConfig.instance.initFaille();
        playBgm();
    }

    public void destroyPortal(){
        resetFaille();
        Invoke("launchDestroyAnim",2f);
    }

    public void exitedFaille(){
        resetFaille();
        Invoke("restoreAccessToFaille",2.5f);
    }

    private void resetFaille(){
        stopBgm();       
        col.enabled = false;
        Destroy(failleGameObj,0.5f);
    }

    public void playBgm(){
        bgm.Play();
    }

    public void stopBgm(){
        bgm.Stop();
    }

    private void launchDestroyAnim(){
        animator.SetTrigger("destroy");
    }

    private void restoreAccessToFaille(){
        col.enabled = true;
    }

    public void destroyObjectPortal(){
        Destroy(gameObject);
        NeuroHackBar.instance.decreaseNbHack();
    }

    private int countPrefabFailles(){
        string AssetsFolderPath = Application.dataPath;
        string levelFolder = AssetsFolderPath + "/Resources/PREFABS/rooms/failles";
        DirectoryInfo dir = new DirectoryInfo(levelFolder);
        FileInfo[] info = dir.GetFiles("*.prefab");
        int fileCount = info.Length;
        Array.Clear(info, 0, info.Length);
        return fileCount;
    }

}
