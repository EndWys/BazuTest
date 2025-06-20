using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.PlayerLogic
{
    public class PlayerSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject _playerPrefab;

        private IActivePlayerHolder _playerProvider;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _playerProvider = new ActivePlayersProvider();

                NetworkManager.OnClientConnectedCallback += OnClientConnected;
                NetworkManager.OnClientDisconnectCallback += OnClientConnected;
            }
        }

        private void OnClientConnected(ulong clientId)
        {
            NetworkObject playerInstance = Instantiate(_playerPrefab, GetSpawnPosition(), Quaternion.identity);
            playerInstance.SpawnAsPlayerObject(clientId);

            _playerProvider.AddPlayer(playerInstance);
        }

        private void OnClientDisconnected(ulong clientId)
        {
            _playerProvider.RemovePlayer(clientId);
        }

        private Vector3 GetSpawnPosition()
        {
            return new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        }

        public override void OnNetworkDespawn()
        {
            if (IsServer)
            {
                NetworkManager.OnClientConnectedCallback -= OnClientConnected;
                NetworkManager.OnClientDisconnectCallback -= OnClientConnected;
            }
        }
    }
}