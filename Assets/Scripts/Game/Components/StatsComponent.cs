using System;
using System.Collections.Generic;
using Game.Data;
using Game.Interfaces;
using UnityEngine;

namespace Game.Components
{
    /// <summary>
    /// Stores stats for the entity
    /// </summary>
    public class StatsComponent : MonoBehaviour, IEntityStats
    {
        public event IEntityStats.StatChangeDelegate StatChangeEvent;

        //TODO: proper object initialization or (alternatively) _stats dictionary inspector
        [SerializeField] private int health = 100;

        private readonly Dictionary<ObjectStatId, ObjectStatValue> _stats = new();
        public IReadOnlyDictionary<ObjectStatId, ObjectStatValue> Values => _stats;


        private void Awake()
        {
            _stats.Add(ObjectStatId.Health, new ObjectStatValue(health, health));
        }

        /// <summary>
        /// Provides information if a stat exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasStat(ObjectStatId id) => _stats.ContainsKey(id);

        /// <summary>
        /// Provides access to stats values. Make sure to check if the required stats exists
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public int this[ObjectStatId id]
        {
            get => _stats.TryGetValue(id, out var stat) ? stat.Current : 0;
            set
            {
                if (_stats.TryGetValue(id, out var stat))
                {
                    value = Mathf.Clamp(value, 0, stat.Max);
                    if (stat.Current == value) return;
                    var oldValue = stat.Current;
                    stat.Current = value;
                    _stats[id] = stat;
                    StatChangeEvent?.Invoke(id, oldValue, value);
                }
                else
                {
                    throw new Exception($"Can not apply value to non-existent stat");
                }
            }
        }
    }
}