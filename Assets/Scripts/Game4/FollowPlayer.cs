using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    private float _offset;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _offset = transform.position.x - target.transform.position.x;
    }

    private void LateUpdate()
    {
        var pos = transform.position;
        pos.x = target.transform.position.x + _offset;
        transform.position = pos;
    }
}
