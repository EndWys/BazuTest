using Assets._Project.Utilities;
using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemySpawner : ServerBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private BaseEnemy[] _enemyPrefabs;

        [Header("Settings")]
        [SerializeField] private float _spawnInterval = 5f;
        [SerializeField] private float _spawnRadius = 10f;

        private float _timer;

        private void Update()
        {
            if (!IsActiveServerObject) return;

            _timer += Time.deltaTime;
            if (_timer >= _spawnInterval)
            {
                _timer = 0f;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            if (_enemyPrefabs == null || _enemyPrefabs.Length == 0)
            {
                Debug.LogWarning("EnemySpawner: No enemy prefabs assigned.");
                return;
            }

            int index = Random.Range(0, _enemyPrefabs.Length);
            BaseEnemy selectedPrefab = _enemyPrefabs[index];

            Vector3 randomPos = transform.position + Random.insideUnitSphere * _spawnRadius;
            randomPos.y = 0f;

            BaseEnemy enemy = Instantiate(selectedPrefab, randomPos, Quaternion.identity);
            enemy.NetworkObject.Spawn();
        }
    }
}