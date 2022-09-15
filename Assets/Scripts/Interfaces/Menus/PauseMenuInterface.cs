using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuInterface : ButtonInterface
{
    [Scene]
    [SerializeField] string mainMenuScene;

    public void ResumeButtonClick()
    {
        PauseMenu.Instance.ResumeGame();
    }

    public void MainMenuButtonClick()
    {
        PauseMenu.Instance.ResumeGame();
        SceneManager.LoadScene(mainMenuScene);
    }
}
