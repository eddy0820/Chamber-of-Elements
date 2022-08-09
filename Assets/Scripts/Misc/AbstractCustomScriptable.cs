using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCustomScriptable : ScriptableObject
{
    [Header("File Paths")]
    [ReadOnly, HideInInspector] public string permaScriptPath = "";
    [ReadOnly, HideInInspector] public string permaPrefabPath = "";
    protected string permaNewName = "";

    public abstract void CreateFilePaths();
    public abstract void CreateBehaviorScript();
    public abstract void CreateBehaviorPrefab();
}
