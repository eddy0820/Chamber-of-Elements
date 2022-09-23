using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuInterface : ButtonInterface
{
    [Scene]
    [SerializeField] string mainMenuScene;

    public void ResumeButtonClick(GameObject arrow)
    {
        PauseMenu.Instance.ResumeGame();
        arrow.SetActive(false);
    }

    public void MainMenuButtonClick()
    {
        PauseMenu.Instance.ResumeGame();
        GameManager.Instance.DataHolder.UnSetPlayer();
        GameManager.Instance.DataHolder.UnSetEnemy();
        SceneManager.LoadScene(mainMenuScene);
    }

    public void MainMenuButtonClickAdventure()
    {
        PauseMenu.Instance.ResumeGame();
        GameManager.Instance.RunTracker.chapters = null;
        GameManager.Instance.RunTracker.currentChapter = null;
        GameManager.Instance.RunTracker.currentBattle = null;
        GameManager.Instance.DataHolder.SetGameMode(GameModes.Battle);
        GameManager.Instance.DataHolder.UnSetPlayer();
        GameManager.Instance.DataHolder.UnSetEnemy();
        SceneManager.LoadScene(mainMenuScene);
    }
}
