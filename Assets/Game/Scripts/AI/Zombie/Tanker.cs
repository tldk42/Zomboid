using System;
using System.Collections.Generic;
using Game.Scripts.AI.BT.Core;
using Game.Scripts.AI.Zombie.Action;

namespace Game.Scripts.AI.Zombie
{
    public class Tanker : BaseBT
    {
        

        protected override void Update()
        {
            base.Update();
        }

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
            {
                new Selector(new List<Node>
                {
                    new Sequence(new List<Node>
                    {
                        new CheckEnemyInRange(Owner),
                        new Wait(Owner, 1.5f),
                        new ChaseTarget(Owner)
                    })
                }),
                new IdleWithStand(Owner)
            });

            return root;
        }
    }
}