﻿using System;
using StarterAssets.Game.Logic;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(IEffectSource))]
    public class TriggerComponent : MonoBehaviour
    { 
        private IEffectSource[] _effectProvider;
        
        private void Awake()
        {
            _effectProvider = GetComponents<IEffectSource>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (GameUtils.GetEntity(other, out var effectTarget))
            {
                foreach (var effectSource in _effectProvider)
                {
                    effectSource.ApplyEffect(effectTarget);
                }
            }
        }
    }
}