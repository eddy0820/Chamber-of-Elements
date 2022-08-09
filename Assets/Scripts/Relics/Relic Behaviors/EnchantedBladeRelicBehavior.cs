using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnchantedBladeRelicBehavior : AbstractRelicBehavior
{ 
    public override void OnBattleBegin(RelicObject relic) {}

    public override void OnRelicEquip(RelicObject relic)
    {
        Player.Instance.AddDynamicPassive((DynamicPassiveObject) Player.Instance.Relic.RelicObject.BehaviorEntries.Passive1.passive, Player.Instance.Relic.RelicObject.BehaviorEntries.Passive1.value, false, true);
    }

    public override void OnRelicUnEquip(RelicObject relic)
    {
        Player.Instance.SubtractDynamicPassive((DynamicPassiveObject) Player.Instance.Relic.RelicObject.BehaviorEntries.Passive1.passive, Player.Instance.Relic.RelicObject.BehaviorEntries.Passive1.value, true);
    }
}
