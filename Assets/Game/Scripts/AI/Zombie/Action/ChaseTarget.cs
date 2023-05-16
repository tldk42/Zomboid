using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class ChaseTarget : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;
        private readonly Transform _Transform;

        #endregion

        public ChaseTarget( Zombie owner) : base()
        {
            _Owner = owner;
            _Transform = owner.transform;
            _Owner.NavMeshAgent.stoppingDistance = _Owner.Data.AttackRange;
        }

        public override NodeState Evaluate()
        {
            _Owner.Data.Speed = 1.5f;
            _Owner.NavMeshAgent.speed = _Owner.Data.Speed;
            Transform targetTransform = (Transform)GetData("Target");
            Vector3 targetPosition = targetTransform.position;
            if (Vector3.Distance(_Transform.position, targetPosition) >= _Owner.Data.AttackRange)
            {
                _Owner.NavMeshAgent.SetDestination(targetPosition);
                _Transform.LookAt(new Vector3(targetPosition.x, _Transform.position.y ,targetPosition.z));
            }

            State = NodeState.ENS_RUNNING;
            return State;
        }
    }
}