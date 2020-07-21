using System;
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
            var xForce = horizontal * 4;
            if (dir * xForce < 0)
            {
                xForce *= 2;
            }
            else if (Math.Abs(xForce) < 0.01f)
            {
                xForce = -dir * 2;
            }

            var yForce = 0;
            if (grounded && jumpIfGrounded > 0)
            {
                grounded = false;
                yForce = 10;
            }

            jumpIfGrounded -= Time.fixedDeltaTime;

            rigidBody.AddForce(new Vector2(xForce, yForce));
        }
    }
}
