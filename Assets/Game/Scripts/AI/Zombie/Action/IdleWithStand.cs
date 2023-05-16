using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class IdleWithStand : Node
    {
        private readonly Zombie _Owner;

        public IdleWithStand(Zombie owner)
        {
            _Owner = owner;
        }

        public override NodeState Evaluate()
        {
            _Owner.State = ZombieState.EZS_PATROLLING;
            // Do Nothing
            State = NodeState.ENS_SUCCESS;
            return State;
        }
    }
}