using System;
using UnityEngine;

namespace Game4
{
    public class HackAndSlasher : MonoBehaviour
    {
        private Rigidbody2D rigidBody;
        private new Camera camera;
        private Transform arm;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            camera = Camera.main;
            arm = transform.Find("Arm");
        }

        private float horizontal;
        private float jumpIfGrounded;
        private bool grounded;
        private float _invulnerable;

        private void Update()
        {
            horizontal = Input.GetAxis("Horizontal");
            if (Input.GetButtonDown("Fire1") || Input.GetAxis("Vertical") > 0)
            {
                jumpIfGrounded = 0.2f;
            }

            var worldMousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            worldMousePos.z = arm.position.z;
            arm.right = worldMousePos - arm.position;
        }

        private void FixedUpdate()
        {
            var dir = rigidBody.velocity.x;
            var xForce = horizontal * 8;

            if (dir * xForce < 0)
            {
                xForce *= 2;
            }
            else if (Math.Abs(xForce) < 0.01f)
            {
                xForce = -dir * 2;
            }
            else if (Mathf.Abs(dir) > 5)
            {
                xForce = 0;
            }

            var yForce = 0;
            if (grounded && jumpIfGrounded > 0)
            {
                grounded = false;
                yForce = 20;
            }

            jumpIfGrounded -= Time.fixedDeltaTime;

            rigidBody.AddForce(new Vector2(xForce, 0));
            rigidBody.AddForce(new Vector2(0, yForce), ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                grounded = true;
                return;
            }

            if (_invulnerable > 0) return;

            if (other.gameObject.CompareTag("Enemy"))
            {
                var dir = rigidBody.velocity.x;
            }
        }
    }
}
