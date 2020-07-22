using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TtsPlayer : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    private Vector3 movementDir;

    private void Update()
    {
        var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        movementDir = transform.position - worldPoint;
    }

    private void FixedUpdate()
    {
        movementDir.z = 0;
        var dist = movementDir.sqrMagnitude;
        movementDir.Normalize();
        transform.position += movementDir *
                              Time.fixedDeltaTime *
                              Mathf.Lerp(

                                  dist/100
                              );
    }
}
