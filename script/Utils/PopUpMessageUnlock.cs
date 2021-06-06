using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PopUpMessageUnlock
{
    public Sprite icon;

    [TextArea(1,2)]
    public string text;
    private Animator popupUiAnimator;
    private Image popupIconUi;
    private Text popupTextUi;

    public void displayPopUp(){
        popupUiAnimator = GameObject.Find("HUD/PanelPopupMessage").GetComponent<Animator>();
        popupIconUi = GameObject.Find("HUD/PanelPopupMessage/Image").GetComponent<Image>();
        popupTextUi = GameObject.Find("HUD/PanelPopupMessage/Text").GetComponent<Text>();
        popupIconUi.sprite = icon;
        popupTextUi.text = text;
        popupUiAnimator.Play("popupMessage");
    }
}
