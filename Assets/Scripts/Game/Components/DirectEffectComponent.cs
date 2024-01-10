using System.Collections;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class DirectEffectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private int delay = 10;
        private IEffectSource[] _effectSources;

        private void Start()
        {
            _effectSources = GetComponents<IEffectSource>();
            StartCoroutine(DelayCoroutine());
        }

        private IEnumerator DelayCoroutine()
        {
            if (!target.TryGetComponent<IGameEntity>(out var entity))
            {
                Debug.LogError("Effect target is invalid!");
                yield break;
            }

            yield return new WaitForSeconds(delay);
            foreach (var effectSource in _effectSources)
            {
                effectSource.ApplyEffect(entity);
            }
        }
    }
}