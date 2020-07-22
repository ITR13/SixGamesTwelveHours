using UnityEngine;

namespace Game4
{
    public class HasEnemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private int health;
        [SerializeField] private float jumpInterval;

        private float _invulnerable;
        private float _jumpTimer;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<HackAndSlasher>();
            if (player == null) return;
            player.HurtMe();
        }

        public void HurtMe(Vector3 direction)
        {
            if (_invulnerable > 0) return;
            rigidBody.AddForce(
                direction.normalized * 8 + Vector3.up * 2,
                ForceMode2D.Impulse
            );
            health -= 1;
            _invulnerable = 1.5f;
        }

        private void Update()
        {
            DoJump();
            DoInvuln();

            _invulnerable -= Time.deltaTime;
            spriteRenderer.color = new Color(
                1,
                1,
                1,
                Mathf.Clamp01(0.5f - _invulnerable) + 0.5f
            );
            spriteRenderer.enabled =
                _invulnerable <= 0.5f || (_invulnerable % 0.4f > 0.15f);
        }

        private void DoJump()
        {

            if (_jumpTimer < 0f)
            {
                _jumpTimer += jumpInterval;
            }
        }
    }
}
