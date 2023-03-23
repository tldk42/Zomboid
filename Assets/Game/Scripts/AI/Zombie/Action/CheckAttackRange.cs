using System;
using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class CheckAttackRange : Node
    {
        #region 애니메이션 캐시 변수

        private static readonly int ShouldMove = Animator.StringToHash("ShouldMove");

        #endregion

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

            if (Vector2.Distance(new Vector2(_Transform.position.x, _Transform.position.z), new Vector2(targetPosition.x, targetPosition.z)) <= _Owner.Data.AttackRange + float.Epsilon)
            {
                State = NodeState.ENS_SUCCESS;
                return State;
            }

            State = NodeState.ENS_FAILURE;
            return State;
        }
    }
}