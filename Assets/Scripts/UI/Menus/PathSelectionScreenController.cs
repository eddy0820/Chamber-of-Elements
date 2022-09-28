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

    [Space(15)]

    [SerializeField] Color battleHiddenColor;
    public Color BattleHiddenColor => battleHiddenColor;

    [Space(15)]
    
    [SerializeField] GameObject startBattleButton;

    RunTracker runTracker;
    public RunTracker RunTracker => runTracker;
    DataHolder dataHolder;

    Battle currentlySelectedBattle;
    public Battle CurrentlySelectedBattle => currentlySelectedBattle;

    public Dictionary<BattleSelection, GameObject> GetBattleSelectionGameObject;

    private void Awake()
    {
        Instance = this;

        try
        {   
            runTracker = GameObject.FindWithTag("RunTracker").GetComponent<RunTracker>();
            dataHolder = GameObject.FindWithTag("DataHolder").GetComponent<DataHolder>();
        }
        catch{}
        
        LayoutRun();
        GoToCurrentBattleSelection();
    }

    private void Update()
    {
        if(currentlySelectedBattle == null)
        {
            startBattleButton.SetActive(false);
        }
        else
        {
            startBattleButton.SetActive(true);
        }
    }

    private void LayoutRun()
    {
        GetBattleSelectionGameObject = new Dictionary<BattleSelection, GameObject>();

        if(runTracker != null)
        {
            runTracker.currentChapter = runTracker.chapters.Peek();
        
            foreach(BattleSelection battleSelection in runTracker.currentChapter.BattleSelectionsTotal)
            {
                GameObject battleSelectGrid = Instantiate(branchGridPrefab, branchGridPrefab.transform.position, branchGridPrefab.transform.rotation, battleSpacingGrid.transform);
                battleSelectGrid.GetComponent<BranchGridController>().Init(battleSelection);
                GetBattleSelectionGameObject.Add(battleSelection, battleSelectGrid);

                foreach(Battle battle in battleSelection.Branches)
                {
                    GameObject battleSelect = Instantiate(branchPrefab, branchPrefab.transform.position, branchPrefab.transform.rotation, battleSelectGrid.transform);
                    battleSelect.GetComponent<BranchInterface>().Init(battle);
                }
            }
        }
    }

    private void GoToCurrentBattleSelection()
    {
        runTracker.currentBattleSelection = runTracker.currentChapter.BattleSelections.Peek();

        foreach(KeyValuePair<BattleSelection, GameObject> branch in GetBattleSelectionGameObject)
        {
            if(branch.Key != runTracker.currentBattleSelection)
            {
                branch.Value.GetComponent<BranchGridController>().HideBattles();
            }
        }
        
        CenterBattle((RectTransform) GetBattleSelectionGameObject[runTracker.currentBattleSelection].transform);
    }

    public void SelectBattle(Battle battle)
    {
        currentlySelectedBattle = battle;
    }

    public void CenterBattle(RectTransform child)
    {
        ScrollRect scrollRect = GetComponentInChildren<ScrollRect>();

        Canvas.ForceUpdateCanvases();

        Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;
        Vector2 childLocalPosition   = child.localPosition;
        Vector2 result = new Vector2
        (
            0 - (viewportLocalPosition.x + childLocalPosition.x),
            0 - (viewportLocalPosition.y + childLocalPosition.y)
        );

        scrollRect.content.localPosition = result;
        
    }
}
