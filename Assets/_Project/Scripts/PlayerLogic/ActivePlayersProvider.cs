using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.PlayerLogic
{
    public interface IActivePlayerHolder
    {
        void AddPlayer(NetworkObject player);
        void RemovePlayer(ulong id);
    }

    public interface IActivePlayerList
    {
        IReadOnlyList<NetworkObject> ActivePlayers { get; }
    }

    public class ActivePlayersProvider : IActivePlayerHolder, IActivePlayerList
    {
        public static ActivePlayersProvider Instance { get; private set; }

        public IReadOnlyList<NetworkObject> ActivePlayers => _activePlayers.Values.ToList();

        private Dictionary<ulong, NetworkObject> _activePlayers;



        public ActivePlayersProvider()
        {
            Instance = this;
            _activePlayers = new();
        }

        public void AddPlayer(NetworkObject player)
        {
            if (!_activePlayers.ContainsKey(player.OwnerClientId))
            {
                _activePlayers.Add(player.OwnerClientId, player);
            }
        }


        public void RemovePlayer(ulong id)
        {
            if (_activePlayers.ContainsKey(id))
            {
                _activePlayers.Remove(id);
            }
        }
    }
}