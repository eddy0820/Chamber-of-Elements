using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashScreenBehavior : MonoBehaviour
{
    public static FlashScreenBehavior Instance {get; private set; }

    CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GameManager.Instance.FlashCanvas.GetComponent<CanvasGroup>();
    }

    public void DoFlash()
    {
        canvasGroup.alpha = 1;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha = canvasGroup.alpha - Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        
        yield break;
    }   
}
