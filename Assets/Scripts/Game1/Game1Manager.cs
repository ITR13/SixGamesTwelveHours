using System.Collections.Generic;
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
                _instance.scoreDisplay.text = $"Score: {value}";
                _score = value;
            }
        }

        public static int Lives
        {
            get => _prevLives;
            set
            {
                _instance.lifeDisplay.text = value < 0 ? "DEAD" : $"HP: {value}";
                _prevLives = value;
            }
        }

        private static int Highscore
        {
            get => PlayerPrefs.GetInt(HighscoreKey, 0);
            set => PlayerPrefs.SetInt(HighscoreKey, value);
        }


        [SerializeField] private Click clickPrefab;
        [SerializeField] private SubMenu subMenu;

        [SerializeField] private TextMeshProUGUI lifeDisplay;
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private TextMeshProUGUI highscoreDisplay;
        private static int _prevLives;
        private static int _score;

        private float _healthTimer;
        private float _spawnTimer;
        private int _burst;

        private void Awake()
        {
            _instance = this;

            Paused = false;
            subMenu.gameObject.SetActive(false);

            Runtime = -3f;
            Lives = 3;
            Score = 0;
            highscoreDisplay.text = $"Highscore: {Highscore}";

            _spawnTimer = 0f;
            _healthTimer = 0f;
            _burst = 0;
        }

        private void Update()
        {
            if (Paused) return;

            Runtime += Time.deltaTime;
            if(Runtime < 0f) return;

            CheckClick();

            _spawnTimer -= Time.deltaTime;
            _healthTimer -= Time.deltaTime;

            if (_spawnTimer <= 0) SpawnClickMe();
            if (_healthTimer <= 0) RegenHealth();

            if(Lives < 0) GameOver();
        }

        private void SpawnClickMe()
        {
            if (_burst <= 0)
            {
                _spawnTimer += Mathf.Lerp(
                    4f,
                    1f,
                    Mathf.Sqrt(Runtime) / 10f
                );
                _burst = 3;
            }
            else
            {
                _spawnTimer += Mathf.Lerp(
                    2f,
                    0.5f,
                    Runtime / 60f
                );
                _burst--;
            }

            var click = Instantiate(clickPrefab);
            click.StartLifeTime = Mathf.Lerp(5, 1.5f, (Runtime - 10f)/40);
            var position = Random.insideUnitCircle;
            position.x *= 7.5f;
            position.y *= 4f;
            click.transform.position = position;
        }

        private void RegenHealth()
        {
            if (Lives < 3) Lives++;
            _healthTimer += Runtime < 60 ? 20 : 120;
        }

        private void CheckClick()
        {
            if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var results = new List<Collider2D>();
            var contactFilter = new ContactFilter2D();
            contactFilter.NoFilter();

            var hits = Physics2D.OverlapPoint(
                worldPoint,
                contactFilter,
                results
            );
            if (hits == 0)
            {
                Misses.Miss();
                return;
            }

            Misses.Hit();
            foreach (var result in results)
            {
                result.GetComponent<Click>().Clicked();
            }
        }

        private void GameOver()
        {
            Paused = true;

            if (Score > Highscore)
            {
                Highscore = Score;
            }

            subMenu.gameObject.SetActive(true);
        }
    }
}
