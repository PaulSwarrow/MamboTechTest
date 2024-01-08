using System;
using System.Collections.Generic;
using System.Linq;
using StarterAssets.Game.Data;
using StarterAssets.Game.Factories;
using StarterAssets.Game.Model;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(StatsComponent))]
    public class GameEntity : MonoBehaviour, IGameEntity
    {
        private StatsComponent _stats;
        private readonly List<IObjectStatus> _currentStatuses = new ();
        //TODO inject
        private readonly StatusFactory _statusFactory = new StatusFactory();

        private void Awake()
        {
            _stats = GetComponent<StatsComponent>();
        }

        public void ApplyEffect(EffectSpec effect)
        {
            var newStatus = _statusFactory.Create(_stats, effect);
            //iterate through existing status for effect interactions
            _currentStatuses.ForEach(status => status.Interact(newStatus));
            //instant effects are handled the same way. Maybe overhead, though it provides unified logic

            if (!newStatus.IsOver) //not canceled or merged
            {
                newStatus.Start();
                _currentStatuses.Add(newStatus);
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