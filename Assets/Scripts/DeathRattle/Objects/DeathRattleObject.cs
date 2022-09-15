using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Death Rattle", menuName = "Death Rattle")]
public class DeathRattleObject : AbstractCustomScriptable
{
    [SerializeField] protected new string name;
    public string Name => name;

    [Header("Behavior")]
    [SerializeField] protected GameObject behavior;
    public AbstractDeathRattle Behavior => behavior.GetComponent<AbstractDeathRattle>();
    [SerializeField] BehaviorScriptEntries behaviorEntries;
    public BehaviorScriptEntries BehaviorEntries => behaviorEntries;

    [ContextMenu("Create File Paths")]
    public override void CreateFilePaths()
    {
        #if UNITY_EDITOR

        permaNewName = Regex.Replace(name, @"\s+", "");

        permaScriptPath = "Assets/Scripts/DeathRattle/" + permaNewName + "DRBehavior.cs";
        permaPrefabPath = "Assets/Prefabs/DeathRattles/" + permaNewName + "DRBehavior.prefab"; 

        #endif   
    }

    public override void CreateBehaviorScript()
    {   
        #if UNITY_EDITOR

        if(File.Exists(permaScriptPath) == false)
        {
            using (StreamWriter outfile = new StreamWriter(permaScriptPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections;");
                outfile.WriteLine("using System.Collections.Generic;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + permaNewName + "DRBehavior : AbstractDeathRattle");
                outfile.WriteLine("{ ");
                outfile.WriteLine("    public override void DoBehavior(Character character, DeathRattleObject deathRattle)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        throw new System.NotImplementedException();");
                outfile.WriteLine("    }");         
                outfile.WriteLine("}");
            }
            
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log("DR Behavior Script Already Exists!");
        }

        #endif
    }

    public override void CreateBehaviorPrefab()
    {
        #if UNITY_EDITOR

        GameObject obj = new GameObject(permaNewName + "DRBehavior");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        obj.AddComponent(System.Type.GetType(permaNewName + "DRBehavior"));

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, permaPrefabPath);  
        DestroyImmediate(obj);

        behavior = prefab;

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        #endif
    }
}
