using Game.Components;
using Game.Data;
using Game.Interfaces;

namespace Game.Logic.Statuses.Impl
{
    /// <summary>
    /// Provides immortality. Cancels any damaging statuses
    /// Assumption: immunity is applied first. Otherwise two sided status interaction must be implemented
    /// Task description says that immortality can not be gained during gameplay so the assumption is acceptable 
    /// </summary>
    public class ImmortalityObjectStatus : ObjectStatus
    {
        public ImmortalityObjectStatus(StatsComponent target, int duration, int period) : base(target, duration, period)
        {
        }

        public override void Interact(IObjectStatus other)
        {
            if(other.IsDamage) other.Finish("Immortality");
        }

        public override string Info => $"Immortal "+ LifeSpanInfo;
        public override bool IsDamage => false;
        public override EffectType ImmuneTarget => EffectType.None;
        protected override void Tick()
        {
        }
    }
}