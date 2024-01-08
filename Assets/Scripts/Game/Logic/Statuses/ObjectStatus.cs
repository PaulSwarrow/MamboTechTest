using StarterAssets.Game.Components;
using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Model
{
    public abstract class ObjectStatus : IObjectStatus
    {
        protected StatsComponent _target;

        protected int Duration;
        protected int Period;
        
        private bool _aborted;
        private float _totalTime;
        private int _count;

        protected ObjectStatus(StatsComponent target, int duration, int period)
        {
            _target = target;
            Duration = duration;
            Period = period;
        }

        public bool IsOver => Duration > 0 && _totalTime >= Duration || _aborted;
        public abstract bool IsDamage { get; }


        public void Start()
        {
            Tick();
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