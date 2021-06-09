using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip deadSound;
    public AudioClip diveSound;
    public AudioClip rouladeSound;
    public AudioClip saber1;
    public AudioClip saber2;
    public AudioClip saber3;
    public AudioClip stealthModeSound;
    public AudioClip stealthKillSound;
    public static PlayerSounds instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }
}
