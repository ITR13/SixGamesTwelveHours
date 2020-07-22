using UnityEngine;

namespace Game6
{
    public class BulletScript : MonoBehaviour
    {
        [SerializeField] private new Rigidbody2D rigidbody;

        private void Awake()
        {
            Destroy(gameObject, 20f);
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = transform.up * 25;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("TRIGGER");
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("COLLISIOn");
        }
    }
}
