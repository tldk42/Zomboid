using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.AI.BT.Core
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        [ReadOnly]private Node _Root;

        protected void Start()
        {
            _Root = SetupTree();
        }

        /** 매 프레임 자식 노드들을 evaluate */
        protected virtual void Update()
        {
            _Root?.Evaluate();
        }

        protected abstract Node SetupTree();
    }
}