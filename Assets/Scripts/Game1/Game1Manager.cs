using TMPro;
using UnityEngine;

namespace Game1
{
    public class Game1Manager : MonoBehaviour
    {
        private const string HighscoreKey = nameof(Game1Manager) + ".Highscore";

        private static Game1Manager _instance;

        public static float Runtime { get; private set; }
        public static bool Paused { get; private set; }

        public static int Score
        {
            get => _score;
            set
            {
                Debug.Log($"Score set to {value}");
                _instance.scoreDisplay.text = $"Score: {value}";
                _score = value;
            }
        }

        public static int Lives
        {
            get => _prevLives;
            set
            {
                Debug.Log($"Health set to {value}");
                _instance.lifeDisplay.text = $"HP: {value}";
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

        private float _healthTimer;
        private float _spawnTimer;

        private void Awake()
        {
            _instance = this;

            Paused = false;

            Runtime = -3f;
            Lives = 3;
            Score = 0;
            highscoreDisplay.text = $"Highscore: {Highscore}";
        }

        private void Update()
        {
            if (Paused) return;

            Runtime += Time.deltaTime;
            _spawnTimer -= Time.deltaTime;
            _healthTimer -= Time.deltaTime;

            if (_spawnTimer <= 0) SpawnClickMe();
            if (_healthTimer <= 0) RegenHealth();

            if(Lives < 0) GameOver();
        }

        private void SpawnClickMe()
        {
            _spawnTimer = Mathf.Lerp(7f, 1f, Runtime / 60f);

            var click = Instantiate(clickPrefab);
            click.StartLifeTime = Mathf.Lerp(5, 1.5f, (Runtime - 10f)/40);
        }

        private void RegenHealth()
        {
            if (Lives < 3) Lives++;
            _healthTimer += Runtime < 60 ? 20 : 120;
        }

        private void GameOver()
        {
            Paused = true;

            if (Score > Highscore)
            {
                Highscore = Score;
            }

            // Show menu
        }
    }
}
