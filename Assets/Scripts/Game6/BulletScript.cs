using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(transform.forward * Time.deltaTime * 5);
    }
}
