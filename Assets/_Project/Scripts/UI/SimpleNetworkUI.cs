using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.UI
{
    public class SimpleNetworkUI : MonoBehaviour
    {
        private void OnGUI()
        {
            int width = 200;
            int height = 50;
            int spacing = 10;
            int x = 10;
            int y = 10;

            if (!NetworkManager.Singleton.IsHost && !NetworkManager.Singleton.IsClient)
            {
                if (GUI.Button(new Rect(x, y, width, height), "Host"))
                {
                    NetworkManager.Singleton.StartHost();
                }

                if (GUI.Button(new Rect(x, y + height + spacing, width, height), "Client"))
                {
                    NetworkManager.Singleton.StartClient();
                }
            }
            else
            {
                if (GUI.Button(new Rect(x, y, width, height), "Shutdown"))
                {
                    NetworkManager.Singleton.Shutdown();
                }
            }
        }
    }
}