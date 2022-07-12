using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion Recipe Database", menuName = "Databases/Minion Recipes")]
public class MinionRecipeDatabase : ScriptableObject
{
    public List<MinionRecipeObject> minionRecipeObjects = new List<MinionRecipeObject>();
}
