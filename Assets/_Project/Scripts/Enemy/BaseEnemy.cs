using Assets._Project.Scripts.PlayerLogic;
using Assets._Project.Utilities;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public abstract class BaseEnemy : ServerBehaviour
    {
        protected Transform _targetPlayerTransform;

        private IActivePlayerList _playerList;

        private float _targetUpdateRate = 1f;
        private float _nextUpdate;

        public override void OnNetworkSpawn()
        {
            enabled = IsServer;
            _playerList = ActivePlayersProvider.Instance;
        }

        protected virtual void Update()
        {
            if (!IsActiveServerObject) return;

            WaitForTargetUpdate();
        }

        protected virtual void WaitForTargetUpdate()
        {
            if (Time.time >= _nextUpdate)
            {
                _nextUpdate = Time.time + _targetUpdateRate;
                UpdateTarget();
            }
        }

        protected virtual void UpdateTarget()
        {
            var players = _playerList.ActivePlayers
            .Where(p => p.IsSpawned)
            .ToList();

            if (players.Count == 0) return;

            NetworkObject closest = players
                .OrderBy(p => Vector3.Distance(transform.position, p.transform.position))
                .FirstOrDefault();

            if(closest != null)
            {
                _targetPlayerTransform = closest.transform;
            }
        }
    }
}