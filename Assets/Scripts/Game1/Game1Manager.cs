using TMPro;
using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        private static Game1Manager _instance;

        public static float Runtime { get; private set; }

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
            Runtime = -3f;
            Lives = 3;
            Score = 0;

        }

        private void Update()
        {
            Runtime += Time.deltaTime;
        }
    }
}
