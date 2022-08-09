using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Focus", menuName = "Focus")]
public class FocusObject : AbstractCustomScriptable
{
    [SerializeField] new string name;
    public string Name => name;

    [Header("Behavior")]
    [SerializeField] protected GameObject behavior;
    public AbstractFocusBehavior Behavior => behavior.GetComponent<AbstractFocusBehavior>();
    [SerializeField] BehaviorScriptEntries behaviorEntries;
    public BehaviorScriptEntries BehaviorEntries => behaviorEntries;


    [ContextMenu("Create File Paths")]
    public override void CreateFilePaths()
    {
        permaNewName = Regex.Replace(name, @"\s+", "");

        permaScriptPath = "Assets/Scripts/Focus/Focus Behaviors/" + permaNewName + "FocusBehavior.cs";
        permaPrefabPath = "Assets/Prefabs/FocusBehaviors/" + permaNewName + "FocusBehavior.prefab";
    }

    public override void CreateBehaviorScript()
    {   
        if(File.Exists(permaScriptPath) == false)
        {
            using (StreamWriter outfile = new StreamWriter(permaScriptPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections;");
                outfile.WriteLine("using System.Collections.Generic;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + permaNewName + "FocusBehavior : AbstractFocusBehavior");
                outfile.WriteLine("{ ");
                outfile.WriteLine("    public override void PerformFocus(FocusObject focus, Character character)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        throw new System.NotImplementedException();");
                outfile.WriteLine("    }");         
                outfile.WriteLine("}");
            }
            
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log("Behavior Script Already Exists!");
        }
    }

    public override void CreateBehaviorPrefab()
    {
        GameObject obj = new GameObject(permaNewName + "FocusBehavior");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        obj.AddComponent(System.Type.GetType(permaNewName + "FocusBehavior"));

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, permaPrefabPath);  
        DestroyImmediate(obj);

        behavior = prefab;

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
