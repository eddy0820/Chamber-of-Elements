using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSelectScreen : AbstractGameInterface
{
    [Header("Databases")]
    [SerializeField] protected CharacterDatabase characterDatabase;
    [SerializeField] protected AffinityDatabase affinityDatabase;
    [SerializeField] protected DataHolder dataHolder;

    [Header("Character Card Settings")]
    [SerializeField] protected GameObject characterCardPrefab;
    [SerializeField] protected GameObject characterSelectGrid;
    [SerializeField] protected int characterCardAmount = 3;

    [Header("Card Color")]
    [SerializeField] protected Color defaultColor;
    [SerializeField] protected Color hoverColor;
    [SerializeField] protected Color selectedColor;

    [Header("Used Stats for Card")]
    [SerializeField] protected StatTypeObject maxHealthStatType;
    [SerializeField] protected StatTypeObject basicAttackStatType;
}
