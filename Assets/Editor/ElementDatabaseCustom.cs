using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ElementDatabase))]
public class ElementDatabaseCustom : Editor
{
    public override void OnInspectorGUI()
    {
        ElementDatabase database = (ElementDatabase) target;

        if(GUILayout.Button("Initialize Element IDs", GUILayout.Height(50)))
        {
            database.InitializeElementIDs();
        }

        DrawDefaultInspector();     
    }
}
