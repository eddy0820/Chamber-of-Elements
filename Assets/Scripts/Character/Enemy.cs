using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{ 
    private void Awake()
    {
        stats = new EnemyStats(characterObject.BaseStats);
        passivesInterface = GameManager.Instance.InterfaceCanvas.transform.GetChild(3).GetComponent<PassivesInterface>();
    
        InitCharacter();
    }
}
