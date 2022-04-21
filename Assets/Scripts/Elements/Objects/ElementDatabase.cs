using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element Database", menuName = "Databases/Element")]
public class ElementDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField] ElementObject[] elements;
    Dictionary<int, ElementObject> getElement = new Dictionary<int, ElementObject>();
    public Dictionary<int, ElementObject> GetElement => getElement;

    public void InitElements()
    {
        getElement = new Dictionary<int, ElementObject>();

        for(int i = 0; i < elements.Length; i++)
        {
            getElement.Add(i, elements[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < elements.Length; i++)
        {
            elements[i].ID = i;
        }
    }

    public void OnBeforeSerialize() {}

    [ContextMenu("Initialize Element IDs")]
    private void InitializeElementIDs()
    {
        getElement = new Dictionary<int, ElementObject>();

        for(int i = 0; i < elements.Length; i++)
        {
            Debug.Log("Giving " + elements[i].name + " an id of " + i + ".\n");
            elements[i].ID = i;
        }
    }
}

