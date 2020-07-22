using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game6
{
    public class TtsPlayer : MonoBehaviour
    {
        private const float ShootInterval = 0.1f;

        [SerializeField] private new Camera camera;
        [SerializeField] private Transform visuals;
        [SerializeField] private BulletScript bulletPrefab;
        [SerializeField] private new Rigidbody2D rigidbody;

        private Vector3 movementDir;
        private Vector3 shootDir;

        private float shootCooldown;
        private int _hurtLayer;

        private void Awake()
        {
            _hurtLayer = LayerMask.NameToLayer("Hurt");
        }

        private void Update()
        {
            var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            movementDir = transform.position - worldPoint;

            shootDir = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );

            if (shootDir.sqrMagnitude > 0.1f)
            {
                visuals.transform.up = shootDir;
            }
        }

        private void FixedUpdate()
        {
            movementDir.z = 0;
            var dist = movementDir.sqrMagnitude;
            movementDir.Normalize();

            rigidbody.velocity = movementDir *
                                  Mathf.Lerp(
                                      10,
                                      2,
                                      (dist-4)/100
                                  );

            if (shootCooldown <= 0)
            {
                shootCooldown = 0;
            }
            else
            {
                shootCooldown -= Time.fixedDeltaTime;
            }

            if (shootDir.sqrMagnitude > 0.1f && shootCooldown <= 0)
            {
                shootCooldown += ShootInterval;
                Shoot(shootDir.normalized);
            }
        }

        private void Shoot(Vector3 direction)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.up = direction;
            bullet.transform.position = transform.position + direction / 2;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != _hurtLayer) return;
            transform.position =
                SavePoint.CurrentSavePoint == null
                    ? Vector3.zero
                    : SavePoint.CurrentSavePoint.transform.position;
        }
    }
}
