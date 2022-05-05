using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element Database", menuName = "Databases/Element")]
public class ElementDatabase : ScriptableObject
{
    [SerializeField] ElementObject[] elements;
    Dictionary<int, ElementObject> getElement = new Dictionary<int, ElementObject>();
    public Dictionary<int, ElementObject> GetElement => getElement;

    public void InitElements()
    {
        InitializeElementIDs();

        getElement = new Dictionary<int, ElementObject>();

        for(int i = 0; i < elements.Length; i++)
        {
            getElement.Add(i, elements[i]);
        }
    }

    [ContextMenu("Initialize Element IDs")]
    public void InitializeElementIDs()
    {
        for(int i = 0; i < elements.Length; i++)
        {
            //Debug.Log("Giving " + elements[i].name + " an id of " + i + ".\n");
            elements[i].ID = i;
        }
    }
}

