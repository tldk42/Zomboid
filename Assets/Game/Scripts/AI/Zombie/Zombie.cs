using Game.Scripts.ScriptableObject;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.AI.Zombie
{
    public enum ZombieState
    {
        EZS_PATROLLING, // 돌아다니는 상태
        EZS_RECOGNIZED, // 상대를 이미 인식한 상태
        EZS_CHASING, // 쫓는 상태
        EZS_DEAD,
    }

    public class Zombie : MonoBehaviour
    {
        public ZombieData Data;
        public Transform SensorTransform;
        public NavMeshAgent NavMeshAgent;
        public Animator Animator;
        public ZombieState State;

        public event System.Action<ZombieState> ZombieStateChanged;

        private void Awake()
        {
            Data = Resources.Load<ZombieData>("Zombie");
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        private void OnDrawGizmos()
        {
            #region 시야각

            Vector3 leftDir = Quaternion.Euler(0f, -Data.FOV * 0.5f, 0f) * SensorTransform.forward;
            Vector3 rightDir = Quaternion.Euler(0f, Data.FOV * 0.5f, 0f) * SensorTransform.forward;

            Gizmos.color = new Color(0.54f, 0.32f, 0.321f, 1f);
            Gizmos.DrawRay(SensorTransform.position, leftDir * Data.FOVRange);
            Gizmos.DrawRay(SensorTransform.position, rightDir * Data.FOVRange);

            #endregion
        }

        private void Dead()
        {
            Data.Hp = 0;
            NavMeshAgent.isStopped = true;
            State = ZombieState.EZS_DEAD;
        }

        protected virtual void OnZombieStateChanged(ZombieState obj)
        {
            ZombieStateChanged?.Invoke(obj);
        }
    }
}