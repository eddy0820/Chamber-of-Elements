using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleLoadingScreenController : MonoBehaviour
{
    GameObject chapterText;
    GameObject battleText;

    RunTracker runTracker;
    DataHolder dataHolder;

    [Scene]
    [SerializeField] string battleScene;

    private void Awake()
    {
        chapterText = transform.GetChild(0).gameObject;
        battleText = transform.GetChild(1).gameObject;

        runTracker = GameObject.FindWithTag("RunTracker").GetComponent<RunTracker>();
        dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
    }

    private void Start()
    {
        runTracker.currentChapter = runTracker.chapters.Peek();
        runTracker.currentBattle = runTracker.currentChapter.Battles.Peek().Branches[0]; ///////

        SetText();

        StartCoroutine(DoChapterText());
    }

    IEnumerator DoChapterText()
    {
        yield return new WaitForSeconds(1f);

        chapterText.GetComponent<TextAnimatorPlayer>().StartShowingText();

        yield break;
    }

    public void ShowBattleText()
    {
        StartCoroutine(DoBattleText());
    }

    IEnumerator DoBattleText()
    {
        yield return new WaitForSeconds(1f);

        battleText.GetComponent<TextAnimatorPlayer>().StartShowingText();

        yield break;
    }

    public void GoToBattle()
    {
        StartCoroutine(DoBattle());
    }

    IEnumerator DoBattle()
    {
        yield return new WaitForSeconds(1.5f);

        dataHolder.SetEnemy(runTracker.currentBattle.Enemy);
        SceneManager.LoadScene(battleScene);

        yield break;
    }

    private void SetText()
    {
        chapterText.GetComponent<TextMeshProUGUI>().text = "Chapter: " + runTracker.currentChapter.ChapterObject.Name;

        switch(runTracker.currentBattle.BattleType)
        {
            case BattleTypes.Starting:
                battleText.GetComponent<TextMeshProUGUI>().text = "Starting Battle";
                break;
            case BattleTypes.Battle:
                battleText.GetComponent<TextMeshProUGUI>().text = "Battle";
                break;
            case BattleTypes.EliteBattle:
                battleText.GetComponent<TextMeshProUGUI>().text = "Elite Battle";
                break;
            case BattleTypes.MiniBoss:
                battleText.GetComponent<TextMeshProUGUI>().text = "Mini Boss";
                break;
            case BattleTypes.BattlePlus:
                battleText.GetComponent<TextMeshProUGUI>().text = "Battle +";
                break;
            case BattleTypes.EliteBattlePlus:
                battleText.GetComponent<TextMeshProUGUI>().text = "Elite Battle +";
                break;
            case BattleTypes.Boss:
                battleText.GetComponent<TextMeshProUGUI>().text = "Boss";
                break;
        }
    }
}
