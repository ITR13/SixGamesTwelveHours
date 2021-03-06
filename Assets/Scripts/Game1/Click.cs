using System;
using UnityEngine;

namespace Game1
{
    public class Click : MonoBehaviour
    {
        private Renderer _renderer;
        private float _fade;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _renderer.material.color = Color.HSVToRGB(0.66f, 1, 1);
        }

        public float StartLifeTime
        {
            get => _startLifeTime;
            set
            {
                _startLifeTime = value;
                _remainingLifeTime = value;
            }
        }

        private float _startLifeTime;
        private float _remainingLifeTime;

        void Update()
        {

            if (_fade > 0)
            {
                _fade -= Time.deltaTime * 5;
                if (_fade < 0)
                {
                    Destroy(gameObject);
                    return;
                }
                var color = Color.HSVToRGB(
                    Mathf.Lerp(1f, 0.66f, _remainingLifeTime / StartLifeTime),
                    1,
                    1
                );
                color.a = _fade;
                _renderer.material.color = color;
                return;
            }

            if (Game1Manager.Paused) return;
            _remainingLifeTime -= Time.deltaTime;
            _renderer.material.color = Color.HSVToRGB(
                Mathf.Lerp(1f, 0.66f, _remainingLifeTime / StartLifeTime),
                1,
                1
            );
        }

        private void FixedUpdate()
        {
            if (_fade > 0) return;
            if (Game1Manager.Paused) return;
            if (_remainingLifeTime > -0.02f) return;
            Game1Manager.Lives--;
            Destroy(gameObject);
        }

        public void Clicked()
        {
            if (_fade > 0) return;
            if (Game1Manager.Paused) return;

            Game1Manager.Score += 10 + Mathf.CeilToInt(_remainingLifeTime);
            _fade = 1f;
        }

        private void OnDestroy()
        {
            Destroy(_renderer.material);
        }
    }
}
