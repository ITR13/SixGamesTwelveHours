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

        [NonSerialized]
        public float StartLifeTime = 5;
        private float _remainingLifeTime;

        void Update()
        {
            _remainingLifeTime -= Time.deltaTime;
            _renderer.material.color = Color.HSVToRGB(
                Mathf.Lerp(1f, 0.66f, _remainingLifeTime/StartLifeTime),
                1,
                1
            );
        }

        private void FixedUpdate()
        {
            if (_remainingLifeTime <= 0.02f)
            {
                Destroy(gameObject);
            }
        }

        private void OnMouseDown()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Destroy(_renderer.material);
        }
    }
}
