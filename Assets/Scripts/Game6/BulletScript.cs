using UnityEngine;

namespace Game6
{
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
}
