using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DivineAmuletRelicBehavior : AbstractRelicBehavior
{ 
    public override void OnBattleBegin(RelicObject relic) {}

    public override void OnRelicEquip(RelicObject relic)
    {
        Player.Instance.UnlockAffinity(AffinityTypes.Order);
    }

    public override void OnRelicUnEquip(RelicObject relic)
    {
        Player.Instance.LockAffinity(AffinityTypes.Order);
        
        if(Player.Instance.AffinityType is AffinityTypes.Order)
        {
            Player.Instance.SwitchAffinity(AffinityTypes.None);
        }
        
    }
}
