using Unity.Netcode;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _playerPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        NetworkObject playerInstance = Instantiate(_playerPrefab, GetSpawnPosition(), Quaternion.identity);

        playerInstance.SpawnAsPlayerObject(clientId);
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
        }
    }
}
