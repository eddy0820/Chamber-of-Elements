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
        SceneManager.LoadScene(mainMenuScene);
    }
}
