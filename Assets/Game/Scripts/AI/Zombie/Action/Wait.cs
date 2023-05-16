using System;
using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class Wait : Node
    {
        private readonly Zombie _Owner;

        private float _Duration;
        private float _ElapsedTime = 0f;
        private bool _IsWaiting = true;


        public Wait(Zombie owner, float duration)
        {
            _Owner = owner;
            _Duration = duration;
        }

        public override NodeState Evaluate()
        {
            if (_IsWaiting)
            {
                _ElapsedTime += Time.deltaTime;
                _Owner.Animator.SetBool(Action.FoundPlayer, true);
                _Owner.NavMeshAgent.isStopped = true;
                if (_ElapsedTime >= _Duration)
                {
                    _Owner.NavMeshAgent.isStopped = false;
                    _IsWaiting = false;
                }
            }
            else
            {
                _ElapsedTime = 0f;
                State = NodeState.ENS_SUCCESS;
            }

            return State;
        }
    }
}