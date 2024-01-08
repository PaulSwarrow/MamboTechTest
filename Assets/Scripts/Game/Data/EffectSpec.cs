using UnityEngine;

namespace StarterAssets.Game.Data
{
    public struct EffectSpec
    {
        public EffectType Type;
        public EffectMode Mode;
        [Tooltip("0: instant effect, -1: infinite status, >0: time in seconds")]
        public int Duration;
        public int Period;
        public int Value;
    }
}