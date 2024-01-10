using Game.Components;
using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Logic.Statuses
{
    public abstract class ObjectStatus : IObjectStatus
    {
        protected readonly StatsComponent Target;
        protected int Duration;
        protected int Period;

        private bool _aborted;
        private float _totalTime;
        private int _count;

        protected ObjectStatus(StatsComponent target, int duration, int period)
        {
            Target = target;
            Duration = duration;
            Period = period;
        }

        public bool IsOver => Duration > 0 && _totalTime >= Duration || _aborted;
        public abstract bool IsDamage { get; }
        public abstract string Info { get; }

        protected string LifeSpanInfo
        {
            get
            {
                if (Duration > 0) return $"for {Mathf.CeilToInt(Duration - _totalTime)}s T: {Period}";
                if (Duration < 0) return $"permanently, T: {Period}";
                return "instantly";
            }
        }

        public void Start()
        {
            if(_aborted) return;//may be cancelled before start!
            Tick();
            if (Duration == 0) _aborted = true;
        }

        public void Update(float deltaTime)
        {
            if (Duration == 0) return;
            _totalTime += deltaTime;

            if (Period > 0 && !_aborted)
            {
                var count = Mathf.FloorToInt(_totalTime / Period);
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