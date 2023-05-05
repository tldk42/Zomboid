using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class AttackTarget : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;
        private readonly Animator _Animator;

        private readonly Transform _Transform;
        private readonly Transform[] _WayPoints;

        #endregion
        
        public AttackTarget(Transform transform) : base()
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
                Debug.Log("목표물 없어짐");
                return State;
            }
            
            _Animator.SetBool(Action.CanAttack, true);

            State = NodeState.ENS_SUCCESS;
            return State;
        }
    }
}