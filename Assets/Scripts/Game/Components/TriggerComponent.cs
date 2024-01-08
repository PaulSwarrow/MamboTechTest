using System;
using StarterAssets.Game.Logic;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(EffectSourceComponent))]
    public class TriggerComponent : MonoBehaviour
    { 
        private EffectSourceComponent _effectProvider;
        
        private void Awake()
        {
            _effectProvider = GetComponent<EffectSourceComponent>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (GameUtils.GetEntity(other, out var effectTarget))
            {
                _effectProvider.ApplyEffect(effectTarget);
            }
        }
    }
}