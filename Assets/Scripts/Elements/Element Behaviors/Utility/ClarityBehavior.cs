using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarityBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element, Character character)
    {
        AffinityTypes currentAffinity = Player.Instance.AffinityType;
        AffinityTypes affinityType = GenerateAffinity();
        
        while(currentAffinity == affinityType)
        {
            affinityType = GenerateAffinity();
        }

        Player.Instance.SwitchAffinity(affinityType);

        return true;
    }

    private AffinityTypes GenerateAffinity()
    {
        System.Random rand  = new System.Random();
        int affinityNum = rand.Next(Player.Instance.UnlockedAffinities.set.Count + 1);

        try
        {
            Player.Instance.UnlockedAffinities.set.ElementAt(affinityNum);
        }
        catch
        {
            return AffinityTypes.None;
        }

        return GameManager.Instance.AffinityDatabase.GetAffinity[Player.Instance.UnlockedAffinities.set.ElementAt(affinityNum)].Type;
    }
}
