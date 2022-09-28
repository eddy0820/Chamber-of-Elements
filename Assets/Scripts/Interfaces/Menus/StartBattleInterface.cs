using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleInterface : ButtonInterface
{
    public void StartBattle(GameObject arrow)
    {
        arrow.SetActive(false);
        PathSelectionScreenController.Instance.RunTracker.currentBattle = PathSelectionScreenController.Instance.CurrentlySelectedBattle;
        UnityEngine.SceneManagement.SceneManager.LoadScene(PathSelectionScreenController.Instance.RunTracker.BattleLoadingScreen);
    }
}
