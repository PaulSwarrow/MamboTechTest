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
        [SerializeField] private bool immuneToPoison;
        [SerializeField] private string damageLogTemplate = "{0} was damaged, health = {1}";
        [SerializeField] private string deathLogTemplate = "I'm a {0}, and I was destroyed";
        
        
        private StatsComponent _stats;
        private readonly List<IObjectStatus> _currentStatuses = new ();
        //TODO inject
        private readonly StatusFactory _statusFactory = new ();

        private void Awake()
        {
            _stats = GetComponent<StatsComponent>();
            _stats.StatChangeEvent += OnStatChange;
        }
        private void OnDestroy()
        {
            
            _stats.StatChangeEvent -= OnStatChange;
        }

        private void Start()
        {
            if (isImmortal)
            {
                AddStatus(_statusFactory.CreateImmortality(_stats));
            }

            if (immuneToPoison)
            {
                AddStatus(_statusFactory.Create(_stats, new EffectSpec(EffectType.Poison, EffectMode.Immune, -1,0,0)));
            }
        }

        public void ApplyEffect(EffectSpec effect)
        {
            AddStatus(_statusFactory.Create(_stats, effect));

        }

        public IEntityStats Stats => _stats;
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

        private void OnStatChange(ObjectStatId stat, int oldValue, int newValue)
        {
            if (stat == ObjectStatId.Health)
            {
                if(oldValue > newValue)
                    Debug.Log(string.Format(damageLogTemplate, name, newValue));

                if (newValue == 0)
                {
                    Debug.Log(string.Format(deathLogTemplate, name));
                }
            }
        }

    }
}