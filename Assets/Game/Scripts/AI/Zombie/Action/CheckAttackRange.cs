using System;
using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class CheckAttackRange : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;
        private readonly Animator _Animator;

        private readonly Transform _Transform;

        #endregion

        public CheckAttackRange(Transform transform) : base()
        {
            _Transform = transform;
            _Owner = transform.GetComponent<ZombieBT>().Owner;
            _Animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {
            var targetPlayer = GetData("Target");

            if (targetPlayer == null)
            {
                State = NodeState.ENS_FAILURE;
                Debug.Log("목표물 X");
                return State;
            }

            Vector3 targetPosition = ((Transform)targetPlayer).position;
            Vector3 position = _Transform.position;
            var distance = Vector2.Distance(new Vector2(position.x, position.z), new Vector2(targetPosition.x, targetPosition.z));
            if (distance <= _Owner.Data.AttackRange + 2.0f)
            {
                if (distance <= _Owner.Data.AttackRange)
                    _Animator.SetBool(Action.ShouldMove, false);
                else
                    _Animator.SetBool(Action.ShouldMove, true);
                State = NodeState.ENS_SUCCESS;
                return State;
            }
            State = NodeState.ENS_FAILURE;
            return State;
        }
    }
}