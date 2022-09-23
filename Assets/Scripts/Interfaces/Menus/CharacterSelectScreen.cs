using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterSelectScreen : AbstractSelectScreen
{

    protected override void Init()
    {
        playerOrEnemy = true;
    }

    public void MainMenuScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        UnsetCharacter();
        MainMenuController.Instance.CharacterSelectScreen.SetActive(false);
        MainMenuController.Instance.MainMenuScreen.SetActive(true);
    }

    public void ChooseGameModeScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        UnsetCharacter();
        MainMenuController.Instance.CharacterSelectScreen.SetActive(false);
        MainMenuController.Instance.ChooseGameModeScreen.SetActive(true);
    }

    public void EnemySelectScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.CharacterSelectScreen.SetActive(false);
        MainMenuController.Instance.EnemySelectScreen.SetActive(true);
    }

    public void StartAdventureMode(GameObject arrow)
    {
        arrow.SetActive(false);
        MainMenuController.Instance.BuildRun();
        MainMenuController.Instance.RunTracker.StartBattle();
    }
}
