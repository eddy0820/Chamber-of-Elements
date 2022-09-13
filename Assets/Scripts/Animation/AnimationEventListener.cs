using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventListener : MonoBehaviour
{
    public void EnemyAttack()
    {
        if(!Player.Instance.MinionExists)
        {
            if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["HitChance"].value)
            {
                DamageIndicatorController.Instance.DoMissIndicator(Player.Instance.transform.position);
            }
            else if(Player.Instance.IsImmuneAffinity(GameManager.Instance.Enemy.AffinityType) == false)
            {
                Player.Instance.Stats.TakeDamage(GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, GameManager.Instance.Enemy);
                ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Medium);

                if(((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticle != null)
                {
                    GameObject particle = Instantiate(((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticle, Player.Instance.transform.position, Quaternion.identity);
                    ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
                    particleSystem.textureSheetAnimation.SetSprite(0, ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticleTexture);
                    particleSystem.Play();
                }
            }
        }
        else
        {
            if(UnityEngine.Random.Range(0, 101) > GameManager.Instance.Enemy.Stats.Stats["HitChance"].value)
            {
                DamageIndicatorController.Instance.DoMissIndicator(Player.Instance.Minion.transform.position);
            }
            else if(Player.Instance.Minion.IsImmuneAffinity(GameManager.Instance.Enemy.AffinityType) == false)
            {
                Player.Instance.Minion.Stats.TakeDamage(GameManager.Instance.Enemy.Stats.Stats["BasicAttack"].value, GameManager.Instance.Enemy.AffinityType, GameManager.Instance.Enemy);
                ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Medium);
                
                if(((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticle != null)
                {
                    GameObject particle = Instantiate(((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticle, Player.Instance.transform.position, Quaternion.identity);
                    ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
                    particleSystem.textureSheetAnimation.SetSprite(0, ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticleTexture);
                    particleSystem.Play();
                }
            }
        }
    }

    public void EnemyAttackFocus()
    {
        ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus.Behavior.PerformFocus(((EnemyObject) GameManager.Instance.Enemy.CharacterObject).Focus, GameManager.Instance.Enemy);
    }

    public void EnemyFinishedAttacking()
    {
        GameManager.Instance.gameObject.GetComponent<EnemyTurnGameState>().finishedAttacking = true;
    }

    public void MinionAttack()
    {
        if(Player.Instance.Minion.Stats.Stats.ContainsKey("FocusCooldown") && Player.Instance.Minion.Stats.Stats.ContainsKey("CanFocus") && Player.Instance.Minion.Stats.Stats.ContainsKey("FocusHitChance"))
        {
            MinionTurnGameState minionTurnGameState = GameManager.Instance.gameObject.GetComponent<MinionTurnGameState>();

            if(minionTurnGameState.currentFocusCounter < Player.Instance.Minion.Stats.Stats["FocusCooldown"].value)
            {
                DoMinionAttackHelper();
                
                minionTurnGameState.currentFocusCounter++;
            }
            else
            {
                if(Player.Instance.Minion.Stats.Stats["CanFocus"].value > 0)
                {
                    if(UnityEngine.Random.Range(0, 101) > Player.Instance.Minion.Stats.Stats["FocusHitChance"].value)
                    {
                        foreach(CharacterEntry characterEntry in ((MinionObject) Player.Instance.Minion.CharacterObject).Focus.BehaviorEntries.FocusAffectedCharacters)
                        {
                            Character character = GameManager.Instance.ConvertCharacterEntry(characterEntry);

                            if(character.CharacterObject != null)
                            {
                                DamageIndicatorController.Instance.DoMissIndicator(character.transform.position);
                            }
                        }    
                    }
                    else
                    {
                        ((MinionObject) Player.Instance.Minion.CharacterObject).Focus.Behavior.PerformFocus(((MinionObject) Player.Instance.Minion.CharacterObject).Focus, Player.Instance.Minion);
                        ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Large);
                    }

                    minionTurnGameState.currentFocusCounter = 0;
                }
                else
                {
                    DoMinionAttackHelper();
                } 
            }
        }
        else
        {
            DoMinionAttackHelper();
        } 
    }

    private void DoMinionAttackHelper()
    {
        if(UnityEngine.Random.Range(0, 101) > Player.Instance.Minion.Stats.Stats["HitChance"].value)
        {
            DamageIndicatorController.Instance.DoMissIndicator(GameManager.Instance.Enemy.transform.position);
        }
        else if(GameManager.Instance.Enemy.IsImmuneAffinity(Player.Instance.Minion.AffinityType) == false)
        {
            GameManager.Instance.Enemy.Stats.TakeDamage(Player.Instance.Minion.Stats.Stats["BasicAttack"].value, Player.Instance.Minion.AffinityType, Player.Instance.Minion);
            ScreenShakeBehavior.Instance.StartShake(ScreenShakeBehavior.ShakePresets.Medium);
        }
    }
}
