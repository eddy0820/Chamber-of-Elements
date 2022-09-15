using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EnemySelectScreen : AbstractSelectScreen
{
    protected override void Init()
    {
        playerOrEnemy = false;
    }
    public void CharacterSelectScreen(GameObject arrow)
    {
        arrow.SetActive(false);
        UnsetCharacter();
        MainMenuController.Instance.EnemySelectScreen.SetActive(false);
        MainMenuController.Instance.CharacterSelectScreen.SetActive(true);
    }

    public void GoToBattle(GameObject arrow)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuController.Instance.BattleScene);
    }
}
