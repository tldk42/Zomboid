using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class CheckEnemyInRange : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;
        private readonly Animator _Animator;

        private readonly Transform _Transform;

        #endregion

        #region 속성

        private const int ENEMY_LAYER_MASK = 1 << 6;

        #endregion


        public CheckEnemyInRange(Transform transform) : base()
        {
            _Owner = transform.GetComponent<ZombieBT>().Owner;
            _Animator = transform.GetComponent<Animator>();
            _Transform = transform;
        }

        public override NodeState Evaluate()
        {
            
            _Owner.NavMeshAgent.isStopped = false;
            _Animator.SetBool(Action.ShouldMove, true);
            _Animator.SetBool(Action.CanAttack, false);
            
            var colliders = new Collider[1];

            Physics.OverlapSphereNonAlloc(_Owner.SensorTransform.position, _Owner.Data.FOVRange, colliders,
                ENEMY_LAYER_MASK);
            

            if (colliders[0] == null)
            {
                _Owner.State = ZombieState.EZS_PATROLLING;
                Parent.Parent.SetData("Target", null);
                _Owner.TargetData = null;
                State = NodeState.ENS_FAILURE;
                return State;
            }
            
            if (_Owner.State == ZombieState.EZS_RECOGNIZED)
            {
                State = NodeState.ENS_SUCCESS;
                return State;
            }

            Vector3 dir = (colliders[0].transform.position - _Owner.SensorTransform.position).normalized;
            var dot = Vector3.Dot(dir, _Owner.SensorTransform.forward);

            if (dot < Mathf.Cos(_Owner.Data.FOV * 0.5f))
            {
                State = NodeState.ENS_FAILURE;
                return State;
            }
            
            Parent.Parent.SetData("Target", colliders[0].transform);
            _Owner.State = ZombieState.EZS_RECOGNIZED;
            _Owner.TargetData = colliders[0].transform;
            State = NodeState.ENS_SUCCESS;
            return State;
        }
    }
}