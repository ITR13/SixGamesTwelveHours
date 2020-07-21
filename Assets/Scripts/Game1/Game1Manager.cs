using TMPro;
using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        private static Game1Manager _instance;

        public static float Time { get; private set; }

        public static int Score
        {
            get => _score;
            set
            {
                _instance.scoreDisplay.text = $"Score: {Score}";
                _score = value;
            }
        }

        public static int Lives
        {
            get => _prevLives;
            set
            {
                _instance.lifeDisplay.text = $"HP: {Score}";
                _prevLives = value;
            }
        }

        [SerializeField] private TextMeshProUGUI lifeDisplay;
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private TextMeshProUGUI highscoreDisplay;
        private static int _prevLives;
        private static int _score;

        private void Awake()
        {
            Time = -3f;
            Lives = 3;
            _prevLives = Lives;
            UpdateLives();
        }

        private void UpdateLives()
        {
            _prevLives = Lives;
            lifeDisplay.text = $"Health: {Lives}";
        }
    }
}
