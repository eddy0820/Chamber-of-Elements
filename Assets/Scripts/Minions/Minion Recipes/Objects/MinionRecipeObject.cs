using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minion Recipe", menuName = "Minion Recipe")]
public class MinionRecipeObject : ScriptableObject
{
    [SerializeField] ElementObject catalystElement;
    public ElementObject CatalystElement => catalystElement;
    [SerializeField] List<ElementObject> secondaryIngredients = new List<ElementObject>();
    public List<ElementObject> SecondaryIngredients => secondaryIngredients;
    [SerializeField] MinionObject result;
    public MinionObject Result => result;
}
