using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunTracker : MonoBehaviour
{
    [ReadOnly] public Queue<Chapter> chapters;

    [Scene]
    [SerializeField] string battleLoadingScreen;

    public Chapter currentChapter;
    public Battle currentBattle;

    public Battle lastBattle;

    float playerHealth;
    public float PlayerHealth => playerHealth;
    RelicObject playerRelic;
    public RelicObject PlayerRelic => playerRelic;
    [ReadOnly, SerializeField] Player. SerializableHashSet<AffinityTypes> unlockedAffinities;
    public Player. SerializableHashSet<AffinityTypes> UnlockedAffinities => unlockedAffinities;
    [ReadOnly, SerializeField] Player. SerializableHashSet<ElementRecipeObject> unlockedElementRecipes;
    public Player. SerializableHashSet<ElementRecipeObject> UnlockedElementRecipes => unlockedElementRecipes;
    [ReadOnly, SerializeField] Player. SerializableHashSet<MinionRecipeObject> unlockedMinionRecipes;
    public Player. SerializableHashSet<MinionRecipeObject> UnlockedMinionRecipes => unlockedMinionRecipes;
    [ReadOnly, SerializeField] Player. SerializableHashSet<RelicRecipeObject> unlockedRelicRecipes;
    public Player. SerializableHashSet<RelicRecipeObject> UnlockedRelicRecipes => unlockedRelicRecipes;
    [ReadOnly, SerializeField] Player. SerializableHashSet<ElementObject> reRollElements;
    public Player. SerializableHashSet<ElementObject> ReRollElements => reRollElements;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        chapters = new Queue<Chapter>();
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
}
