using Assets._Project.Scripts.PlayerLogic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class ShootingEnemy : BaseEnemy
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private float _shootInterval = 3f;

        private float _shootTimer;

        protected override void Update()
        {
            if (!IsServer) return;

            WaitForTargetUpdate();

            WaitForShoot();
        }

        private void WaitForShoot()
        {
            _shootTimer += Time.deltaTime;

            if (_targetPlayerTransform != null)
            {
                Vector3 direction = (_targetPlayerTransform.position - transform.position).normalized;
                direction.y = 0f;
                transform.rotation = Quaternion.LookRotation(direction);

                if (_shootTimer >= _shootInterval)
                {
                    _shootTimer = 0f;
                    ShootAtTarget();
                }
            }
        }

        private void ShootAtTarget()
        {
            if (_projectilePrefab == null || _shootPoint == null) return;

            Projectile projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
            projectile.Init((_targetPlayerTransform.position - _shootPoint.position).normalized);
            projectile.NetworkObject.Spawn();
        }
    }
}