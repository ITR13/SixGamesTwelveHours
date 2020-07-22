using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float _offset;

    private void Awake()
    {
        _offset = transform.position.x - target.transform.position.x;
    }

    private void LateUpdate()
    {
        var mousePos = (Input.mousePosition.x / (float)Screen.width) - 0.5f;
        mousePos = Mathf.Clamp01(mousePos * 2 * 6);
        var targetPos = transform.position;
        targetPos.x = target.transform.position.x + _offset * mousePos;
        transform.position = targetPos;
    }
}
