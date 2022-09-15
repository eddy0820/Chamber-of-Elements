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
        SceneManager.LoadScene(mainMenuScene);
    }
}
