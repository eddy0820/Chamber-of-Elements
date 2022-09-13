using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLoseScreenNavigation : MonoBehaviour
{
    [SerializeField] GameObject retryArrow;
    [SerializeField] GameObject mainMenuArrow;

    [Space(15)]

    [Scene]
    [SerializeField] string mainMenuScene;

    public void RetryButtonSelected()
    {
        retryArrow.SetActive(true);
    }

    public void RetryButtonDeselected()
    {
        retryArrow.SetActive(false);
    }

    public void MainMenuButtonSelected()
    {
        mainMenuArrow.SetActive(true);
    }

    public void MainMenuButtonDeselected()
    {
        mainMenuArrow.SetActive(false);
    }

    public void RetryButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
