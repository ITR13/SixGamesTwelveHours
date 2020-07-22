using UnityEngine;

namespace Game4
{
    public class HasEnemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;

        private void 

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<HackAndSlasher>();
            if (player == null) return;
            player.HurtMe();
        }

        public void HurtMe(Vector3 direction)
        {
            
        }
    }
}
