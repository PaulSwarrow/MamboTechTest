using System;
using System.Collections.Generic;
using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class StatsComponent : MonoBehaviour
    {
        //TODO: proper object initialization
        [Serializable]
        private struct StatSpec
        {
            public ObjectStatId Id;
            public int MaxValue;
        }

        [SerializeField] private List<StatSpec> config;

        private readonly Dictionary<ObjectStatId, ObjectStat> _stats = new ();
        public IReadOnlyDictionary<ObjectStatId, ObjectStat> Values => _stats;


        private void Awake()
        {
            foreach (var spec in config)
            {
                _stats.Add(spec.Id, new ObjectStat(spec.MaxValue, spec.MaxValue));
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

            get => _stats.TryGetValue(id, out var stat) ? stat.Value : 0;
            set
            {
                if (_stats.TryGetValue(id, out var stat))
                {
                    stat.Value = Mathf.Clamp(value, 0, stat.MaxValue);
                    _stats[id] = stat;
                }
                else
                {
                    throw new Exception($"Can not apply value to non-existent stat");
                }
            }
        }

        private void Update()
        {
            if (_stats.TryGetValue(ObjectStatId.Health, out var data) && data.Value <= 0)
            {
                //DEAD!
            }
        }
    }
}