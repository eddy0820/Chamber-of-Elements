using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "New Relic", menuName = "Relic")]
public class RelicObject : AbstractCustomScriptable
{
    [SerializeField] new string name;
    public string Name => name;
    
    [SerializeField] Sprite relicTexture;
    public Sprite RelicTexture => relicTexture;

    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;

    [Header("Behavior")]
    [SerializeField] protected GameObject behavior;
    public AbstractRelicBehavior Behavior => behavior.GetComponent<AbstractRelicBehavior>();
    [SerializeField] BehaviorScriptEntries behaviorEntries;
    public BehaviorScriptEntries BehaviorEntries => behaviorEntries;


    [ContextMenu("Create File Paths")]
    public override void CreateFilePaths()
    {
        permaNewName = Regex.Replace(name, @"\s+", "");
        permaNewName = Regex.Replace(permaNewName, "'", "");

        permaScriptPath = "Assets/Scripts/Relics/Relic Behaviors/" + permaNewName + "RelicBehavior.cs";
        permaPrefabPath = "Assets/Prefabs/RelicBehaviors/" + permaNewName + "RelicBehavior.prefab";
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
                outfile.WriteLine("public class " + permaNewName + "RelicBehavior : AbstractRelicBehavior");
                outfile.WriteLine("{ ");
                outfile.WriteLine("    public override void OnBattleBegin(RelicObject relic)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        throw new System.NotImplementedException();");
                outfile.WriteLine("    }"); 
                outfile.WriteLine("");
                outfile.WriteLine("    public override void OnRelicEquip(RelicObject relic)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        throw new System.NotImplementedException();");
                outfile.WriteLine("    }"); 
                outfile.WriteLine(""); 
                outfile.WriteLine("    public override void OnRelicUnEquip(RelicObject relic)");
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
        GameObject obj = new GameObject(permaNewName + "RelicBehavior");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        obj.AddComponent(System.Type.GetType(permaNewName + "RelicBehavior"));

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, permaPrefabPath);  
        DestroyImmediate(obj);

        behavior = prefab;

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
