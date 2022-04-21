using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Affinity", menuName = "Affinity")]
public class AffinityObject : ScriptableObject
{
    [SerializeField] AffinityTypes type;
    public AffinityTypes Type => type;
    [SerializeField] Sprite sprite;
    public Sprite Sprite => sprite;
    [SerializeField] ElementObject recipeElement;
    public ElementObject RecipeElement => recipeElement;
}

