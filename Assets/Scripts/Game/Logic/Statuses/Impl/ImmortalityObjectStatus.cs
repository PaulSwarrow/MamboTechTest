using StarterAssets.Game.Components;
using StarterAssets.Game.Data;

namespace StarterAssets.Game.Model.Impl
{
    public class ImmortalityObjectStatus : ObjectStatus
    {
        public ImmortalityObjectStatus(StatsComponent target, int duration, int period) : base(target, duration, period)
        {
        }

        public override void Interact(IObjectStatus other)
        {
            if(other.IsDamage) other.Finish("Immortality");
        }

        public override bool IsDamage => false;
        public override EffectType ImmuneTarget => EffectType.None;
        protected override void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}