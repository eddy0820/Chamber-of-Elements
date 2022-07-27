using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Character
{
    private void Awake()
    {
        if(characterObject != null)
        {
            stats = new MinionStats(characterObject.BaseStats);

            ChangeAttacker(GameManager.Instance.Enemy);
            InitCharacter();
        
            Player.Instance.SetMinionExists(true);
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = null;
            GetComponentInChildren<Animator>().runtimeAnimatorController = null;

            SwitchAffinity(AffinityTypes.None);
        }
    }

    public void NullifyMinion()
    {
        if(characterObject != null)
        {
            foreach(MinionObject.PassiveEntryTarget characterPassive in ((MinionObject) characterObject).PassivesToGiveCharacters) 
            {
                Character character = GameManager.Instance.ConvertCharacterEntry(characterPassive.character);

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

            GameManager.Instance.InterfaceCanvas.GetComponentInChildren<CharactersInterface>().minionText.text = "";

            Player.Instance.SetMinionExists(false);
        }
    }

    public void CreateMinion(MinionObject minionObject)
    {
        if(characterObject == null)
        {
            characterObject = minionObject;

            stats = new MinionStats(characterObject.BaseStats);

            ChangeAttacker(GameManager.Instance.Enemy);
            InitCharacter();
        
            Player.Instance.SetMinionExists(true);
        }   
    }

    protected override void InitCharacter()
    {
        base.InitCharacter();

        foreach(MinionObject.PassiveEntryTarget characterPassive in ((MinionObject) characterObject).PassivesToGiveCharacters)
        {
            Character character = GameManager.Instance.ConvertCharacterEntry(characterPassive.character);

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
