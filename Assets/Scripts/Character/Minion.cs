using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Character
{
    private void Awake()
    {
        if(characterObject != null)
        {
            stats = new MinionStats(characterObject.BaseStats, HPText, this);

            ChangeAttacker(GameManager.Instance.Enemy);
            InitCharacter();
        
            Player.Instance.SetMinionExists(true);

            HPText.SetActive(true);
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = null;
            GetComponentInChildren<Animator>().runtimeAnimatorController = null;

            SwitchAffinity(AffinityTypes.None);

            HPText.SetActive(false);
        }
    }

    public void NullifyMinion()
    {
        if(characterObject != null)
        {
            foreach(MinionObject.PassiveEntryTarget characterPassive in ((MinionObject) characterObject).PassivesToGiveCharacters) 
            {
                Character character = ElementBehaviorUtil.ConvertCharacterEntry(characterPassive.character);

                character.RemovePassive(characterPassive.passive);
            } 

            characterObject = null;
            stats = null;

            GetComponentInChildren<SpriteRenderer>().sprite = null;
            GetComponentInChildren<Animator>().runtimeAnimatorController = null;

            SwitchAffinity(AffinityTypes.None);

            PassiveObject[] deletePassives = new PassiveObject[passives.Count];
            passives.CopyTo(deletePassives);

            foreach(PassiveObject passive in deletePassives)
            {
                RemovePassive(passive);
            }   
 
            passives = new HashSet<PassiveObject>();

            actionsToDoStartOfEveryTurn = null;
            actionsToDoEndOfEveryTurn = null;
            actionsToDoOnHit = null;
            passivesWithTurns = null;
            minDynamicPassivesList = null;

            immunityAffinityTypes = null;
            immunityPassives = null;
            immunityElements = null;

            ChangeAttacker(null);

            CharactersInterface.Instance.minionText.text = "";

            Player.Instance.SetMinionExists(false);

            HPText.SetActive(false);

            particles = null;
        }
    }

    public void CreateMinion(MinionObject minionObject)
    {
        if(characterObject == null)
        {
            characterObject = minionObject;

            stats = new MinionStats(characterObject.BaseStats, HPText, this);

            ChangeAttacker(GameManager.Instance.Enemy);
            InitCharacter();
        
            Player.Instance.SetMinionExists(true);

            HPText.SetActive(true);
        }   
    }

    protected override void InitCharacter()
    {
        base.InitCharacter();

        foreach(MinionObject.PassiveEntryTarget characterPassive in ((MinionObject) characterObject).PassivesToGiveCharacters)
        {
            Character character = ElementBehaviorUtil.ConvertCharacterEntry(characterPassive.character);

            if(characterPassive.passive is FlatPassiveObject)
            {
                character.AddFlatPassive((FlatPassiveObject) characterPassive.passive);
            }
            else if(characterPassive.passive is DynamicPassiveObject)
            {
                character.AddDynamicPassive(((DynamicPassiveObject) characterPassive.passive), characterPassive.value, false, false);
            }
            else if(characterPassive.passive is ImmunityPassiveObject)
            {
                character.AddImmunityPassive((ImmunityPassiveObject) characterPassive.passive);
            }
        }
    }
}
