using System;
using System.Collections;
using StarterAssets.Game.Data;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(IEffectSource))]
    public class DirectEffectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private int delay = 10;
        private IEffectSource _effectSource;

        private void Start()
        {
            _effectSource = GetComponent<IEffectSource>();
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
            _effectSource.ApplyEffect(entity);
        }
    }
}