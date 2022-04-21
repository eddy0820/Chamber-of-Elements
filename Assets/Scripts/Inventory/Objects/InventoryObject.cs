using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] ElementDatabase database;
    public ElementDatabase Database => database;
    [SerializeField] ElementRecipeDatabase recipeDatabase;
    public ElementRecipeDatabase RecipeDatabase => recipeDatabase;
    [SerializeField] ElementObject[] reRollElements;
    [SerializeField] Inventory container;
    public Inventory Container => container;

    public void ReRollElements()
    {
        for(int i = 0; i < container.elementSlots.Length; i++)
        {
            if(container.elementSlots[i].ID < 0)
            {
                int count = 0;
                int excludeId = 0;
                var element = database.GetElement[UnityEngine.Random.Range(0, reRollElements.Length)];
                
                for(int j = 0; j < i; j++)
                {
                    if(container.elementSlots[j].ID == element.ID)
                    {
                        excludeId = container.elementSlots[j].ID;
                        count++;
                    }
                }

                if(count >= 2)
                {
                    int newRandom = UnityEngine.Random.Range(0, reRollElements.Length);

                    while(newRandom == excludeId)
                    {
                        newRandom = UnityEngine.Random.Range(0, reRollElements.Length);
                    }

                    container.elementSlots[i].UpdateSlot(new Element(database.GetElement[newRandom]));
                    
                }
                else
                {
                    container.elementSlots[i].UpdateSlot(new Element(element));
                }
            }
        }
    }

    public int CanCombine(Element hoverElement, Element mouseElement)
    {
        int result = -2;

        foreach(ElementRecipeObject recipe in recipeDatabase.elementRecipeObjects)
        {
            if(hoverElement.ID == mouseElement.ID)
            {
                if(database.GetElement[hoverElement.ID].Type == ElementTypes.Primal)
                {
                    result = -1;
                } 
            }
            else if(recipe.Ingredients.Contains(database.GetElement[hoverElement.ID]) && recipe.Ingredients.Contains(database.GetElement[mouseElement.ID]))
            {
                result = recipe.Result.ID;
            }
        }

        return result;
    }
}

[System.Serializable]
public class Inventory
{
    public Element[] elementSlots = new Element[5];
}
