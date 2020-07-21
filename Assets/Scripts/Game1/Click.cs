using UnityEngine;

namespace Game1
{
    public class Click : MonoBehaviour
    {
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        public float Lifetime;

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
