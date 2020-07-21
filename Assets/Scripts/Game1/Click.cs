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
            if (Game1Manager.Paused) return;
            _remainingLifeTime -= Time.deltaTime;
            _renderer.material.color = Color.HSVToRGB(
                Mathf.Lerp(1f, 0.66f, _remainingLifeTime/StartLifeTime),
                1,
                1
            );
        }

        private void FixedUpdate()
        {
            if (Game1Manager.Paused) return;
            if (!(_remainingLifeTime <= 0.02f)) return;
            Game1Manager.Lives--;
            Destroy(gameObject);
        }

        private void OnMouseDown()
        {
            if (Game1Manager.Paused) return;

            Destroy(gameObject);
            Game1Manager.Score = 10 + Mathf.CeilToInt(_remainingLifeTime);
        }

        private void OnDestroy()
        {
            Destroy(_renderer.material);
        }
    }
}
