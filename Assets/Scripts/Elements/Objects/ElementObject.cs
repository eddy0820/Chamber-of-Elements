using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ElementObject : AbstractCustomScriptable
{
    public new string name = "New Element Name";

    [Space(20)]
    [ReadOnly] public int ID = -1;
    [ReadOnly, SerializeField] ElementTypes type;
    public ElementTypes Type => type;

    [Header("General Info")]
    [SerializeField] AffinityTypes affinityType = AffinityTypes.None;
    public AffinityTypes AffinityType => affinityType;
    [SerializeField] AffinityTypes secondaryAffinityType = AffinityTypes.None;
    public AffinityTypes SecondaryAffinityType => secondaryAffinityType;
    [SerializeField] Sprite elementTexture;
    public Sprite ElementTexture => elementTexture;

    [Space(20)]

    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;

    [Header("Hit Particle")]
    [SerializeField] GameObject hitParticle;
    public GameObject HitParticle => hitParticle;
    [SerializeField] Sprite hitParticleTexture;
    public Sprite HitParticleTexture => hitParticleTexture;

    [Header("Damage")]
    [SerializeField] float damage = -1;
    public float Damage => damage;
    [SerializeField] bool doAttackInBehavior = false;
    public bool DoAttackInBehavior => doAttackInBehavior;

    [Header("Behavior")]
    [SerializeField] protected GameObject behavior;
    public AbstractElementBehavior Behavior => behavior.GetComponent<AbstractElementBehavior>();
    [SerializeField] BehaviorScriptEntries behaviorEntries;
    public BehaviorScriptEntries BehaviorEntries => behaviorEntries;

    private void Awake()
    {
        OnAwake();
    }

    public abstract void OnAwake();

    protected void SetType(ElementTypes _type)
    {
        type = _type;
    }

    [ContextMenu("Create File Paths")]
    public override void CreateFilePaths()
    {
        #if UNITY_EDITOR

        permaNewName = Regex.Replace(name, @"\s+", "");

        switch(type)
        {
            case ElementTypes.Primal:
                permaScriptPath = "Assets/Scripts/Elements/Element Behaviors/Primal/" + permaNewName + "Behavior.cs";
                permaPrefabPath = "Assets/Prefabs/ElementBehaviors/Primal/" + permaNewName + "Behavior.prefab";
                break;
            case ElementTypes.Primordial:
                permaScriptPath = "Assets/Scripts/Elements/Element Behaviors/Primordial/" + permaNewName + "Behavior.cs";
                permaPrefabPath = "Assets/Prefabs/ElementBehaviors/Primordial/" + permaNewName + "Behavior.prefab";
                break;
            case ElementTypes.Elemental:
                permaScriptPath = "Assets/Scripts/Elements/Element Behaviors/Elemental/" + permaNewName + "Behavior.cs";
                permaPrefabPath = "Assets/Prefabs/ElementBehaviors/Elemental/" + permaNewName + "Behavior.prefab";
                break;
            case ElementTypes.Physical:
                permaScriptPath = "Assets/Scripts/Elements/Element Behaviors/Physical/" + permaNewName + "Behavior.cs";
                permaPrefabPath = "Assets/Prefabs/ElementBehaviors/Physical/" + permaNewName + "Behavior.prefab";
                break;
            case ElementTypes.Arena:
                permaScriptPath = "Assets/Scripts/Elements/Element Behaviors/Arena/" + permaNewName + "Behavior.cs";
                permaPrefabPath = "Assets/Prefabs/ElementBehaviors/Arena/" + permaNewName + "Behavior.prefab";
                break;
            case ElementTypes.Utility:
                permaScriptPath = "Assets/Scripts/Elements/Element Behaviors/Utility/" + permaNewName + "Behavior.cs";
                permaPrefabPath = "Assets/Prefabs/ElementBehaviors/Utility/" + permaNewName + "Behavior.prefab";
                break;
            default:
                permaScriptPath = "";
                permaPrefabPath = "";
                break;
        }

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
                outfile.WriteLine("public class " + permaNewName + "Behavior : AbstractElementBehavior");
                outfile.WriteLine("{ ");
                outfile.WriteLine("    public override bool DoBehavior(ElementObject element, Character character)");
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

        #endif
    }

    public override void CreateBehaviorPrefab()
    {
        #if UNITY_EDITOR

        GameObject obj = new GameObject(permaNewName + "Behavior");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        obj.AddComponent(System.Type.GetType(permaNewName + "Behavior"));

        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, permaPrefabPath);  
        DestroyImmediate(obj);

        behavior = prefab;

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        #endif
    }
}
