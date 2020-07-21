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
        public float StartLifeTime;
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
            if (!(_remainingLifeTime <= 0.02f)) return;
            Game1Manager.Lives--;
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            Destroy(gameObject);
            Game1Manager.Score = ;
        }

        private void OnDestroy()
        {
            Destroy(_renderer.material);
        }
    }
}
