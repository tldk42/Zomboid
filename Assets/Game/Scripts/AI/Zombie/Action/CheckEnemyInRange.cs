﻿using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class CheckEnemyInRange : Node
    {
        #region 필수 변수

        private readonly Zombie _Owner;

        #endregion

        #region 속성

        private const int ENEMY_LAYER_MASK = 1 << 6;

        #endregion


        public CheckEnemyInRange(Zombie owner) : base()
        {
             _Owner = owner;
        }

        public override NodeState Evaluate()
        {
            
            _Owner.NavMeshAgent.isStopped = false;

            var colliders = new Collider[1];

            Physics.OverlapSphereNonAlloc(_Owner.SensorTransform.position, _Owner.Data.FOVRange, colliders,
                ENEMY_LAYER_MASK);
            

            if (colliders[0] == null)
            {
                _Owner.State = ZombieState.EZS_PATROLLING;
                Parent.Parent.SetData("Target", null);
                State = NodeState.ENS_FAILURE;
                return State;
            }
            
            if (_Owner.State == ZombieState.EZS_RECOGNIZED)
            {
                _Owner.Animator.SetBool(Action.FoundPlayer, true);
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

            if (_Owner.State == ZombieState.EZS_PATROLLING)
            {
                _Owner.Animator.SetBool(Action.ShouldMove, true);
            }

            Parent.Parent.SetData("Target", colliders[0].transform);
            _Owner.State = ZombieState.EZS_RECOGNIZED;
            State = NodeState.ENS_SUCCESS;
            return State;
        }
    }
}