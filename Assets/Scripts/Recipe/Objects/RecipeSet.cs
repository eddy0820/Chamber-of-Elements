using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe Set", menuName = "Databases/Recipe Set")]
public class RecipeSet : ScriptableObject
{
    [SerializeField] List<ElementRecipeObject> elementRecipes = new List<ElementRecipeObject>();
    public List<ElementRecipeObject> ElementRecipes => elementRecipes;
    [SerializeField] List<MinionRecipeObject> minionRecipes = new List<MinionRecipeObject>();
    public List<MinionRecipeObject> MinionRecipes => minionRecipes;
    [SerializeField] List<RelicRecipeObject> relicRecipes = new List<RelicRecipeObject>();
    public List<RelicRecipeObject> RelicRecipes => relicRecipes;
}
