using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChaosAmuletRelicBehavior : AbstractRelicBehavior
{ 
    public override void OnBattleBegin(RelicObject relic) {}

    public override void OnRelicEquip(RelicObject relic)
    {
        Player.Instance.UnlockAffinity(AffinityTypes.Disorder);
    }

    public override void OnRelicUnEquip(RelicObject relic)
    {
        Player.Instance.LockAffinity(AffinityTypes.Disorder);
        
        if(Player.Instance.AffinityType is AffinityTypes.Disorder)
        {
            Player.Instance.SwitchAffinity(AffinityTypes.None);
        }
    }
}
