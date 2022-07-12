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

            InitCharacter();
            ChangeAttacker(GameManager.Instance.Enemy);

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

            immunityAffinityTypes = null;
            immunityPassives = null;
            immunityElements = null;

            ChangeAttacker(null);

            Player.Instance.SetMinionExists(false);
        }
    }

    public void CreateMinion(MinionObject minionObject)
    {
        if(characterObject == null)
        {
            characterObject = minionObject;

            stats = new MinionStats(characterObject.BaseStats);

            InitCharacter();
            ChangeAttacker(GameManager.Instance.Enemy);

            Player.Instance.SetMinionExists(true);
        }   
    }
}
