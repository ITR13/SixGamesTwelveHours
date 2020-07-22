using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TtsPlayer : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject bullet;

    private Vector3 movementDir;
    private Vector3 shootDir;

    private void Update()
    {
        var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        movementDir = transform.position - worldPoint;

        shootDir = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0
        );

        transform.up = shootDir;
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

        if (shootDir.sqrMagnitude > 0.1f)
        {
            Shoot(shootDir.normalized);
        }
    }

    private void Shoot(Vector3 direction)
    {
        var bullet = Instantiate(bullet);
        bullet.transform.up = direction;
        bullet.transform.position = transform.position + direction / 2;
    }
}
