using Game.Components;
using Game.Data;
using Game.Interfaces;

namespace Game.Logic.Statuses.Impl
{
    /// <summary>
    /// Provides immunity to specified effect.
    /// Assumption: immunity is applied first. Otherwise two sided status interaction must be implemented
    /// Task description says that immunity can not be gained during gameplay so the assumption is acceptable 
    /// </summary>
    public class ImmuneObjectStatus : ObjectStatus
    {
        private readonly EffectType _immuneTo;

        public ImmuneObjectStatus(StatsComponent target, int duration, int period, EffectType immuneTo) : base(target, duration, period)
        {
            _immuneTo = immuneTo;
        }


        public override void Interact(IObjectStatus other)
        {
            if (other.ImmuneTarget == _immuneTo)
            {
                other.Finish("By Immunity");
            }
        }

        public override bool IsDamage => false;
        public override string Info => $"Immune to: {_immuneTo} {LifeSpanInfo}";
        public override EffectType ImmuneTarget => EffectType.None;

        protected override void Tick()
        {
            
        }
    }
}