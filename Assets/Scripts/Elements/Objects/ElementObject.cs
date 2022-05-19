using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

public abstract class ElementObject : ScriptableObject
{
    public new string name = "New Element Name";

    [Space(20)]
    [ReadOnly] public int ID = -1;
    [ReadOnly, SerializeField] ElementTypes type;
    public ElementTypes Type => type;

    [Header("General Info")]
    [SerializeField] AffinityTypes[] affinityTypes;
    public AffinityTypes[] AffinityTypes => affinityTypes;
    [SerializeField] Sprite elementTexture;
    public Sprite ElementTexture => elementTexture;

    [Space(20)]
    [TextArea(15, 20)]
    [SerializeField] string description;
    public string Description => description;

    [Header("Damage")]
    [SerializeField] float damage = -1;
    public float Damage => damage;
    [SerializeField] bool doAttackInBehavior = false;
    public bool DoAttackInBehavior => doAttackInBehavior;

    [Header("Behavior")]
    [SerializeField] protected GameObject behavior;
    public AbstractElementBehavior Behavior => behavior.GetComponent<AbstractElementBehavior>();
    [SerializeField] float extraValue = -1;
    public float ExtraValue => extraValue;
    [SerializeField] float secondaryExtraValue = -1;
    public float SecondaryExtraValue => secondaryExtraValue;

    [Space(15)]

    [SerializeField] ElementObject associatedElement;
    public ElementObject AssociatedElement => associatedElement;
    [SerializeField] ElementObject secondaryAssociatedElement;
    public ElementObject SecondaryAssociatedElement => secondaryAssociatedElement;

    [Space(15)]

    [SerializeField] PassiveObject associatedPassive;
    public PassiveObject AssociatedPassive => associatedPassive;
    [SerializeField] PassiveObject secondaryAssociatedPassive;
    public PassiveObject SecondaryAssociatedPassive => secondaryAssociatedPassive;
    [SerializeField] PassiveObject tertiaryAssociatedPassive;
    public PassiveObject TertiaryAssociatedPassive => tertiaryAssociatedPassive;
    
    [System.NonSerialized] public string permaScriptPath = "";
    [System.NonSerialized] public string permaPrefabPath = "";
    [System.NonSerialized] public string permaNewName = "";

    protected void SetType(ElementTypes _type)
    {
        type = _type;
    }

    public void CreateElementBehaviorScript()
    {
        string scriptPath;
        string prefabPath;
        string newName;

        newName = Regex.Replace(name, @"\s+", "");

        switch(type)
        {
            case ElementTypes.Primal:
                scriptPath = "Assets/Scripts/Elements/Element Behaviors/Primal/" + newName + "Behavior.cs";
                prefabPath = "Assets/Prefabs/Element Behaviors/Primal/" + newName + "Behavior.prefab";
                break;
            case ElementTypes.Primordial:
                scriptPath = "Assets/Scripts/Elements/Element Behaviors/Primordial/" + newName + "Behavior.cs";
                prefabPath = "Assets/Prefabs/Element Behaviors/Primordial/" + newName + "Behavior.prefab";
                break;
            case ElementTypes.Elemental:
                scriptPath = "Assets/Scripts/Elements/Element Behaviors/Elemental/" + newName + "Behavior.cs";
                prefabPath = "Assets/Prefabs/Element Behaviors/Elemental/" + newName + "Behavior.prefab";
                break;
            case ElementTypes.Physical:
                scriptPath = "Assets/Scripts/Elements/Element Behaviors/Physical/" + newName + "Behavior.cs";
                prefabPath = "Assets/Prefabs/Element Behaviors/Physical/" + newName + "Behavior.prefab";
                break;
            case ElementTypes.Arena:
                scriptPath = "Assets/Scripts/Elements/Element Behaviors/Arena/" + newName + "Behavior.cs";
                prefabPath = "Assets/Prefabs/Element Behaviors/Arena/" + newName + "Behavior.prefab";
                break;
            case ElementTypes.Utility:
                scriptPath = "Assets/Scripts/Elements/Element Behaviors/Utility/" + newName + "Behavior.cs";
                prefabPath = "Assets/Prefabs/Element Behaviors/Utility/" + newName + "Behavior.prefab";
                break;
            default:
                scriptPath = "";
                prefabPath = "";
                break;
        }

        //prefabPath = AssetDatabase.GenerateUniqueAssetPath(prefabPath);
        
        if(File.Exists(scriptPath) == false)
        {
            using (StreamWriter outfile = new StreamWriter(scriptPath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using System.Collections;");
                outfile.WriteLine("using System.Collections.Generic;");
                outfile.WriteLine("");
                outfile.WriteLine("public class " + newName + "Behavior : AbstractElementBehavior");
                outfile.WriteLine("{ ");
                outfile.WriteLine("    public override bool DoBehavior(ElementObject element)");
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

        permaScriptPath = scriptPath;
        permaPrefabPath = prefabPath;
        permaNewName = newName;
    }

    public void CreateElementBehaviorPrefab()
    {
        if(permaPrefabPath != "" && File.Exists(permaPrefabPath) == false)
        {
            GameObject obj = new GameObject(permaNewName + "Behavior");
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;

            obj.AddComponent(System.Type.GetType(permaNewName + "Behavior"));
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(obj, permaPrefabPath);  
            DestroyImmediate(obj);

            behavior = prefab;
        }
        else
        {
            Debug.Log("Behavior Prefab Already Exists!");
        } 
    }
}
