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

    //Queue<Battle> battles;
    //public Queue<Battle> Battles => battles;

    Queue<BattleSelection> battles;
    public Queue<BattleSelection> Battles => battles;

    public Chapter(ChapterObject _chapterObject, int _numBattles, int _numEliteBattles, int _numBattlesPlus, int _numEliteBattlesPlus)
    {
        System.Random rand = new System.Random();

        chapterObject = _chapterObject;
        numBattles = _numBattles;
        numEliteBattles = _numEliteBattles;
        numBattlesPlus = _numBattlesPlus;
        numEliteBattlesPlus = _numEliteBattlesPlus;

        battles = new Queue<BattleSelection>();

        battles.Enqueue(new BattleSelection(1, chapterObject, chapterObject.FirstBattleEnemyPool, BattleTypes.Starting, rand));

        for(int i = 0; i < numBattles; i++)
        {   
            battles.Enqueue(new BattleSelection(CalculateBranches(chapterObject.Battles.BranchChance, chapterObject.Battles.MaxBranches), chapterObject, chapterObject.BattleEnemyPool, BattleTypes.Battle, rand));
        }

        for(int i = 0; i < numEliteBattles; i++)
        {
            battles.Enqueue(new BattleSelection(CalculateBranches(chapterObject.EliteBattles.BranchChance, chapterObject.EliteBattles.MaxBranches), chapterObject, chapterObject.EliteBattleEnemyPool, BattleTypes.EliteBattle, rand));
        }

        battles.Enqueue(new BattleSelection(1, chapterObject, chapterObject.MiniBossEnemyPool, BattleTypes.MiniBoss, rand));

        for(int i = 0; i < numBattlesPlus; i++)
        {
            battles.Enqueue(new BattleSelection(CalculateBranches(chapterObject.BattlesPlus.BranchChance, chapterObject.BattlesPlus.MaxBranches), chapterObject, chapterObject.BattleEnemyPool, BattleTypes.BattlePlus, rand));
        }

        for(int i = 0; i < numEliteBattlesPlus; i++)
        {
            battles.Enqueue(new BattleSelection(CalculateBranches(chapterObject.EliteBattlesPlus.BranchChance, chapterObject.EliteBattlesPlus.MaxBranches), chapterObject, chapterObject.EliteBattleEnemyPool, BattleTypes.EliteBattlePlus, rand));
        }

        battles.Enqueue(new BattleSelection(1, chapterObject, chapterObject.BossEnemyPool, BattleTypes.Boss, rand));
    }

    public int CalculateBranches(float branchChance, int maxBranches)
    {
        return Random.Range(0.0f, 1.0f) <= branchChance ? Random.Range(2, maxBranches + 1) : 1; 
    }
}
