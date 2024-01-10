using Game.Components;
using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Logic.Statuses
{
    public abstract class ObjectStatus : IObjectStatus
    {
        protected StatsComponent _target;

        protected int Duration;
        protected int Period;
        
        private bool _aborted;
        protected float TotalTime;
        private int _count;

        protected ObjectStatus(StatsComponent target, int duration, int period)
        {
            _target = target;
            Duration = duration;
            Period = period;
        }

        public bool IsOver => Duration > 0 && TotalTime >= Duration || _aborted;
        public abstract bool IsDamage { get; }
        public abstract string Info { get; }

        protected string LifeSpanInfo
        {
            get
            {
                if (Duration > 0) return $"for {Mathf.CeilToInt(Duration - TotalTime)}s T: {Period}";
                if (Duration < 0) return $"permanently, T: {Period}";
                return "instantly";
            }
        }
        public void Start()
        {
            Tick();
            if (Duration == 0) _aborted = true;
        }

        public void Update(float deltaTime)
        {
            if (Duration == 0) return;
            TotalTime += deltaTime;

            if (Period > 0 && !_aborted)
            {
                var count = Mathf.FloorToInt(TotalTime / Period);
                if (count > _count)
                {
                    _count = count;
                    Tick();
                }
            }
        }

        public void Finish(string cause)
        {
            //TODO log cause
            _aborted = true;
        }

        /// <summary>
        /// React to new effect apply attempt. Remove status, merge or forbid new effect from being applied
        /// </summary>
        /// <param name="other">another status instance</param>
        /// <returns>If effect is allowed to be applied</returns>
        public abstract void Interact(IObjectStatus other);

        public abstract EffectType ImmuneTarget { get; }

        /// <summary>
        /// Apply the effect to the target
        /// </summary>
        protected abstract void Tick();
    }
}