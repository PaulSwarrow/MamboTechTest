using System;
using System.Collections.Generic;
using StarterAssets.Game.Data;
using StarterAssets.Game.Logic;
using UnityEditorInternal;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class StatsComponent : MonoBehaviour, IEntityStats
    {
        public event IEntityStats.StatChangeDelegate StatChangeEvent;
        [Serializable]
        private struct StatSpec
        {
            public ObjectStatId Id;
            public int MaxValue;
        }

        //TODO: proper object initialization
        [SerializeField] private List<StatSpec> config;

        private readonly Dictionary<ObjectStatId, ObjectStatValue> _stats = new ();
        public IReadOnlyDictionary<ObjectStatId, ObjectStatValue> Values => _stats;


        private void Awake()
        {
            foreach (var spec in config)
            {
                _stats.Add(spec.Id, new ObjectStatValue(spec.MaxValue, spec.MaxValue));
            }
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
                    if(stat.Current == value) return;
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