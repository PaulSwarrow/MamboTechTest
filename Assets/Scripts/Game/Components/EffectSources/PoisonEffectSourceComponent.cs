using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Components.EffectSources
{
    /// <summary>
    /// Provides poison effect source.
    /// Todo: split effect source behavior and effect settings. Effect spec may be implemented with Odin inspector ObjectPicker
    /// </summary>
    public class PoisonEffectSourceComponent : MonoBehaviour, IEffectSource
    {
        [Tooltip("Set to -1 for permanent status")]
        [SerializeField] private int duration = 10;
        [SerializeField] private int period = 1;
        [SerializeField] private int damage = 10;

        public void ApplyEffect(IGameEntity effectTarget)
        {
            effectTarget.ApplyEffect(new EffectSpec(EffectType.Poison, EffectMode.Apply, duration, period, damage));
        }
    }
}