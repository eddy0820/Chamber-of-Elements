using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion Recipe", menuName = "Recipes/Minion Recipe")]
public class MinionRecipeObject : AbstractRecipeObject<MinionObject>
{
    [Header("Catalyst")]
    [SerializeField] ElementObject catalystElement;
    public ElementObject CatalystElement => catalystElement;
}
