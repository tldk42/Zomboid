using UnityEngine;

namespace Game.Scripts.AI.Zombie
{
    public class OnHit : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Hero"))
            {
                Debug.Log("HIT!!");
                GetComponent<BoxCollider>().enabled = false;
                // Player player = other.GetComponent<Player>();
                // player._animator.Die();
                // StartCoroutine(GameManager.Instance.GameOver());
            }
        }
    }
}