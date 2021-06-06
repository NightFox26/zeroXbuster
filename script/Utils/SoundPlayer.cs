using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource sound;    
    public AudioSource[] soundList;
    public static SoundPlayer instance;    

    private void Awake() {
        instance = this; 
    }

    public void playSound(){
        sound.Play();
    } 

    public void playSoundFromList(int i){
        soundList[i].Play();
    }  

    //fonctionne mais a voir...... 

    // public static void playSoundOfObj(GameObject item){        
    //     GameObject obj = Instantiate(item, PlayerMove.instance.transform.position + new Vector3(10,10,0), Quaternion.identity);
    //     obj.GetComponent<SoundPlayer>().playSound();        
    // } 

}