using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SapphireRingRelicBehavior : AbstractRelicBehavior
{ 
    public override void OnBattleBegin(RelicObject relic)
    {
        Player.Instance.SwitchAffinity(AffinityTypes.Water);
    }

    public override void OnRelicEquip(RelicObject relic) {}

    public override void OnRelicUnEquip(RelicObject relic) {}
}
