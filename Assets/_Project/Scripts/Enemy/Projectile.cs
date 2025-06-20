using Assets._Project.Scripts.PlayerLogic;
using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class Projectile : NetworkBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private int _damage = 10;

        private Vector3 _direction;
        private float _timer;

        private bool _isValid => IsServer && IsSpawned;

        public void Init(Vector3 dir)
        {
            _direction = dir.normalized;
        }

        private void Update()
        {
            if (!_isValid) return;

            transform.position += _direction * _speed * Time.deltaTime;

            _timer += Time.deltaTime;
            if (_timer >= _lifeTime)
            {
                NetworkObject.Despawn();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isValid) return;

            if (other.TryGetComponent(out PlayerHealth health))
            {
                health.TakeDamage(_damage);
                NetworkObject.Despawn();
            }
        }
    }
}