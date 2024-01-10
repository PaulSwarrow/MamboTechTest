using Game.Interfaces;
using Game.Logic;
using UnityEngine;

namespace Game.Components
{
    /// <summary>
    /// Applies effects when an object enters the trigger
    /// </summary>
    public class TriggerComponent : MonoBehaviour
    {
        private IEffectSource[] _effectSources;

        private void Awake()
        {
            _effectSources = GetComponents<IEffectSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (GameUtils.GetEntity(other, out var effectTarget))
            {
                foreach (var effectSource in _effectSources)
                {
                    effectSource.ApplyEffect(effectTarget);
                }
            }
        }
    }
}