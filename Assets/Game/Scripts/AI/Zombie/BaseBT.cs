using Game.Scripts.AI.BT.Core;

namespace Game.Scripts.AI.Zombie
{
    public class BaseBT : BehaviorTree
    {
        public Zombie Owner { get;  set; }

        private void Awake()
        {
            Owner = transform.GetComponent<Zombie>();
        }
        
        protected override Node SetupTree()
        {
            throw new System.NotImplementedException();
        }
    }
}