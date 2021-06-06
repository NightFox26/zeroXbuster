using System.Collections;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{   
    public static PopupMessage instance;

    private void Awake() {
        if(instance != null){
            Debug.LogWarning("il y a deja une instance de popupMessage");
            return;
        }
        instance = this;
    }

    public void showPopup(GameObject popupPanel){        
        popupPanel.SetActive(true);
        StartCoroutine(hidePopupDelayed(popupPanel));
    }

    IEnumerator hidePopupDelayed(GameObject popupPanel){
        yield return new WaitForSeconds(2);
        popupPanel.SetActive(false);
    }


}
