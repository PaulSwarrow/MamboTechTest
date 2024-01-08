using System.Collections;
using System.Collections.Generic;
using StarterAssets.Game.Data;
using StarterAssets.Game.Factories;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(StatsComponent))]
    public class GameEntity : MonoBehaviour, IGameEntity
    {
        //Setup:
        [SerializeField] private bool isImmortal;
        
        
        private StatsComponent _stats;
        private readonly List<IObjectStatus> _currentStatuses = new ();
        //TODO inject
        private readonly StatusFactory _statusFactory = new ();

        private void Awake()
        {
            _stats = GetComponent<StatsComponent>();
        }

        private void Start()
        {
            if (isImmortal)
            {
                AddStatus(_statusFactory.CreateImmortality(_stats));
            }
        }

        public void ApplyEffect(EffectSpec effect)
        {
            AddStatus(_statusFactory.Create(_stats, effect));

        }

        public IReadOnlyDictionary<ObjectStatId, ObjectStat> Stats => _stats.Values;
        public IReadOnlyCollection<IObjectStatus> StatusInfo => _currentStatuses;

        private void AddStatus(IObjectStatus status)
        {
            //iterate through existing status for effect interactions
            _currentStatuses.ForEach(s => s.Interact(status));
            //instant effects are handled the same way. Maybe overhead, though it provides unified logic

            if (!status.IsOver) //not canceled or merged
            {
                status.Start();
                if (!status.IsOver) //do not add if it is instant effect
                    _currentStatuses.Add(status);
            }
            //else - dispose?
            
        }

        private void Update()
        {
            for (int i = _currentStatuses.Count - 1; i >= 0; i--)
            {
                var status = _currentStatuses[i];
                status.Update(Time.deltaTime);
                if (status.IsOver)
                {
                    _currentStatuses.RemoveAt(i);
                    //dispose?
                }
            }
        }
    }
}