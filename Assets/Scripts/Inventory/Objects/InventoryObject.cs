using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField] ElementDatabase database;
    public ElementDatabase Database => database;
    [SerializeField] Inventory container;
    public Inventory Container => container;

    public void ReRollElements()
    {
        System.Random rand  = new System.Random();

        ElementObject excludedElement = null;

        if(Player.Instance.AffinityType != AffinityTypes.None)
        {
            excludedElement = GameManager.Instance.AffinityDatabase.GetAffinity[Player.Instance.AffinityType].RecipeElement;
        }

        for(int i = 0; i < container.elementSlots.Length; i++)
        {
            Dictionary<int, int> IDAndAmount = new Dictionary<int, int>();

            for(int j = 0; j < container.elementSlots.Length; j++)
            {
                if(container.elementSlots[j].ID >= 0)
                {   
                    if(IDAndAmount.ContainsKey(container.elementSlots[j].ID))
                    {
                        IDAndAmount[container.elementSlots[j].ID]++;
                    }
                    else
                    {
                        IDAndAmount.Add(container.elementSlots[j].ID, 1);
                    }
                }
            }

            if(container.elementSlots[i].ID < 0)
            {
                container.elementSlots[i].UpdateSlot(new Element(RollElement(IDAndAmount, rand, excludedElement)));
            }
        }
    }

    private ElementObject RollElement(Dictionary<int, int> IDAndAmount, System.Random rand, ElementObject excludedElement)
    {   
        ElementObject element = database.GetElement[Player.Instance.ReRollElements.set.ElementAt(rand.Next(Player.Instance.ReRollElements.set.Count)).ID];

        if(excludedElement != null && excludedElement.ID == element.ID)
        {
            return RollElement(IDAndAmount, rand, excludedElement);
        }
        else
        {
            if(IDAndAmount.ContainsKey(element.ID))
            {
                if(IDAndAmount[element.ID] >= 2)
                {
                    return RollElement(IDAndAmount, rand, excludedElement);
                }
                else
                {
                    return element;
                }
            }
            else
            {
                return element;
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

                if(hoverElement.ID == affinity.RecipeElement.ID && Player.Instance.UnlockedAffinities.set.Contains(affinity.Type))
                {
                    return -1;
                } 
            } 
        }

        foreach(ElementRecipeObject recipe in Player.Instance.UnlockedElementRecipes.set)
        {  
            if((recipe.Ingredients.Contains(database.GetElement[hoverElement.ID]) && recipe.Ingredients.Contains(database.GetElement[mouseElement.ID])) && hoverElement.ID != mouseElement.ID)
            {
                return recipe.Result.ID;
            }
        }

        return -2;
    }

    public MinionObject CanCombineMinion(Element hoverElement, Element mouseElement)
    {
        MinionObject minion = null;

        if(Player.Instance.MinionExists)
        {
            return minion;
        }

        foreach(MinionRecipeObject recipe in Player.Instance.UnlockedMinionRecipes.set)
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
        foreach(RelicRecipeObject recipe in Player.Instance.UnlockedRelicRecipes.set)
        {
            if((recipe.Ingredients.Contains(database.GetElement[hoverElement.ID]) && recipe.Ingredients.Contains(database.GetElement[mouseElement.ID])) && hoverElement.ID != mouseElement.ID)
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
        foreach(Element e in container.elementSlots)
        {
            e.UpdateSlot(new Element());
        }
    }
}

[System.Serializable]
public class Inventory
{
    public Element[] elementSlots = new Element[5];
}
