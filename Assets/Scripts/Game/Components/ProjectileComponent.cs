using System;
using StarterAssets.Game.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets.Game.Components
{
    [RequireComponent(typeof(EffectSourceComponent))]
    public class ProjectileComponent : MonoBehaviour
    {
        [SerializeField] private float radius = 1;
        [SerializeField] private float speed = 1;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private int lifeSpan = 5;
        
        private Transform _self;
        private EffectSourceComponent _effectProvider;

        /// <summary>
        /// External handler used to store the projectile into the pool.
        /// TODO: more general solution
        /// </summary>
        public Action<ProjectileComponent> DestructionHandler;

        private float _launchTimestemp;

        private void Awake()
        {
            _self = transform;
            _effectProvider = GetComponent<EffectSourceComponent>();
        }

        private void OnEnable()
        {
            _launchTimestemp = Time.time;
        }

        private void FixedUpdate()
        {
            if (Time.time - _launchTimestemp > lifeSpan)
            {
                Die();
                return;
            }
            
            //TODO support different movement behaviors here. 
            var delta = speed * Time.fixedDeltaTime;
            if (Physics.SphereCast(_self.position, radius, _self.forward, out var hit, delta, layerMask))
            {
                if (GameUtils.GetEntity(hit.collider, out var effectTarget))
                {
                    _effectProvider.ApplyEffect(effectTarget);
                }

                Die();
            }
            else
            {
                _self.position += _self.forward * delta;
            }
        }

        private void Die()
        {
            
            if (DestructionHandler == null)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                DestructionHandler(this);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}