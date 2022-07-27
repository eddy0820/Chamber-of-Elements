using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractRelicBehavior : MonoBehaviour
{
    public abstract void OnBattleBegin(RelicObject relic);
    public abstract void OnRelicEquip(RelicObject relic);
    public abstract void OnRelicUnEquip(RelicObject relic);


}

public interface IOnClickRelic
{
    public void OnRelicClick(RelicObject relic, Element element);
}
