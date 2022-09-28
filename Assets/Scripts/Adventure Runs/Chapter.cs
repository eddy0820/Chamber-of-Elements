using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Chapter
{
    ChapterObject chapterObject;
    public ChapterObject ChapterObject => chapterObject;

    int numBattles;
    int numEliteBattles;
    int numBattlesPlus;
    int numEliteBattlesPlus;

    Queue<BattleSelection> battleSelections;
    public Queue<BattleSelection> BattleSelections => battleSelections;

    Queue<BattleSelection> battleSelectionsTotal;
    public Queue<BattleSelection> BattleSelectionsTotal => battleSelectionsTotal;
    
    public Chapter(ChapterObject _chapterObject, int _numBattles, int _numEliteBattles, int _numBattlesPlus, int _numEliteBattlesPlus)
    {
        System.Random rand = new System.Random();

        chapterObject = _chapterObject;
        numBattles = _numBattles;
        numEliteBattles = _numEliteBattles;
        numBattlesPlus = _numBattlesPlus;
        numEliteBattlesPlus = _numEliteBattlesPlus;

        battleSelections = new Queue<BattleSelection>();

        battleSelections.Enqueue(new BattleSelection(1, chapterObject, chapterObject.FirstBattleEnemyPool, BattleTypes.Starting, rand));

        for(int i = 0; i < numBattles; i++)
        {   
            battleSelections.Enqueue(new BattleSelection(CalculateBranches(chapterObject.Battles.BranchChance, chapterObject.Battles.MaxBranches), chapterObject, chapterObject.BattleEnemyPool, BattleTypes.Battle, rand));
        }

        for(int i = 0; i < numEliteBattles; i++)
        {
            battleSelections.Enqueue(new BattleSelection(CalculateBranches(chapterObject.EliteBattles.BranchChance, chapterObject.EliteBattles.MaxBranches), chapterObject, chapterObject.EliteBattleEnemyPool, BattleTypes.EliteBattle, rand));
        }

        battleSelections.Enqueue(new BattleSelection(1, chapterObject, chapterObject.MiniBossEnemyPool, BattleTypes.MiniBoss, rand));

        for(int i = 0; i < numBattlesPlus; i++)
        {
            battleSelections.Enqueue(new BattleSelection(CalculateBranches(chapterObject.BattlesPlus.BranchChance, chapterObject.BattlesPlus.MaxBranches), chapterObject, chapterObject.BattleEnemyPool, BattleTypes.BattlePlus, rand));
        }

        for(int i = 0; i < numEliteBattlesPlus; i++)
        {
            battleSelections.Enqueue(new BattleSelection(CalculateBranches(chapterObject.EliteBattlesPlus.BranchChance, chapterObject.EliteBattlesPlus.MaxBranches), chapterObject, chapterObject.EliteBattleEnemyPool, BattleTypes.EliteBattlePlus, rand));
        }

        battleSelections.Enqueue(new BattleSelection(1, chapterObject, chapterObject.BossEnemyPool, BattleTypes.Boss, rand));

        battleSelectionsTotal = new Queue<BattleSelection>(battleSelections);
    }

    public int CalculateBranches(float branchChance, int maxBranches)
    {
        return Random.Range(0.0f, 1.0f) <= branchChance ? Random.Range(2, maxBranches + 1) : 1; 
    }
}
