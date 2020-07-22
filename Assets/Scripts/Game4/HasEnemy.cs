using UnityEngine;

namespace Game4
{
    public class HasEnemy : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<HackAndSlasher>();
            if (player == null) return;
            player.HurtMe();
        }
    }
}
