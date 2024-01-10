using System;
using System.Collections.Generic;
using Game.Data;

namespace Game.Interfaces
{
    public interface IEntityStats
    {
        public delegate void StatChangeDelegate(ObjectStatId stat, int oldValue, int newValue);

        event StatChangeDelegate StatChangeEvent;
        bool HasStat(ObjectStatId id);

        /// <summary>
        /// Provides access to stats values. Make sure to check if the required stats exists
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public int this[ObjectStatId id] { get; }

        IReadOnlyDictionary<ObjectStatId, ObjectStatValue> Values { get; }
    }
}