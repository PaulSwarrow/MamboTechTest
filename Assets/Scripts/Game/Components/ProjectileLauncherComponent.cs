using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    /// <summary>
    /// Gun behavior
    /// </summary>
    public class ProjectileLauncherComponent : MonoBehaviour
    {
        [SerializeField] private ProjectileComponent projectilePrefab;
        [SerializeField] private int period = 2;
        [SerializeField] private Transform launchPoint;

        private readonly Queue<ProjectileComponent> _pool = new();
        private float timeSinceLastTick;

        private void Update()
        {
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= period)
            {
                Launch();
                timeSinceLastTick -= period;
            }
        }

        private void Launch()
        {
            var projectile = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(projectilePrefab);
            projectile.transform.position = launchPoint.position;
            projectile.transform.rotation = launchPoint.rotation;
            projectile.DestructionHandler = OnProjectileDie;
            projectile.gameObject.SetActive(true);
        }

        private void OnProjectileDie(ProjectileComponent projectile)
        {
            projectile.gameObject.SetActive(false);
            _pool.Enqueue(projectile);
        }
    }
}