using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Components.EffectSources
{
    /// <summary>
    /// Provides damage effect source.
    /// Todo: split effect source behavior and effect settings. Effect spec may be implemented with Odin inspector ObjectPicker
    /// </summary>
    public class DamageEffectSourceComponent : MonoBehaviour, IEffectSource
    {
        [SerializeField] private int damage = 10;

        public void ApplyEffect(IGameEntity effectTarget)
        {
            effectTarget.ApplyEffect(new EffectSpec(EffectType.Damage, damage));
        }
    }
}