using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunTracker : MonoBehaviour
{
    public Queue<Chapter> chapters;

    [Scene]
    [SerializeField] string pathSelectionScreen;

    [Scene]
    [SerializeField] string battleLoadingScreen;

    [Space(15)]
    [SerializeField] AdventureRunObject defaultRun;

    public Chapter currentChapter;
    public Battle currentBattle;

    public Battle lastBattle;

    float playerHealth;
    public float PlayerHealth => playerHealth;
    RelicObject playerRelic;
    public RelicObject PlayerRelic => playerRelic;
    Player.SerializableHashSet<AffinityTypes> unlockedAffinities;
    public Player.SerializableHashSet<AffinityTypes> UnlockedAffinities => unlockedAffinities;
    Player.SerializableHashSet<ElementRecipeObject> unlockedElementRecipes;
    public Player.SerializableHashSet<ElementRecipeObject> UnlockedElementRecipes => unlockedElementRecipes;
    Player.SerializableHashSet<MinionRecipeObject> unlockedMinionRecipes;
    public Player.SerializableHashSet<MinionRecipeObject> UnlockedMinionRecipes => unlockedMinionRecipes;
    Player.SerializableHashSet<RelicRecipeObject> unlockedRelicRecipes;
    public Player.SerializableHashSet<RelicRecipeObject> UnlockedRelicRecipes => unlockedRelicRecipes;
    Player.SerializableHashSet<ElementObject> reRollElements;
    public Player.SerializableHashSet<ElementObject> ReRollElements => reRollElements;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        chapters = new Queue<Chapter>();
    }

    public void GoToPathSelection()
    {
        SceneManager.LoadScene(pathSelectionScreen);
    }

    public void StartBattle()
    {   
        SceneManager.LoadScene(battleLoadingScreen);
    }

    public void EndBattle(float _playerHealth, RelicObject _playerRelic, Player.SerializableHashSet<ElementRecipeObject> _unlockedElementRecipes,  Player.SerializableHashSet<MinionRecipeObject> _unlockedMinionRecipes, Player.SerializableHashSet<RelicRecipeObject> _unlockedRelicRecipes, Player.SerializableHashSet<ElementObject> _reRollElements)
    {
        currentChapter.Battles.Dequeue();

        if(currentChapter.Battles.Count == 0)
        {
            chapters.Dequeue();
        }

        lastBattle = currentBattle;

        playerHealth = _playerHealth;
        playerRelic = _playerRelic;
        unlockedElementRecipes = _unlockedElementRecipes;
        unlockedMinionRecipes = _unlockedMinionRecipes;
        unlockedRelicRecipes = _unlockedRelicRecipes;
        reRollElements = _reRollElements;

        if(chapters.Count == 0 && currentChapter.Battles.Count == 0)
        {
            // End Run
            Debug.Log("Run Finished!");
        }
        else
        {
            StartBattle();
        }    
    }

    public void BuildRun()
    {
        chapters = new Queue<Chapter>();

        foreach(ChapterObject chapter in defaultRun.Chapters)
        {
            chapters.Enqueue(new Chapter
            (
                chapter,
                Random.Range(chapter.Battles.Min, chapter.Battles.Max + 1), 
                Random.Range(chapter.EliteBattles.Min, chapter.EliteBattles.Max + 1),
                Random.Range(chapter.BattlesPlus.Min, chapter.BattlesPlus.Max + 1),
                Random.Range(chapter.EliteBattlesPlus.Min, chapter.EliteBattlesPlus.Max + 1)
            ));
        }   

        DebugPrintRun();
    }

    public void DebugPrintRun()
    {
        foreach(Chapter chapter in chapters)
        {
            Debug.Log("Chapter: " + chapter.ChapterObject.Name);

            foreach(BattleSelection battleSelection in chapter.Battles)
            {
                switch(battleSelection.BranchBattleType)
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

                Debug.Log("----Branches: " + battleSelection.NumBranches);

                foreach(Battle battle in battleSelection.Branches)
                {
                    Debug.Log("------Enemy: " + battle.Enemy.Name);
                }
            }
        }
    }
}
