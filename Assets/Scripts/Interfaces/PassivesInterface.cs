using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class PassivesInterface : AbstractGameInterface
{
    [SerializeField] GameObject passiveUIPrefab;
    Dictionary<string, GameObject> passivesDisplayed = new Dictionary<string, GameObject>();

    protected override void OnAwake() {}

    protected override void Initialize() {}

    public void InitPassiveSlotUI(PassiveObject passive)
    {
        GameObject obj = Instantiate(passiveUIPrefab, transform.position, Quaternion.identity, gameObject.transform);

        obj.GetComponent<Image>().sprite = passive.PassiveTexture;

        var title = obj.transform.GetChild(0);
        var description = obj.transform.GetChild(1);

        TextMeshProUGUI textTitle = title.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textDescription = description.GetComponent<TextMeshProUGUI>();

        if(passive is FlatPassiveObject || passive is ImmunityPassiveObject)
        {
            textTitle.text = passive.Name;
            textDescription.text = passive.Description;
        }
        else if(passive is DynamicPassiveObject)
        {
            string newTitle;
            string newDescription = passive.Description;

            DynamicPassiveObject dynamicPassive = (DynamicPassiveObject) passive;

            if(dynamicPassive.StatModifyingPassive && (dynamicPassive.ModifierType == StatModifierTypes.PercentAdd || dynamicPassive.ModifierType == StatModifierTypes.PercentMult))
            {
                newTitle = passive.Name + "(" + Mathf.Abs((dynamicPassive.Value * 100)).ToString() + ")";
                newDescription = newDescription.Replace("'X'", Mathf.Abs((dynamicPassive.Value * 100)).ToString());
            }
            else
            {
                newTitle = passive.Name + "(" + Mathf.Abs(dynamicPassive.Value).ToString() + ")";
                newDescription = newDescription.Replace("'X'", Mathf.Abs(dynamicPassive.Value).ToString());
            }
            
            textTitle.text = newTitle;
            textDescription.text = newDescription;
        }
        
        title.gameObject.SetActive(false);
        description.gameObject.SetActive(false);

        AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnterSlot(obj); });
        AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExitSlot(obj); });

        passivesDisplayed.Add(passive.Name, obj);
    }

    public void RemovePassiveSlotUI(PassiveObject passive)
    {
        Destroy(passivesDisplayed[passive.Name]);
        passivesDisplayed.Remove(passive.Name);
    }

    public void UpdateDynamicPassiveUI(DynamicPassiveObject passive)
    {
        GameObject passiveObject = passivesDisplayed[passive.Name];

        TextMeshProUGUI textTitle = passiveObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI textDescription = passiveObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        string newTitle;
        string newDescription = passive.Description;

        if(passive.StatModifyingPassive && (passive.ModifierType == StatModifierTypes.PercentAdd || passive.ModifierType == StatModifierTypes.PercentMult))
        {
            newTitle = passive.Name + "(" + Mathf.Abs((((DynamicPassiveObject) passive).Value * 100)).ToString() + ")";
            newDescription = newDescription.Replace("'X'", Mathf.Abs((((DynamicPassiveObject) passive).Value * 100)).ToString());
        }
        else
        {
            newTitle = passive.Name + "(" + Mathf.Abs(((DynamicPassiveObject) passive).Value).ToString() + ")";
            newDescription = newDescription.Replace("'X'", Mathf.Abs(((DynamicPassiveObject) passive).Value).ToString());
        }
        
        textTitle.text = newTitle;
        textDescription.text = newDescription;
    }

    private void OnEnterSlot(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(true);
        obj.transform.GetChild(1).gameObject.SetActive(true);
    }

    private void OnExitSlot(GameObject obj)
    {
        obj.transform.GetChild(0).gameObject.SetActive(false);
        obj.transform.GetChild(1).gameObject.SetActive(false);
    }

    protected override void UpdateInterface() {}
    protected override void UpdateMouseObjectTransform() {}
}
