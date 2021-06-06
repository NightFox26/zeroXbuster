using UnityEngine;
using UnityEngine.UI;

public class BulletsBar : MonoBehaviour
{
    public Slider slider;
    public Text textBullet;

    public void SetMaxBullets(float bullets){
        slider.maxValue = bullets;  
        updateTextBullet();     
    }

    public void SetBullets(float bullets){
        slider.value = bullets;
        updateTextBullet();
    }

    private void updateTextBullet(){
        textBullet.text = slider.value+"\n --- \n"+slider.maxValue;
    }   

}
