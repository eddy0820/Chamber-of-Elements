using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(ElementObject), true)]
public class ElementObjectCustom : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        ElementObject elementObject = (ElementObject) target;

        if(GUILayout.Button("Create File Paths", GUILayout.Height(25)))
        {
            elementObject.CreateFilePaths();
        }

        if(GUILayout.Button("Create Element Behavior Script", GUILayout.Height(40)))
        {
            elementObject.CreateElementBehaviorScript();
        }

        if(GUILayout.Button("Create Element Behavior Prefab", GUILayout.Height(40)))
        {
            if(!EditorApplication.isCompiling)
            {
                elementObject.CreateElementBehaviorPrefab();
            }
            else
            {
                Debug.Log("Script compiling, please wait.");
            }
            
        }

        if(GUILayout.Button("Open Behavior Scipt", GUILayout.Height(25)))
        { 
            var script = (MonoScript)AssetDatabase.LoadAssetAtPath(elementObject.permaScriptPath, typeof(MonoScript));
            AssetDatabase.OpenAsset(script);
        }
    }
}
