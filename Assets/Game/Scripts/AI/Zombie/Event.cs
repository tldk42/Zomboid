using System;
using Game.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.AI.Zombie
{
    public class Event : MonoBehaviour
    {
        [SerializeField] private Object Collider;

        public void ATK_CollisionEnable()
        {
            SoundManager.Instance.PlaySound("Attack");
            Collider.GetComponent<BoxCollider>().enabled = true;
        }

        public void ATK_CollisionDisable()
        {
            Collider.GetComponent<BoxCollider>().enabled = false;
        }
    }
}