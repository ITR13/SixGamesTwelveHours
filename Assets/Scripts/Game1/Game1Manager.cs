using TMPro;
using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        private const string HighscoreKey = nameof(Game1Manager) + ".Highscore";

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

        private static int Highscore
        {
            get => PlayerPrefs.GetInt(HighscoreKey, 0);
            set => PlayerPrefs.SetInt(HighscoreKey, value);
        }


        [SerializeField] private Click clickPrefab;

        [SerializeField] private TextMeshProUGUI lifeDisplay;
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private TextMeshProUGUI highscoreDisplay;
        private static int _prevLives;
        private static int _score;

        private void Awake()
        {
            _instance = this;

            Runtime = -3f;
            Lives = 3;
            Score = 0;
            highscoreDisplay.text = $"Highscore: {Highscore}";
        }

        private int t;
        private void Update()
        {
            Runtime += Time.deltaTime;
            if (Runtime >= t)
            {
                t += 1;
                Instantiate(clickPrefab);
            }
        }

        private void GameOver()
        {
            if (Score > Highscore)
            {
                Highscore = Score;
            }

            // Show menu
        }
    }
}
