using System;
using UnityEngine;

namespace Game4
{
    public class HackAndSlasher : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody;
        private new Camera camera;
        private Transform arm;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            camera = Camera.main;
            arm = transform.Find("Arm");
            _invulnerable = 5;
        }

        private float horizontal;
        private float jumpIfGrounded;
        private bool grounded;
        private float _invulnerable;

        private void Update()
        {
            _invulnerable -= Time.deltaTime;
            spriteRenderer.color = new Color(
                1, 1, 1, Mathf.Clamp01(0.5f - _invulnerable) + 0.5f
            );
            spriteRenderer.enabled =
                _invulnerable <= 0.5f || (_invulnerable % 0.4f > 0.15f);

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
        }

        public void HurtMe()
        {
            if(_invulnerable > 0) return;
            var dir = -Mathf.Sign(rigidBody.velocity.x);
            _invulnerable = 2.7f;
            rigidBody.AddForce(
                new Vector2(dir * 8, 0),
                ForceMode2D.Impulse
            );
        }
    }
}
