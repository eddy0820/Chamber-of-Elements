using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractRecipeObject<T> : AbstractRecipeObject
{
    [SerializeField] List<ElementObject> ingredients = new List<ElementObject>();
    public List<ElementObject> Ingredients => ingredients;
    [SerializeField] T result;
    public T Result => result;
}

public abstract class AbstractRecipeObject : ScriptableObject {}
