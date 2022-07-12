using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element Recipe Database", menuName = "Databases/Element Recipes")]
public class ElementRecipeDatabase : ScriptableObject
{
    public List<ElementRecipeObject> elementRecipeObjects = new List<ElementRecipeObject>();
}
