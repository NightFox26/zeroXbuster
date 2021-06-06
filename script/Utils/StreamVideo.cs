using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {
     public RawImage rawImage;
     public VideoPlayer videoPlayer;
     void Start () {
          StartCoroutine(PlayVideo());
     }
     IEnumerator PlayVideo()
     {
          videoPlayer.Prepare();
          WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(1);          
          while (!videoPlayer.isPrepared)
          {
               yield return waitForSeconds;
               break;
          }
          rawImage.texture = videoPlayer.texture;
          videoPlayer.Play();    
     }


     public void runVideo(){
          StartCoroutine(PlayVideo());
     }
}