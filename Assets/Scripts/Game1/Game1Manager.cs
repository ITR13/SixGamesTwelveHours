using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        public static float Time { get; private set; }
        public static int Lives;

        private static int _prevLives;

        private void Awake()
        {
            Time = -3f;
            Lives = 3;
            _prevLives = Lives;
        }

        private void Update()
        {
            if (Lives != _prevLives)
            {
                UpdateLives();
            }
        }

        private void UpdateLives()
        {
            _prevLives = Lives;

        }
    }
}
