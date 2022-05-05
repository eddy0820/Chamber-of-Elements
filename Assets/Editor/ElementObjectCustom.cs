using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(ElementObject), true)]
public class ElementObjectCustom : Editor
{
    string scriptPath = "";
    string prefabPath = "";
    string newName = "";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        ElementObject elementObject = (ElementObject) target;

        if(GUILayout.Button("Create Element Behavior Script", GUILayout.Height(50)))
        {
            elementObject.CreateElementBehaviorScript(out scriptPath, out prefabPath, out newName);
        }

        if(GUILayout.Button("Create Element Behavior Prefab", GUILayout.Height(50)))
        {
            if(!EditorApplication.isCompiling)
            {
                if(scriptPath != "" && File.Exists(scriptPath) == true)
                {
                    elementObject.CreateElementBehaviorPrefab(scriptPath, prefabPath, newName);
                }
                else
                {
                    Debug.Log("Create The Script First!");
                }
            }
            else
            {
                Debug.Log("Script compiling, please wait.");
            }
            
        }

        if(GUILayout.Button("Open Behavior Scipt", GUILayout.Height(50)))
        {
            if(scriptPath != "" && File.Exists(scriptPath) == true)
            {
                var script = (MonoScript)AssetDatabase.LoadAssetAtPath(scriptPath, typeof(MonoScript));
                AssetDatabase.OpenAsset(script);
            }
            else
            {
                Debug.Log("Create The Script First!");
            }
        }
    }

    
}
