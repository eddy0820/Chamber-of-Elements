using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuInterface : ButtonInterface
{
    public void CharacterSelectScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.MainMenuScreen.SetActive(false);
        MainMenuController.Instance.CharacterSelectScreen.SetActive(true);
    }

    public void HowToPlaySrceen(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.MainMenuScreen.SetActive(false);
        MainMenuController.Instance.HowToPlayMenuScreen.SetActive(true);
    }

    public void ChooseGameModeScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.MainMenuScreen.SetActive(false);
        MainMenuController.Instance.ChooseGameModeScreen.SetActive(true);
    }

    public void ExitGame(GameObject arrow)
    {
        Debug.Log("Exited Game!");
        Application.Quit();
    }
}
