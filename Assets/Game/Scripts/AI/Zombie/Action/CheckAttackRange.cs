using System;
using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class CheckAttackRange : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;
        private readonly Transform _Transform;

        #endregion

        public CheckAttackRange(Zombie owner) : base()
        {
            _Owner = owner;
            _Transform = owner.transform;
        }

        public override NodeState Evaluate()
        {
            var targetPlayer = GetData("Target");

            if (targetPlayer == null)
            {
                State = NodeState.ENS_FAILURE;
                return State;
            }

            Vector3 targetPosition = ((Transform)targetPlayer).position;
            Vector3 position = _Transform.position;

            var distance = Vector2.Distance(new Vector2(position.x, position.z),
                new Vector2(targetPosition.x, targetPosition.z));
            if (distance <= _Owner.Data.AttackRange + 1.0f)
            {
                _Owner.Animator.SetBool(Action.ShouldMove, (!(distance <= _Owner.Data.AttackRange)));
                State = NodeState.ENS_SUCCESS;
                return State;
            }

            State = NodeState.ENS_FAILURE;
            return State;
        }
    }
}