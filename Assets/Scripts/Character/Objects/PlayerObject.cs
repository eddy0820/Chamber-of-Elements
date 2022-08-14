using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Characters/Player")]
public class PlayerObject : CharacterObject 
{
    [Header("Player")]
    [SerializeField] List<AffinityTypes> unlockedStartingAffinities;
    public List<AffinityTypes> UnlockedStartingAffinities => unlockedStartingAffinities;

    [SerializeField] List<UnlockedRecipeEntry> unlockedRecipeEntries;
    public List<UnlockedRecipeEntry> UnlockedElementRecipeEntries => unlockedRecipeEntries;

    [Header("Character Card Settings")]
    [SerializeField] Vector3 cardPosition;
    public Vector3 CardPosition => cardPosition;
    [SerializeField] Vector3 cardScale = Vector3.one;
    public Vector3 CardScale => cardScale;

    [System.Serializable]
    public class UnlockedRecipeEntry
    {
        [SerializeField] bool recipeSet;
        public bool RecipeSet => recipeSet;

        [SerializeField] RecipeSet set;
        public RecipeSet Set => set;
        
        [SerializeField] AbstractRecipeObject recipe;
        public AbstractRecipeObject Recipe => recipe;
    }
}
