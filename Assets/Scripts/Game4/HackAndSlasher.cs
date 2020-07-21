using UnityEngine;

namespace Game4
{
    public class HackAndSlasher : MonoBehaviour
    {
        private Rigidbody2D rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private float horizontal;
        private float jumpIfGrounded;
        private bool grounded;

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Fire") || Input.GetAxis("Vertical") > 0)
            {
                jumpIfGrounded = 0.2f;
            }
        }

        private void FixedUpdate()
        {
            var dir = rigidBody.velocity.x;
            rigidBody.AddForce();
        }
    }
}
