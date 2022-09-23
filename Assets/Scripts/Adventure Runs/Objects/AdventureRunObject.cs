using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Adventure Run", menuName = "Adventure Run")]
public class AdventureRunObject : ScriptableObject
{
    [SerializeField] List<ChapterObject> chapters;
    public List<ChapterObject> Chapters => chapters;
}
