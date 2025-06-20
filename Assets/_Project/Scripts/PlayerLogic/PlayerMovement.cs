using Cinemachine;
using Unity.Netcode;
using UnityEngine;

namespace Assets._Project.Scripts.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : NetworkBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        private CharacterController _controller;

        public override void OnNetworkSpawn()
        {
            _controller = GetComponent<CharacterController>();

            if (IsOwner)
            {
                var vCam = FindObjectOfType<CinemachineVirtualCamera>();
                if (vCam != null)
                {
                    vCam.Follow = transform;
                    vCam.LookAt = transform;
                }
            }
        }

        private void Update()
        {
            if (!IsOwner) return;

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            _controller.SimpleMove(move * _moveSpeed);
        }
    }
}