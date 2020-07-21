using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        public static float Time { get; private set; }
        public static int Lives;

        private void Awake()
        {
            Time = -3f;
        }

        private void Update()
        {

        }
    }
}
