using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] ElementDatabase database;
    public ElementDatabase Database => database;
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

    public void ReRollSpecificElement(ElementObject element)
    {
        for(int i = 0; i < container.elementSlots.Length; i++)
        {
            if(container.elementSlots[i].ID < 0)
            {
                container.elementSlots[i].UpdateSlot(new Element(element));
            }
        }
    }

    public int CanCombine(Element hoverElement, Element mouseElement)
    {
        if(hoverElement.ID == mouseElement.ID)
        {
            if(hoverElement.AffinityType != AffinityTypes.None)
            {
                AffinityObject affinity = GameManager.Instance.AffinityDatabase.GetAffinity[hoverElement.AffinityType];

                if(hoverElement.ID == affinity.RecipeElement.ID && Player.Instance.UnlockedAffinities.unlocked.Contains(affinity.Type))
                {
                    return -1;
                } 
            } 
        }

        foreach(ElementRecipeObject recipe in Player.Instance.UnlockedElementRecipes.unlocked)
        {  
            if(recipe.Ingredients.Contains(database.GetElement[hoverElement.ID]) && recipe.Ingredients.Contains(database.GetElement[mouseElement.ID]))
            {
                return recipe.Result.ID;
            }
        }

        return -2;
    }

    public MinionObject CanCombineMinion(Element hoverElement, Element mouseElement)
    {
        MinionObject minion = null;

        foreach(MinionRecipeObject recipe in Player.Instance.UnlockedMinionRecipes.unlocked)
        {
            if(recipe.CatalystElement.ID == hoverElement.ID || recipe.CatalystElement.ID == mouseElement.ID)
            {
                Element catalyst;
                Element otherElement;

                if(recipe.CatalystElement.ID == hoverElement.ID)
                {
                    catalyst = hoverElement;
                    otherElement = mouseElement;
                }
                else
                {
                    catalyst = mouseElement;
                    otherElement = hoverElement;
                }

                if(recipe.Ingredients.Contains(database.GetElement[otherElement.ID]))
                {
                    minion = recipe.Result;
                }
            }
        }

        return minion;
    }

    public RelicObject CanCombineRelic(Element hoverElement, Element mouseElement)
    {
        foreach(RelicRecipeObject recipe in Player.Instance.UnlockedRelicRecipes.unlocked)
        {
            if(recipe.Ingredients.Contains(database.GetElement[hoverElement.ID]) && recipe.Ingredients.Contains(database.GetElement[mouseElement.ID]))
            {
                return recipe.Result;
            }
        }

        return null;
    }

    public int FindElement(int id)
    {
        for(int i = 0; i < container.elementSlots.Length; i++)
        {
            if(container.elementSlots[i].ID == id)
            {
                return i;
            }
        }

        return -1;
    }

    public List<int> FindElements(int id)
    {
        List<int> elements = new List<int>();

        for(int i = 0; i < container.elementSlots.Length; i++)
        {
            if(container.elementSlots[i].ID == id)
            {
                elements.Add(i);
            }
        }

        if(elements.Count > 0)
        {
            return elements;
        }
        else
        {
            return null;
        }
    }

    public int FindFirstEmptySlot()
    {
        for(int i = 0; i < container.elementSlots.Length; i++)
        {
            if(container.elementSlots[i].ID == -1)
            {
                return i;
            }
        }

        return -1;
    }

    public void ClearElements()
    {
        container.elementSlots = new Element[GameManager.Instance.InterfaceCanvas.transform.GetChild(0).childCount];
    }
}

[System.Serializable]
public class Inventory
{
    public Element[] elementSlots = new Element[5];
}
