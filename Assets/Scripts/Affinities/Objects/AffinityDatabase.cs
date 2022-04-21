using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Affinity Database", menuName = "Databases/Affinity")]
public class AffinityDatabase : ScriptableObject
{
    [SerializeField] AffinityObject[] affinities;
    Dictionary<AffinityTypes, AffinityObject> getAffinity = new Dictionary<AffinityTypes, AffinityObject>();
    public Dictionary<AffinityTypes, AffinityObject> GetAffinity => getAffinity;

    public void InitAffinities()
    {
        getAffinity = new Dictionary<AffinityTypes, AffinityObject>();

        foreach(AffinityObject affinity in affinities)
        {
            getAffinity[affinity.Type] = affinity;
        }
    }
}
