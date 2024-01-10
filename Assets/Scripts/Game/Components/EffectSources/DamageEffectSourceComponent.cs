using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class DamageEffectSourceComponent : MonoBehaviour, IEffectSource
    {
        [SerializeField] private int damage = 10;

        public void ApplyEffect(IGameEntity effectTarget)
        {
            effectTarget.ApplyEffect(new EffectSpec(EffectType.Damage, damage));
        }
    }
}