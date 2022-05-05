using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClarityBehavior : AbstractElementBehavior
{
    public override bool DoBehavior(ElementObject element)
    {
        AffinityTypes currentAffinity = GameManager.Instance.Player.AffinityType;
        AffinityTypes affinityType = GenerateAffinity();
        
        while(currentAffinity == affinityType)
        {
            affinityType = GenerateAffinity();
        }

        GameManager.Instance.Player.SwitchAffinity(affinityType);
        GameManager.Instance.Player.UpdateAffinitySprite(affinityType);

        return true;
    }

    private AffinityTypes GenerateAffinity()
    {
        int affinity = Random.Range(1, 6);
        AffinityTypes affinityType = AffinityTypes.None;

        switch(affinity) 
        {
            case 1:
                affinityType = AffinityTypes.Water;
                break;
            case 2:
                affinityType = AffinityTypes.Fire;
                break;
            case 3:
                affinityType = AffinityTypes.Earth;
                break;
            case 4:
                affinityType = AffinityTypes.Air;
                break;
            case 5:
                affinityType = AffinityTypes.None;
                break;
        }

        return affinityType;
    }
}
