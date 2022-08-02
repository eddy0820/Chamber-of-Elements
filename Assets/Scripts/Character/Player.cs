using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    public static Player Instance {get; private set; }

    [Header("Player Specific")]
    [SerializeField] Sprite affinityNoneSprite; 
    public Sprite AffinityNoneSprite => affinityNoneSprite;
    SpriteRenderer affinitySprite;

    [ReadOnly] bool minionExists;
    public bool MinionExists => minionExists;

    [ReadOnly] bool hasRelic;
    public bool HasRelic => hasRelic;

    Minion minion;
    public Minion Minion => minion;
    Relic relic;
    public Relic Relic => relic;
    
    [Space(15)]
    [ReadOnly, SerializeField] UnlockedList<AffinityTypes> unlockedAffinities;
    public UnlockedList<AffinityTypes> UnlockedAffinities => unlockedAffinities;
    [ReadOnly, SerializeField] UnlockedList<ElementRecipeObject> unlockedElementRecipes;
    public UnlockedList<ElementRecipeObject> UnlockedElementRecipes => unlockedElementRecipes;
    [ReadOnly, SerializeField] UnlockedList<MinionRecipeObject> unlockedMinionRecipes;
    public UnlockedList<MinionRecipeObject> UnlockedMinionRecipes => unlockedMinionRecipes;
    [ReadOnly, SerializeField] UnlockedList<RelicRecipeObject> unlockedRelicRecipes;
    public UnlockedList<RelicRecipeObject> UnlockedRelicRecipes => unlockedRelicRecipes;


    private void Awake()
    {
        Instance = this;
        
        stats = new PlayerStats(characterObject.BaseStats);
        affinitySprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        InitCharacter();
        ChangeAttacker(GameManager.Instance.Enemy);

        minion = GameObject.FindWithTag("Minion").GetComponent<Minion>();
        relic = GetComponentInChildren<Relic>();
    }

    public override void SwitchAffinity(AffinityTypes type)
    {
        base.SwitchAffinity(type);
        UpdateAffinitySprite(type);
    }

    private void UpdateAffinitySprite(AffinityTypes type)
    {
        if(type == AffinityTypes.None)
        {
            affinitySprite.sprite = affinityNoneSprite;
        }
        else
        {
            affinitySprite.sprite = GameManager.Instance.AffinityDatabase.GetAffinity[type].Sprite;
        }
    }

    protected override void InitCharacter()
    {
        base.InitCharacter();

        unlockedAffinities.unlocked = new HashSet<AffinityTypes>(((PlayerObject) characterObject).UnlockedStartingAffinities);
        
        unlockedElementRecipes.unlocked = new HashSet<ElementRecipeObject>();
        unlockedMinionRecipes.unlocked = new HashSet<MinionRecipeObject>();

        foreach(PlayerObject.UnlockedRecipeEntry recipeEntry in ((PlayerObject) characterObject).UnlockedElementRecipeEntries)
        {
            if(recipeEntry.RecipeSet)
            {
                unlockedElementRecipes.unlocked.UnionWith(recipeEntry.Set.ElementRecipes);
                unlockedMinionRecipes.unlocked.UnionWith(recipeEntry.Set.MinionRecipes);
                unlockedRelicRecipes.unlocked.UnionWith(recipeEntry.Set.RelicRecipes);
            }
            else
            {
                if(recipeEntry.Recipe is ElementRecipeObject)
                {
                    unlockedElementRecipes.unlocked.Add((ElementRecipeObject) recipeEntry.Recipe);
                }
                else if(recipeEntry.Recipe is MinionRecipeObject)
                {
                    unlockedMinionRecipes.unlocked.Add((MinionRecipeObject) recipeEntry.Recipe);
                }
                else if(recipeEntry.Recipe is RelicRecipeObject)
                {
                    unlockedRelicRecipes.unlocked.Add((RelicRecipeObject) recipeEntry.Recipe);
                }
            }
        }
    }

    public void SetMinionExists(bool exists)
    {
        minionExists = exists;
    }

    public void SetHasRelic(bool exists)
    {
        hasRelic = exists;
    }

    public bool UnlockAffinity(AffinityTypes affinity)
    {
        return unlockedAffinities.unlocked.Add(affinity);
    }

    public bool LockAffinity(AffinityTypes affinity)
    {
        return unlockedAffinities.unlocked.Remove(affinity);
    }

    public bool UnlockRecipeSet(RecipeSet recipeSet)
    {
        bool allAdded = true;

        foreach(ElementRecipeObject er in recipeSet.ElementRecipes)
        {
            if(unlockedElementRecipes.unlocked.Add(er) == false)
            {
                allAdded = false;
            }
        }

        foreach(MinionRecipeObject mr in recipeSet.MinionRecipes)
        {
            if(unlockedMinionRecipes.unlocked.Add(mr) == false)
            {
                allAdded = false;
            }
        }

        foreach(RelicRecipeObject rr in recipeSet.RelicRecipes)
        {
            if(unlockedRelicRecipes.unlocked.Add(rr) == false)
            {
                allAdded = false;
            }
        }

        return allAdded;
    }

    public bool LockRecipeSet(RecipeSet recipeSet)
    {
        bool allRemoved = true;

        foreach(ElementRecipeObject er in recipeSet.ElementRecipes)
        {
            if(unlockedElementRecipes.unlocked.Remove(er) == false)
            {
                allRemoved = false;
            }
        }

        foreach(MinionRecipeObject mr in recipeSet.MinionRecipes)
        {
            if(unlockedMinionRecipes.unlocked.Remove(mr) == false)
            {
                allRemoved = false;
            }
        }

        foreach(RelicRecipeObject rr in recipeSet.RelicRecipes)
        {
            if(unlockedRelicRecipes.unlocked.Remove(rr) == false)
            {
                allRemoved = false;
            }
        }

        return allRemoved;
    }

    [System.Serializable]
    public class UnlockedList<T> : ISerializationCallbackReceiver
    {
        [ReadOnly, SerializeField] List<T> _unlocked = new List<T>();
        public HashSet<T> unlocked = new HashSet<T>();
    
        public void OnBeforeSerialize()
        {
            _unlocked.Clear();
            foreach(T affinityType in unlocked)
            {
                _unlocked.Add(affinityType);
            }
        }

        public void OnAfterDeserialize() {}
    }
}
