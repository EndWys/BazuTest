using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemySpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _enemyPrefab;
        [SerializeField] private float _spawnInterval = 5f;
        [SerializeField] private float _spawnRadius = 10f;

        private float _timer;

        private void Update()
        {
            if (!IsSpawned || !IsHost) return;

            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer = 0f;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * _spawnRadius;
            randomPos.y = 0f;

            NetworkObject enemy = Instantiate(_enemyPrefab, randomPos, Quaternion.identity);
            enemy.Spawn();
        }
    }
}