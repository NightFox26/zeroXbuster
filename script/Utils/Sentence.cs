using UnityEngine;

[System.Serializable]
public class Sentence
{
    public string persoName;
    public Sprite face;
    public bool postionTop;

    [TextArea(1,2)]
    public string message;
    public GameObject setActiveGameObject;
    public GameObject setInactiveGameObject;
    public AudioSource stopingBgm;
    public AudioSource playAudio;
}
