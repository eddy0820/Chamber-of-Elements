using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element Recipe", menuName = "Element Recipe")]
public class ElementRecipeObject : ScriptableObject
{
    [SerializeField] List<ElementObject> ingredients = new List<ElementObject>();
    public List<ElementObject> Ingredients => ingredients;
    [SerializeField] ElementObject result;
    public ElementObject Result => result;
}
