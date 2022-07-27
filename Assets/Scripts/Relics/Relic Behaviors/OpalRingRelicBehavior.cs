using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpalRingRelicBehavior : AbstractRelicBehavior
{ 
    public override void OnBattleBegin(RelicObject relic) 
    {
        Player.Instance.SwitchAffinity(AffinityTypes.Air);
    }

    public override void OnRelicEquip(RelicObject relic) {}

    public override void OnRelicUnEquip(RelicObject relic) {}
}
