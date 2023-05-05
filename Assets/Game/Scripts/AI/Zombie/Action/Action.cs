using UnityEngine;
namespace Game.Scripts.AI.Zombie.Action
{
    public static class Action
    {
        #region 애니메이션 캐시 변수

        public static readonly int ShouldMove = Animator.StringToHash("ShouldMove");
        public static readonly int CanAttack = Animator.StringToHash("CanAttack");
        public static readonly int FoundPlayer = Animator.StringToHash("FoundPlayer");
        public static readonly int IsDead = Animator.StringToHash("IsDead");

        #endregion
    }
}