using System.Collections.Generic;
using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    /// <summary>
    /// Facade for all game entities. All object interactions should be though this component to keep the logic clear
    /// </summary>
    public interface IGameEntity 
    {
        void ApplyEffect(EffectSpec effect);
        
        IReadOnlyDictionary<ObjectStatId, ObjectStat> Stats { get; }
    }
}