using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Assets._Project.Scripts.PlayerLogic;

namespace Assets._Project.Scripts.Enemy
{
    public class EnemyAI : NetworkBehaviour
    {
        private NavMeshAgent _agent;
        private IActivePlayerList _playerList;

        private float _updateRate = 1.0f;
        private float _nextUpdate;



        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void OnNetworkSpawn()
        {
            enabled = IsServer;
            _playerList = ActivePlayersProvider.Instance;
        }

        private void Update()
        {
            if (!IsServer) return;

            if (Time.time >= _nextUpdate)
            {
                _nextUpdate = Time.time + _updateRate;
                UpdateTarget();
            }
        }

        private void UpdateTarget()
        {
            var players = _playerList.ActivePlayers
                .Where(p => p /*&& p.IsAlive*/ && p.IsPlayerObject)
                .ToList();

            if (players.Count == 0) return;

            Transform closest = players
                .OrderBy(p => Vector3.Distance(transform.position, p.transform.position))
                .First().transform;

            _agent.SetDestination(closest.position);
        }
    }
}