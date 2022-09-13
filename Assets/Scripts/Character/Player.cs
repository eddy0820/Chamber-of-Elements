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
    [ReadOnly, SerializeField] SerializableHashSet<AffinityTypes> unlockedAffinities;
    public SerializableHashSet<AffinityTypes> UnlockedAffinities => unlockedAffinities;
    [ReadOnly, SerializeField] SerializableHashSet<ElementRecipeObject> unlockedElementRecipes;
    public SerializableHashSet<ElementRecipeObject> UnlockedElementRecipes => unlockedElementRecipes;
    [ReadOnly, SerializeField] SerializableHashSet<MinionRecipeObject> unlockedMinionRecipes;
    public SerializableHashSet<MinionRecipeObject> UnlockedMinionRecipes => unlockedMinionRecipes;
    [ReadOnly, SerializeField] SerializableHashSet<RelicRecipeObject> unlockedRelicRecipes;
    public SerializableHashSet<RelicRecipeObject> UnlockedRelicRecipes => unlockedRelicRecipes;
    [ReadOnly, SerializeField] SerializableHashSet<ElementObject> reRollElements;
    public SerializableHashSet<ElementObject> ReRollElements => reRollElements;

    public void DoAwake(PlayerObject playerObject)
    {
        Instance = this;

        if(playerObject != null)
        {
            characterObject = playerObject;
        }
        
        stats = new PlayerStats(characterObject.BaseStats, HPText, this);
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

        unlockedAffinities.set = new HashSet<AffinityTypes>(((PlayerObject) characterObject).UnlockedStartingAffinities);
        
        unlockedElementRecipes.set = new HashSet<ElementRecipeObject>();
        unlockedMinionRecipes.set = new HashSet<MinionRecipeObject>();
        unlockedRelicRecipes.set = new HashSet<RelicRecipeObject>();

        foreach(PlayerObject.UnlockedRecipeEntry recipeEntry in ((PlayerObject) characterObject).UnlockedElementRecipeEntries)
        {
            if(recipeEntry.RecipeSet)
            {
                unlockedElementRecipes.set.UnionWith(recipeEntry.Set.ElementRecipes);
                unlockedMinionRecipes.set.UnionWith(recipeEntry.Set.MinionRecipes);
                unlockedRelicRecipes.set.UnionWith(recipeEntry.Set.RelicRecipes);
            }
            else
            {
                if(recipeEntry.Recipe is ElementRecipeObject)
                {
                    unlockedElementRecipes.set.Add((ElementRecipeObject) recipeEntry.Recipe);
                }
                else if(recipeEntry.Recipe is MinionRecipeObject)
                {
                    unlockedMinionRecipes.set.Add((MinionRecipeObject) recipeEntry.Recipe);
                }
                else if(recipeEntry.Recipe is RelicRecipeObject)
                {
                    unlockedRelicRecipes.set.Add((RelicRecipeObject) recipeEntry.Recipe);
                }
            }
        }

        reRollElements.set = new HashSet<ElementObject>();

        foreach(ElementObject element in ((PlayerObject) characterObject).StartingReRollElements)
        {
            reRollElements.set.Add(element);
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
        return unlockedAffinities.set.Add(affinity);
    }

    public bool LockAffinity(AffinityTypes affinity)
    {
        return unlockedAffinities.set.Remove(affinity);
    }

    public bool UnlockRecipeSet(RecipeSet recipeSet)
    {
        bool allAdded = true;

        foreach(ElementRecipeObject er in recipeSet.ElementRecipes)
        {
            if(unlockedElementRecipes.set.Add(er) == false)
            {
                allAdded = false;
            }
        }

        foreach(MinionRecipeObject mr in recipeSet.MinionRecipes)
        {
            if(unlockedMinionRecipes.set.Add(mr) == false)
            {
                allAdded = false;
            }
        }

        foreach(RelicRecipeObject rr in recipeSet.RelicRecipes)
        {
            if(unlockedRelicRecipes.set.Add(rr) == false)
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
            if(unlockedElementRecipes.set.Remove(er) == false)
            {
                allRemoved = false;
            }
        }

        foreach(MinionRecipeObject mr in recipeSet.MinionRecipes)
        {
            if(unlockedMinionRecipes.set.Remove(mr) == false)
            {
                allRemoved = false;
            }
        }

        foreach(RelicRecipeObject rr in recipeSet.RelicRecipes)
        {
            if(unlockedRelicRecipes.set.Remove(rr) == false)
            {
                allRemoved = false;
            }
        }

        return allRemoved;
    }

    [System.Serializable]
    public class SerializableHashSet<T> : ISerializationCallbackReceiver
    {
        [ReadOnly, SerializeField] List<T> _list = new List<T>();
        public HashSet<T> set = new HashSet<T>();
    
        public void OnBeforeSerialize()
        {
            _list.Clear();
            foreach(T entry in set)
            {
                _list.Add(entry);
            }
        }

        public void OnAfterDeserialize() {}
    }
}
