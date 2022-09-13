using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance {get; private set; }

    bool isGamePaused;
    public bool IsGamePaused => isGamePaused;

    private void Awake()
    {
        Instance = this;
    }

    public void PauseGame()
    {   
        Time.timeScale = 0.0f;
        isGamePaused = true;
        GameManager.Instance.PauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isGamePaused = false;
        GameManager.Instance.PauseCanvas.SetActive(false);
    }
}
