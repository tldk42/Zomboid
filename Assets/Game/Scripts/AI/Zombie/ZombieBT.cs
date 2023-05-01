using System;
using System.Collections.Generic;
using Game.Scripts.AI.BT.Core;
using Game.Scripts.AI.Zombie.Action;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.AI.Zombie
{
    public class ZombieBT : BehaviorTree
    {
        public Zombie Owner { get; private set; }


        [Title("경로 설정")] [SerializeField, SceneObjectsOnly, GUIColor(0.37f, 0.52f, 0.64f, 1f)]
        public Transform[] WayPoints;

        /// <summary>
        /// 1. 범위 내에서 overlap되는 충돌체가 있는지 확인한다.
        ///     * 그 충돌체가 시야각 안에 있으면 Target데이터를 업데이트한다. 
        /// 2. Target데이터가 존재하면 따라간다.
        /// 3. 따라가다가 공격 범위안에 들어온다.
        /// 4. 공격한다.
        /// ------------------- 반복 -----------------
        /// </summary>
        private void Awake()
        {
            Owner = transform.GetComponent<Zombie>();
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new Sequence(new List<Node>
                        {
                            new CheckAttackRange(transform),
                            new AttackTarget(transform)
                        }),
                        new Sequence(new List<Node>
                        {
                            new CheckEnemyInRange(transform),
                            new Wait(transform, 1.6f),
                            new ChaseTarget(transform),
                        })
                    }),

                    new TaskPatrol(transform, WayPoints),
                }
            );
            return root;
        }
    }
}