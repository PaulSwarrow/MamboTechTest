using System;
using StarterAssets.Game.Logic;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(EffectSourceComponent))]
    public class ProjectileComponent : MonoBehaviour
    {
        [SerializeField] private float radius = 1;
        [SerializeField] private float speed = 1;
        [SerializeField] private LayerMask layerMask;
        
        private Transform _self;
        private EffectSourceComponent _effectProvider;
        
        private void Awake()
        {
            _self = transform;
            _effectProvider = GetComponent<EffectSourceComponent>();
        }

        private void FixedUpdate()
        {
            //TODO support different movement behaviors here. 
            var delta = speed * Time.fixedDeltaTime;
            if (Physics.SphereCast(_self.position, radius, _self.forward, out var hit, delta, layerMask))
            {
                if (GameUtils.GetEntity(hit.collider, out var effectTarget))
                {
                    _effectProvider.ApplyEffect(effectTarget);
                }

                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                _self.position += _self.forward * delta;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}