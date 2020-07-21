using TMPro;
using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        public static float Time { get; private set; }
        public static int Lives;

        [SerializeField] private TextMeshProUGUI lifeDisplay;
        private static int _prevLives;

        private void Awake()
        {
            Time = -3f;
            Lives = 3;
            _prevLives = Lives;
            UpdateLives();
        }

        private void Update()
        {
            if (Lives != _prevLives) UpdateLives();

        }

        private void UpdateLives()
        {
            _prevLives = Lives;
            lifeDisplay.text = $"Health: {Lives}";
        }
    }
}
