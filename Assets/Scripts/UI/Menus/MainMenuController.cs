using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance {get; private set; }
    [SerializeField] GameObject mainMenuScreen;
    public GameObject MainMenuScreen => mainMenuScreen;
    [SerializeField] GameObject howToPlayMenuScreen;
    public GameObject HowToPlayMenuScreen => howToPlayMenuScreen;
    [SerializeField] GameObject characterSelectScreen;
    public GameObject CharacterSelectScreen => characterSelectScreen;
    [SerializeField] GameObject enemySelectScreen;
    public GameObject EnemySelectScreen => enemySelectScreen;
    [SerializeField] GameObject chooseGameModeScreen;
    public GameObject ChooseGameModeScreen => chooseGameModeScreen;

    [Space(15)]

    [Scene]
    [SerializeField] string battleScene;
    public string BattleScene => battleScene;
    [SerializeField] GameObject dataHolderPrefab;
    [SerializeField] GameObject runTrackerPrefab;

    [Space(15)]

    [SerializeField] GameObject characterSelectNext;
    public GameObject CharacterSelectNext => characterSelectNext;
    [SerializeField] GameObject characterSelectStart;
    public GameObject CharacterSelectStart => characterSelectStart;

    [Space(15)]

    [SerializeField] AffinityDatabase affinityDatabase;
    public AffinityDatabase AffinityDatabase => affinityDatabase;
    [SerializeField] protected CharacterDatabase characterDatabase;
    public CharacterDatabase CharacterDatabase => characterDatabase;

    [Space(15)]

    [SerializeField] AdventureRunObject defaultRun;

    DataHolder dataHolder;
    public DataHolder DataHolder => dataHolder;
    RunTracker runTracker;
    public RunTracker RunTracker => runTracker;


    private void Awake()
    {
        Instance = this;
             
        affinityDatabase.InitAffinities();

        try
        {
            dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
            runTracker = GameObject.FindWithTag("RunTracker").GetComponent<RunTracker>();
        }
        catch {}

        if(dataHolder == null)
        {
            GameObject dataHolderObject = Instantiate(dataHolderPrefab, dataHolderPrefab.transform.position, dataHolderPrefab.transform.rotation);
            dataHolder = dataHolderObject.GetComponent<DataHolder>();
        }

        if(runTracker == null)
        {
            GameObject runTrackerObject = Instantiate(runTrackerPrefab, runTrackerPrefab.transform.position, Quaternion.identity);
            runTracker = runTrackerObject.GetComponent<RunTracker>();
        }
    }

    public void BuildRun()
    {
        foreach(ChapterObject chapter in defaultRun.Chapters)
        {
            runTracker.chapters.Enqueue(new Chapter
            (
                chapter,
                Random.Range(chapter.FirstHalfBattleRange.Min, chapter.FirstHalfBattleRange.Max + 1), 
                Random.Range(chapter.FirstHalfEliteBattleRange.Min, chapter.FirstHalfEliteBattleRange.Max + 1),
                Random.Range(chapter.SecondHalfBattleRange.Min, chapter.SecondHalfBattleRange.Max + 1),
                Random.Range(chapter.SecondHalfEliteBattleRange.Min, chapter.SecondHalfEliteBattleRange.Max)
            ));
        }   

        //DebugPrintRun();
    }

    public void DebugPrintRun()
    {
        foreach(Chapter chapter in runTracker.chapters)
        {
            Debug.Log("Chapter: " + chapter.ChapterObject.Name);

            foreach(Battle battle in chapter.Battles)
            {
                switch(battle.BattleType)
                {
                    case BattleTypes.Starting:
                        Debug.Log("--Starting Battle: ");
                        break;
                    case BattleTypes.Battle:
                        Debug.Log("--Battle: ");
                        break;
                    case BattleTypes.EliteBattle:
                        Debug.Log("--Elite Battle: ");
                        break;
                    case BattleTypes.MiniBoss:
                        Debug.Log("--MiniBoss Battle: ");
                        break;
                    case BattleTypes.BattlePlus:
                        Debug.Log("--Battle+: ");
                        break;
                    case BattleTypes.EliteBattlePlus:
                        Debug.Log("--Elite Battle+: ");
                        break;
                    case BattleTypes.Boss:
                        Debug.Log("--Boss Battle: ");
                        break;
                }

                Debug.Log("----Enemy: " + battle.Enemy.Name);
            }
        }
    }
}
