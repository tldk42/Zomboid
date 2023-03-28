using Game.Scripts.AI.BT.Core;
using UnityEngine;

namespace Game.Scripts.AI.Zombie.Action
{
    public class TaskPatrol : Node
    {

        #region 필수 변수

        private readonly Zombie _Owner;
        private readonly Animator _Animator;

        private readonly Transform _Transform;
        private readonly Transform[] _WayPoints;

        #endregion

        #region 속성

        private int _CurrentWaypointIndex = 0;
        private float _WaitTime = 3f;
        private float _WaitCounter = 0f;
        private bool _IsWaiting;

        #endregion

        public TaskPatrol(Transform transform, Transform[] wayPoints) : base()
        {
            _Owner = transform.GetComponent<ZombieBT>().Owner;
            _Animator = transform.GetComponent<Animator>();
            _Transform = transform;
            _WayPoints = wayPoints;
        }

        /**
         * 1. 목표 지점에서 대기 상태 -> WaitTime만큼 대기
         * 2. 목표 지점에 도착 상태 -> 목표지점 Index + 1, Wait Flag 켜기
         * 3. 목표 지점에 가는 상태 -> 이동시키기
         */
        public override NodeState Evaluate()
        {
            if (_IsWaiting)
            {
                _WaitCounter += Time.deltaTime;
                if (_WaitCounter >= _WaitTime)
                {
                    _Owner.NavMeshAgent.isStopped = false;
                    _IsWaiting = false;
                    _Animator.SetBool(Action.ShouldMove, true);
                }
            }
            else
            {
                Vector3 waypointPosition = _WayPoints[_CurrentWaypointIndex].position;
                if (Vector3.Distance(new Vector3(_Transform.position.x, waypointPosition.y, _Transform.position.z), waypointPosition) <= _Owner.NavMeshAgent.stoppingDistance)
                {
                    _Owner.NavMeshAgent.isStopped = true;
                    _WaitCounter = 0f;
                    _IsWaiting = true;

                    _CurrentWaypointIndex = (_CurrentWaypointIndex + 1) % _WayPoints.Length;

                    _Animator.SetBool(Action.ShouldMove, false);
                }
                else
                {
                    _Owner.NavMeshAgent.SetDestination(waypointPosition);
                    _Animator.SetBool(Action.ShouldMove, true);
                    _Transform.LookAt(new Vector3(waypointPosition.x, _Transform.position.y, waypointPosition.z)
                    );
                }
            }

            _Owner.State = ZombieState.EZS_PATROLLING;
            State = NodeState.ENS_RUNNING;
            return State;
        }
    }
}