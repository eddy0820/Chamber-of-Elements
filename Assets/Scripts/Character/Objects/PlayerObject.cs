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
