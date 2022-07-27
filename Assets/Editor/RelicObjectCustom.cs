using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(RelicObject), true)]
public class RelicObjectCustom : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        RelicObject relicObject = (RelicObject) target;

        if(GUILayout.Button("Create File Paths", GUILayout.Height(25)))
        {
            relicObject.CreateFilePaths();
        }

        if(GUILayout.Button("Create Relic Behavior Script", GUILayout.Height(40)))
        {
            relicObject.CreateRelicBehaviorScript();
        }

        if(GUILayout.Button("Create Relic Behavior Prefab", GUILayout.Height(40)))
        {
            if(!EditorApplication.isCompiling)
            {
                relicObject.CreateRelicBehaviorPrefab();
            }
            else
            {
                Debug.Log("Script compiling, please wait.");
            }
            
        }

        if(GUILayout.Button("Open Behavior Scipt", GUILayout.Height(25)))
        { 
            var script = (MonoScript)AssetDatabase.LoadAssetAtPath(relicObject.permaScriptPath, typeof(MonoScript));
            AssetDatabase.OpenAsset(script);
        }
    }  
}
