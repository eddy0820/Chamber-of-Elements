using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverInterface : ButtonInterface
{
    [Scene]
    [SerializeField] string mainMenuScene;

    public void RetryButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButtonClick()
    {
        GameManager.Instance.DataHolder.UnSetPlayer();
        GameManager.Instance.DataHolder.UnSetEnemy();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void MainMenuButtonClickAdventure()
    {
        GameManager.Instance.RunTracker.chapters = null;
        GameManager.Instance.RunTracker.currentChapter = null;
        GameManager.Instance.RunTracker.currentBattle = null;
        GameManager.Instance.DataHolder.SetGameMode(GameModes.Battle);
        GameManager.Instance.DataHolder.UnSetPlayer();
        GameManager.Instance.DataHolder.UnSetEnemy();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void NextBattle()
    {
        GameManager.Instance.RunTracker.EndBattle(Player.Instance.Stats.CurrentHealth, Player.Instance.Relic.RelicObject, Player.Instance.UnlockedElementRecipes, Player.Instance.UnlockedMinionRecipes, Player.Instance.UnlockedRelicRecipes, Player.Instance.ReRollElements);
    }
}
