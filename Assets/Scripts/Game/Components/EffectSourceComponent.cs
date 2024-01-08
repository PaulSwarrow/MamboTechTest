using System.Collections.Generic;
using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class EffectSourceComponent : MonoBehaviour
    {
        [SerializeField] private EffectSpec[] _effects;

        public void ApplyEffect(IGameEntity effectTarget)
        {
            foreach (var spec in _effects)
            {
                effectTarget.ApplyEffect(spec);
            }
        }
    }
}