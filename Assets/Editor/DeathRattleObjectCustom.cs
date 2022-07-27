using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(DeathRattleObject), true)]
public class DeathRattleObjectCustom : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        DeathRattleObject drObject = (DeathRattleObject) target;

        if(GUILayout.Button("Create File Paths", GUILayout.Height(25)))
        {
            drObject.CreateFilePaths();
        }

        if(GUILayout.Button("Create Death Rattle Behavior Script", GUILayout.Height(40)))
        {
            drObject.CreateDRBehaviorScript();
        }

        if(GUILayout.Button("Create Death Rattle Behavior Prefab", GUILayout.Height(40)))
        {
            if(!EditorApplication.isCompiling)
            {
                drObject.CreateDRBehaviorPrefab();
            }
            else
            {
                Debug.Log("Script compiling, please wait.");
            }
            
        }

        if(GUILayout.Button("Open Behavior Scipt", GUILayout.Height(25)))
        { 
            var script = (MonoScript)AssetDatabase.LoadAssetAtPath(drObject.permaScriptPath, typeof(MonoScript));
            AssetDatabase.OpenAsset(script);
        }
    }  
}
