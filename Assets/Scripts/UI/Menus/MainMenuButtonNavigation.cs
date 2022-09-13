using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonNavigation : MonoBehaviour
{
    [Header("Main Menu Arrows")]
    [SerializeField] GameObject playArrow;
    [SerializeField] GameObject howToPlayArrow;
    [SerializeField] GameObject quitArrow;

    public void ExitGame()
    {
        Debug.Log("Exited Game!");
        Application.Quit();
    }

    public void PlayButtonSelected()
    {
        playArrow.SetActive(true);
    }

    public void PlayButtonDeselected()
    {
        playArrow.SetActive(false);
    }

    public void HowToPlayButtonSelected()
    {
        howToPlayArrow.SetActive(true);
    }

    public void HowToPlayButtonDeselected()
    {
        howToPlayArrow.SetActive(false);
    }

    public void QuitButtonSelected()
    {
        quitArrow.SetActive(true);
    }

    public void QuitButtonDeselected()
    {
        quitArrow.SetActive(false);
    }
}
