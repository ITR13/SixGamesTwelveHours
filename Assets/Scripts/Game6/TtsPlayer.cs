using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TtsPlayer : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    private Vector2 movementDir;

    private void Update()
    {
        var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
        movementDir = transform.position - worldPoint;
    }

    private void FixedUpdate()
    {
        transform.position += movementDir * Time.deltaTime;
    }
}
