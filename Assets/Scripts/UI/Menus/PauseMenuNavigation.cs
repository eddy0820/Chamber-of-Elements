using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject resumeArrow;
    [SerializeField] GameObject mainMenuArrow;

    [Space(15)]

    [Scene]
    [SerializeField] string mainMenuScene;

    public void ResumeButtonSelected()
    {
        resumeArrow.SetActive(true);
    }

    public void ResumeButtonDeselected()
    {
        resumeArrow.SetActive(false);
    }

    public void MainMenuButtonSelected()
    {
        mainMenuArrow.SetActive(true);
    }

    public void MainMenuButtonDeselected()
    {
        mainMenuArrow.SetActive(false);
    }

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
