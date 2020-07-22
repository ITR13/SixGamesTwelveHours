using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TtsPlayer : MonoBehaviour
{
    [SerializeField] private new Camera camera;


    private void Update()
    {
        var worldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        
    }
}
