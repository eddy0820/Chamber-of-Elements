using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dynamic Passive", menuName = "Passives/Immunity")]
public class ImmunityPassiveObject : PassiveObject 
{
    [Header("Immunity Passive Specific")]
    [SerializeField] bool immuneToAffinityTypes;
    public bool ImmuneToAffinityTypes => immuneToAffinityTypes;
    [SerializeField] List<AffinityTypes> immuneToAffinitesList;
    public List<AffinityTypes> ImmuneToAffinitiesList => immuneToAffinitesList;

    [Space(15)]
    [SerializeField] bool immuneToPassives;
    public bool ImmuneToPassives => immuneToPassives;
    [SerializeField] List<PassiveObject> immuneToPassivesList;
    public List<PassiveObject> ImmuneToPassivesList => immuneToPassivesList;

    [Space(15)]
    [SerializeField] bool immuneToElements;
    public bool ImmuneToElements => immuneToElements;
    [SerializeField] List<ElementObject> immuneToElementsList;
    public List<ElementObject> ImmuneToElementsList => immuneToElementsList;

    public override void TakeAffect(Character character)
    {
        if(immuneToAffinityTypes)
        {
            foreach(AffinityTypes type in immuneToAffinitesList)
            {
                character.immunityAffinityTypes.Add(type, this);
            } 
        }

        if(immuneToPassives)
        {
            foreach(PassiveObject passive in immuneToPassivesList)
            {
                character.immunityPassives.Add(passive, this);
            }
        }

        if(immuneToElements)
        {
            foreach(ElementObject element in immuneToElementsList)
            {
                character.immunityElements.Add(element, this);
            }
        }
    }

    public override void RemoveAffect(Character character) 
    {
        if(immuneToAffinityTypes)
        {
            foreach(KeyValuePair<AffinityTypes, ImmunityPassiveObject> type in character.immunityAffinityTypes)
            {
                if(type.Value.Name == this.Name)
                {
                    character.immunityAffinityTypes.Remove(type.Key);
                }
            } 
        }

        if(immuneToPassives)
        {
            foreach(KeyValuePair<PassiveObject, ImmunityPassiveObject> passive in character.immunityPassives)
            {
                if(passive.Value.Name == this.Name)
                {
                    character.immunityPassives.Remove(passive.Key);
                }
            } 
        }

        if(immuneToElements)
        {
            foreach(KeyValuePair<ElementObject, ImmunityPassiveObject> element in character.immunityElements)
            {
                if(element.Value.Name == this.Name)
                {
                    character.immunityElements.Remove(element.Key);
                }
            } 
        }
    }

}
