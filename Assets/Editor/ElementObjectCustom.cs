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

        if(GUILayout.Button("Create Element Behavior Script", GUILayout.Height(50)))
        {
            elementObject.CreateElementBehaviorScript();
        }

        if(GUILayout.Button("Create Element Behavior Prefab", GUILayout.Height(50)))
        {
            if(!EditorApplication.isCompiling)
            {
                if(File.Exists(elementObject.permaScriptPath))
                {
                    elementObject.CreateElementBehaviorPrefab();
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
            if(File.Exists(elementObject.permaScriptPath))
            {
                var script = (MonoScript)AssetDatabase.LoadAssetAtPath(elementObject.permaScriptPath, typeof(MonoScript));
                AssetDatabase.OpenAsset(script);
            }
            else
            {
                Debug.Log("Create The Script First!");
            }
        }
    }

    
}
