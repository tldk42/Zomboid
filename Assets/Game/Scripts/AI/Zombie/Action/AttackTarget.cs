using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class AttackTarget : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;

        private readonly Transform[] _WayPoints;

        #endregion
        
        public AttackTarget(Zombie owner) : base()
        {
            _Owner = owner;
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
            
            _Owner.Animator.SetTrigger(Action.CanAttack);

            State = NodeState.ENS_SUCCESS;
            return State;
        }
    }
}