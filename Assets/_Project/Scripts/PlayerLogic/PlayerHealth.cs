using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.PlayerLogic
{
    public class PlayerHealth : NetworkBehaviour
    {
        public NetworkVariable<int> Health = new NetworkVariable<int>(100);

        public void TakeDamage(int amount)
        {
            if (!IsServer) return;

            Health.Value -= amount;
            if (Health.Value <= 0)
            {
                NetworkObject.Despawn();
                Debug.Log($"{OwnerClientId} is DEAD");
            }
        }
    }
}