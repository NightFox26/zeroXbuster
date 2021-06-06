using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    public void destroyAnimation(){
        Destroy(gameObject,1f);
    }

    public void destroyInstantAnimation(){
        Destroy(gameObject);
    }

    public void disableAnimation(){
        gameObject.SetActive(false);
    }
}
