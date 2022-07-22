using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDeathRattle : MonoBehaviour
{
    public abstract void DoBehavior(Character character, DeathRattleObject deathRattle);
}
