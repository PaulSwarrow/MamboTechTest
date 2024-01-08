using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets.Game.Components
{
    public class ProjectileLauncherComponent : MonoBehaviour
    {
        [SerializeField] private ProjectileComponent projectilePrefab;
        [SerializeField] private int frequency = 2;
        [SerializeField] private int flightTime = 5;
        [SerializeField] private Transform launchPoint;

        private readonly Queue<ProjectileComponent> _pool = new();

        private void Update()
        {
            
        }


        private IEnumerable ProjectileCoroutine()
        {
            ProjectileComponent projectile = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(projectilePrefab);
            projectile.transform.position = launchPoint.position;
            projectile.transform.rotation = launchPoint.rotation;
            projectile.gameObject.SetActive(true);
            yield return new WaitForSeconds(flightTime);

            projectile.gameObject.SetActive(false);
            _pool.Enqueue(projectile);

        }
    }
}