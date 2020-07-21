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
            get => _prevScore;
            set
            {
                _instance.scoreDisplay.text = $"Score: {Score}";
                _prevScore = Score;
            }
        }

        public static int Lives;

        [SerializeField] private TextMeshProUGUI lifeDisplay;
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private TextMeshProUGUI highscoreDisplay;
        private static int _prevLives;
        private static int _prevScore;

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
            if(_prevScore != _prevScore)
        }

        private void UpdateLives()
        {
            _prevLives = Lives;
            lifeDisplay.text = $"Health: {Lives}";
        }
    }
}
