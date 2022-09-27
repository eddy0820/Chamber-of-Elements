using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chapter", menuName = "Chapter")]
public class ChapterObject : ScriptableObject
{
    [SerializeField] new string name;
    public string Name => name;
    [SerializeField] Sprite background;
    public Sprite Background => background;

    [Space(15)]
    
    [SerializeField] BattleSelectionInfo battles;
    public BattleSelectionInfo Battles => battles;
    [SerializeField] BattleSelectionInfo eliteBattles;
    public BattleSelectionInfo EliteBattles => eliteBattles;
    [SerializeField] BattleSelectionInfo battlesPlus;
    public BattleSelectionInfo BattlesPlus => battlesPlus;
    [SerializeField] BattleSelectionInfo eliteBattlesPlus;
    public BattleSelectionInfo EliteBattlesPlus => eliteBattlesPlus;

    [Space(15)]

    [SerializeField] List<EnemyObject> firstBattleEnemyPool;
    public List<EnemyObject> FirstBattleEnemyPool => firstBattleEnemyPool;
    [SerializeField] List<EnemyObject> battleEnemyPool;
    public List<EnemyObject> BattleEnemyPool => battleEnemyPool;
    [SerializeField] List<EnemyObject> eliteBattleEnemyPool;
    public List<EnemyObject> EliteBattleEnemyPool => eliteBattleEnemyPool;
    [SerializeField] List<EnemyObject> miniBossEnemyPool;
    public List<EnemyObject> MiniBossEnemyPool => miniBossEnemyPool;
    [SerializeField] List<EnemyObject> bossEnemyPool;
    public List<EnemyObject> BossEnemyPool => bossEnemyPool;
    
    [System.Serializable]
    public class BattleSelectionInfo
    {
        [Header("Num Battle Ranges")]
        [SerializeField] int min;
        public int Min => min;

        [SerializeField] int max;
        public int Max => max;

        [Header("Branches")]

        [Range(0.0f, 1.0f)]
        [SerializeField] float branchChance = 0.5f;
        public float BranchChance => branchChance;
        
        [Range(1, 5)]
        [SerializeField] int maxBranches;
        public int MaxBranches => maxBranches;
    }
}
