using System;
using UnityEngine;

namespace Game1
{
    public class Click : MonoBehaviour
    {
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material.color = Color.HSVToRGB(0.66f, 1, 1);

                _remainingLifeTime = StartLifeTime;
        }

        [HideInInspector, NonSerialized]
        public float StartLifeTime;
        private float _remainingLifeTime;

        void Update()
        {
            _renderer.material.color = Color.HSVToRGB(
                Mathf.Lerp(0.66f, 1f, Lifetime)
            );
        }

        private void OnMouseDown()
        {
            Debug.Log("Test!");
        }

        private void OnDestroy()
        {
            Destroy(_renderer.material);
        }
    }
}
