using Unity.Netcode;

namespace Assets._Project.Utilities
{
    public class ServerBehaviour : NetworkBehaviour
    {
        public bool IsActiveServerObject => IsServer && IsSpawned;
    }
}