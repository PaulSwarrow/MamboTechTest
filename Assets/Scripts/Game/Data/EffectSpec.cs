using System;
using UnityEngine;

namespace StarterAssets.Game.Data
{
    [Serializable]
    public struct EffectSpec
    {
        public EffectType Type;
        [Tooltip("Type of status created by effect: immunity or stats change")]
        public EffectMode Mode;
        [Tooltip("0: instant effect, -1: infinite status, >0: time in seconds")]
        public int Duration;
        [Tooltip("How frequently status is ticking. In seconds")]
        public int Period;
        [Min(0)]
        public int Value;

        //Instant stat change effect
        public EffectSpec(EffectType type, int value) : this()
        {
            Type = type;
            Value = value;
        }

        //custom effect
        public EffectSpec(EffectType type, EffectMode mode, int duration, int period, int value)
        {
            Type = type;
            Mode = mode;
            Duration = duration;
            Period = period;
            Value = value;
        }
    }
}