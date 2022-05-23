using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Character
{
    private void Awake()
    {
        stats = new MinionStats(characterObject.BaseStats);

        InitCharacter();
    }
}
