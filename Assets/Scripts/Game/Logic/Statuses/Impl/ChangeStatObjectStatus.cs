using Game.Components;
using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Logic.Statuses.Impl
{
    public class ChangeStatObjectStatus : ObjectStatus
    {
        private readonly ObjectStatId _statId;
        private readonly bool _merge;
        private readonly int _sign;
        private readonly EffectType _type;
        private int _value;

        public ChangeStatObjectStatus(StatsComponent target, ObjectStatId statId, int duration, int period, bool merge, EffectType type,
            int value) : base(target, duration, period)
        {
            _statId = statId;
            _merge = merge;
            _type = type;
            _value = value;
            _sign = type == EffectType.Heal ? 1 : -1;
        }

        public override void Interact(IObjectStatus other)
        {
            if (other is ChangeStatObjectStatus anotherDamage && _merge && _type == anotherDamage._type) 
            {
                MergeWith(anotherDamage);
                other.Finish("Merged");
            }

        }

        public override bool IsDamage => _sign < 0;
        public override string Info => $"{_type}: {_value} "+ LifeSpanInfo;

        public override EffectType ImmuneTarget => _type;

        protected override void Tick()
        {
            if (Target.HasStat(_statId))
            {
                Target[_statId] += _sign * _value;
            }
        }
        private void MergeWith(ChangeStatObjectStatus status)
        {
            if (Duration == -1 || status.Duration == -1) Duration = -1;//permanent status priority
            else Duration += status.Duration;
            Period = Mathf.Min(Period, status.Period, 1);
            _value = Mathf.Max(_value, status._value);
        }
    }
}