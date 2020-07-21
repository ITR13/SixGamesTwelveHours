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
            
        }

        private void OnMouseDown()
        {
            Debug.Log("Test!");
        }

        private void OnDestroy()
        {
            
        }
    }
}
