using StarterAssets.Game.Components;
using StarterAssets.Game.Data;

namespace StarterAssets.Game.Model.Impl
{
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