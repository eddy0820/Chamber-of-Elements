using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathSelectionScreenController : MonoBehaviour
{
    public static PathSelectionScreenController Instance {get; private set; }

    [SerializeField] GameObject branchGridPrefab;
    [SerializeField] GameObject branchPrefab;

    [Space(15)]

    [SerializeField] GameObject battleSpacingGrid;

    [Space(15)]
    [SerializeField] Scrollbar scrollbar;

    RunTracker runTracker;
    public RunTracker RunTracker => runTracker;
    DataHolder dataHolder;

    Battle currentlySelectedBattle;
    public Battle CurrentlySelectedBattle => currentlySelectedBattle;

    private void Awake()
    {
        Instance = this;

        runTracker = GameObject.FindWithTag("RunTracker").GetComponent<RunTracker>();
        dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();

        runTracker.currentChapter = runTracker.chapters.Peek();
        
        foreach(BattleSelection battleSelection in runTracker.currentChapter.Battles)
        {
            GameObject battleSelectGrid = Instantiate(branchGridPrefab, branchGridPrefab.transform.position, branchGridPrefab.transform.rotation, battleSpacingGrid.transform);

            foreach(Battle battle in battleSelection.Branches)
            {
                GameObject battleSelect = Instantiate(branchPrefab, branchPrefab.transform.position, branchPrefab.transform.rotation, battleSelectGrid.transform);
                battleSelect.GetComponent<BranchInterface>().Init(battle);
            }
        }
    }

    public void SelectBattle(Battle battle)
    {
        currentlySelectedBattle = battle;
    }
}
