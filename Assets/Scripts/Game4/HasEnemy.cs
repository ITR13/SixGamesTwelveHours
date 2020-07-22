using UnityEngine;

namespace Game4
{
    public class HasEnemy : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private int health;

        private float _invulnerable;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<HackAndSlasher>();
            if (player == null) return;
            player.HurtMe();
        }

        public void HurtMe(Vector3 direction)
        {
            rigidBody.AddForce(
                direction.normalized * 8,
                ForceMode2D.Impulse
            );
            health -= 1;
            _invulnerable = 0.5f;
        }
    }
}
