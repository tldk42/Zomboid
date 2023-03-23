using Game.Scripts.ScriptableObject;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace Game.Scripts.AI.Zombie
{
    public class Zombie : MonoBehaviour
    {
        public ZombieData Data;
        public Transform SensorTransform;
        public NavMeshAgent NavMeshAgent;
        [SerializeField, ReadOnly, InspectorName("타켓")]public Transform TargetData;

        private void Awake()
        {
            Data = Resources.Load<ZombieData>("Zombie");
            NavMeshAgent = GetComponent<NavMeshAgent>();
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
    }
}