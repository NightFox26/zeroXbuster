using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text textLife;

    public void setMaxHealth(float health){
        slider.maxValue = health; 
        updateTextLife();
    }

    public void setHealth(float health){
        slider.value = health;
        updateTextLife();
    }

    private void updateTextLife(){
        textLife.text = slider.value+"\n --- \n"+slider.maxValue;
    }

}
