using StarterAssets.Game.Logic;
using UnityEngine;

namespace StarterAssets.Game.Components
{
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