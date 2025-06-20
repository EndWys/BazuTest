using UnityEngine.AI;
using Assets._Project.Scripts.PlayerLogic;

namespace Assets._Project.Scripts.Enemy
{
    public class ChasingEnemy : BaseEnemy
    {
        private IActivePlayerList _playerList;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        protected override void UpdateTarget()
        {
            base.UpdateTarget();

            if (_targetPlayerTransform != null)
            {
                _agent.SetDestination(_targetPlayerTransform.position);
            }
        }
    }
}