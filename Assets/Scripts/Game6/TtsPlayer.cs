using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TtsPlayer : MonoBehaviour
{
    private const float ShootInterval = 0.4f;

    [SerializeField] private new Camera camera;
    [SerializeField] private Transform visuals;
    [SerializeField] private BulletScript bulletPrefab;

    private Vector3 movementDir;
    private Vector3 shootDir;

    private float shootCooldown;

    private void Update()
    {
        var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        movementDir = transform.position - worldPoint;

        shootDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {

        }

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
        transform.position += movementDir *
                              Time.fixedDeltaTime *
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
}
