using UnityEngine;
using UnityEditor;
using System.IO;

public abstract class ScriptCustom : Editor
{
    SerializedProperty permaScriptPath;
    SerializedProperty permaPrefabPath;

    void OnEnable()
    {
        permaScriptPath = serializedObject.FindProperty("permaScriptPath");
        permaPrefabPath = serializedObject.FindProperty("permaPrefabPath");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        serializedObject.Update();
        EditorGUILayout.PropertyField(permaScriptPath);
        EditorGUILayout.PropertyField(permaPrefabPath);
        serializedObject.ApplyModifiedProperties();

        AbstractCustomScriptable obj = (AbstractCustomScriptable) target;

        if(GUILayout.Button("Create File Paths", GUILayout.Height(25)))
        {
            obj.CreateFilePaths();
        }

        if(GUILayout.Button("Create Behavior Script", GUILayout.Height(40)))
        {
            obj.CreateBehaviorScript();
        }

        if(GUILayout.Button("Create Behavior Prefab", GUILayout.Height(40)))
        {
            if(!EditorApplication.isCompiling)
            {
                obj.CreateBehaviorPrefab();
            }
            else
            {
                Debug.Log("Script compiling, please wait.");
            }
            
        }

        if(GUILayout.Button("Open Behavior Scipt", GUILayout.Height(25)))
        { 
            var script = (MonoScript)AssetDatabase.LoadAssetAtPath(obj.permaScriptPath, typeof(MonoScript));
            AssetDatabase.OpenAsset(script);
        }
    }  
}
