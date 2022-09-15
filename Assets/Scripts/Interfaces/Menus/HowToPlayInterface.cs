using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayInterface : ButtonInterface
{
    public void MainMenuScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.HowToPlayMenuScreen.SetActive(false);
        MainMenuController.Instance.MainMenuScreen.SetActive(true);
    }
}
