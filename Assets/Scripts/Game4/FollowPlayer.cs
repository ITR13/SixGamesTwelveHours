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
        var pos = transform.position;
        pos.x = target.transform.position.x + _offset;

        var mousePos = (Input.mousePosition.x /(float) Screen.width) * 2 - 1;

        transform.position = pos;
    }
}
