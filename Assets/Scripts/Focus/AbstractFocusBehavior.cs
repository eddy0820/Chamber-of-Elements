using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFocusBehavior : MonoBehaviour
{
    public abstract void PerformFocus(FocusObject focus, Character character);

    public virtual void DoParticleEffectEnemy(Character character)
    {
        GameObject particle = Instantiate(((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticle, character.transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.textureSheetAnimation.SetSprite(0, ((EnemyObject) GameManager.Instance.Enemy.CharacterObject).HitParticleTexture);
        particleSystem.Play();
    }
}
