using System;
using Game.Scripts.AI.BT.Core;
using UnityEngine;
namespace Game.Scripts.AI.Zombie.Action
{
    public class Wait : Node
    {
        private readonly Zombie _Owner;
        private readonly Animator _Animator;

        private readonly Transform _Transform;
        private float _Duration;
        private float _ElapsedTime = 0f;
        private bool _IsWaiting = true;


        public Wait(Transform transform, float duration)
        {
            _Owner = transform.GetComponent<ZombieBT>().Owner;
            _Animator = transform.GetComponent<Animator>();
            _Transform = transform;
            _Duration = duration;
        }

        public override NodeState Evaluate()
        {
            if (_IsWaiting)
            {
                _ElapsedTime += Time.deltaTime;
                _Animator.SetBool(Action.FoundPlayer, true);
                _Owner.NavMeshAgent.isStopped = true;
                Debug.Log(_ElapsedTime);
                if (_ElapsedTime >= _Duration)
                {
                    _Owner.NavMeshAgent.isStopped = false;
                    _IsWaiting = false;
                    _Animator.SetBool(Action.FoundPlayer, false);
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