using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 20f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.fixedDeltaTime * 25);
    }
}
