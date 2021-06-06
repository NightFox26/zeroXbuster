using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    private AudioSource mixer;
    private string randomMusic = "";
    public static MusicSelector instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de MusicSelector");
            return;
        }
        instance = this;
    }
    void Start()
    {
        mixer = gameObject.transform.Find("Audio/stage musque").gameObject.GetComponent<AudioSource>();
        mixer.Stop();
    }

    public void StartBgm()
    {
        string cdTitle = PlayerPrefs.GetString("BGMselected","Aleatoir");   
        randomMusic = "";     
        if(cdTitle == "Aleatoir"){
            AudioClip musicFind = findRandomBgm();
            if(musicFind){
                mixer.clip = musicFind;
                instantiateBgmBooster(randomMusic);
            }
        }else{
            AudioClip music = Resources.Load("Audio/audioSelection/"+cdTitle) as AudioClip;
            mixer.clip = music;
            instantiateBgmBooster(cdTitle);
        }
        mixer.Play();
    }

    private AudioClip findRandomBgm(){
       int nbMusic = PlayerGainsObjects.instance.allBoughtBGMusics.Count;
       if(nbMusic>0){
           int randIndexBgm = Random.Range(0,nbMusic);
           GameObject bgmGameObject = PlayerGainsObjects.instance.allBoughtBGMusics[randIndexBgm] as GameObject;
           string bgmTitle = bgmGameObject.GetComponent<BGMusic>().title;
           randomMusic = bgmTitle;
           AudioClip music = Resources.Load("Audio/audioSelection/"+bgmTitle) as AudioClip;
           return music;
       }
       return null;
    }

    private void instantiateBgmBooster(string musicSelected){
       
        foreach (GameObject music in PlayerGainsObjects.instance.allBoughtBGMusics)
        {
            BGMusic musicBgm = music.GetComponent<BGMusic>();
            if(musicBgm.title == musicSelected){
                string boosterName = "";
                switch (musicBgm.image.name)
                {
                    case "cd titan":
                        boosterName = "MusicBoosterTitan";
                        break;
                    case "cd berserk":
                        boosterName = "MusicBoosterBerserk";
                        break;
                    case "cd5":
                        boosterName = "MusicBoosterDbz";
                        break;
                    case "cd4":
                        boosterName = "MusicBoosterNaruto";
                        break;
                    case "cd3":
                        boosterName = "MusicBoosterSeiya";
                        break;
                    case "cd gundam":
                        boosterName = "MusicBoosterGundam";
                        break;
                    case "cd1":
                        boosterName = "MusicBoosterMegaman";
                        break;
                    case "cd2":
                        boosterName = "MusicBoosterMegaman";
                        break;
                    case "cd megaman":
                        boosterName = "MusicBoosterMegaman";
                        break;
                    case "cd megaman2":
                        boosterName = "MusicBoosterMegaman";
                        break;
                    case "cd megaman3":
                        boosterName = "MusicBoosterMegaman";
                        break;
                }
                
                if(boosterName == "")
                    return;

                GameObject spawnPointPLayer = GameObject.FindGameObjectWithTag("Respawn");
                GameObject musicBooster = Resources.Load("PREFABS/itemsBGMBooster/"+boosterName) as GameObject;

                musicBooster.GetComponent<ItemBooster>().minValue1 = musicBgm.powerupIncreasing;
                musicBooster.GetComponent<ItemBooster>().maxValue1 = musicBgm.powerupIncreasing;              
                musicBooster.GetComponent<ItemBooster>().powerUp1 = (PowersUp.List) musicBgm.powerup;

                Instantiate(musicBooster, new Vector3(spawnPointPLayer.transform.position.x+5,spawnPointPLayer.transform.position.y,0),Quaternion.identity);
            }
        }
    }

}
