using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhilosophersStoneRelicBehavior : AbstractRelicBehavior, IOnClickRelic
{ 
    public override void OnBattleBegin(RelicObject relic) {}

    public override void OnRelicEquip(RelicObject relic) {}

    public override void OnRelicUnEquip(RelicObject relic) {}

    public void OnRelicClick(RelicObject relic, Element element)
    {
        ElementObject metal = relic.BehaviorEntries.Element1;
        ElementObject silver = relic.BehaviorEntries.Element2;
        ElementObject gold = relic.BehaviorEntries.Element3;
        
        ElementObject elementObject = null;

        if(element.ID == metal.ID)
        {
            elementObject = silver;
        }
        else if(element.ID == silver.ID)
        {
            elementObject = gold;
        }
        else if (element.ID == gold.ID)
        {
            elementObject = metal;
        }

        element.UpdateSlot(new Element(elementObject));
        GameManager.Instance.mouseElement.obj.GetComponent<Image>().sprite = elementObject.ElementTexture; 
        //GameManager.Instance.mouseElement.obj.GetComponent<Animator>().SetTrigger("NewElement");
    } 
}
