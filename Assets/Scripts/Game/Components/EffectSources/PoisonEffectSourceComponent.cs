using StarterAssets.Game.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets.Game.Components
{
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