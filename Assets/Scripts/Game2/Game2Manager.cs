using System;
using System.ComponentModel;
using System.IO;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Game2
{
    public class Game2Manager : MonoBehaviour
    {
        private const int Bps = 44100;
        private const string HighscoreKey = nameof(Game2Manager) + ".Highscore";

        private enum ClickState
        {
            WaitingForPreClick,
            WaitingForClick,
            Clicked
        }

        [SerializeField] private GameObject subMenu;
        [SerializeField] private Image circle;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI highscoreText;
        [SerializeField] private AudioSource audioSource;

        private static int Highscore
        {
            get => PlayerPrefs.GetInt(HighscoreKey, 0);
            set => PlayerPrefs.SetInt(HighscoreKey, value);
        }

        private float TotalError
        {
            get => _totalError;
            set
            {
                _totalError = value;
                circle.color = Color.HSVToRGB(0, Mathf.Clamp01(value), 1);
            }
        }

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                scoreText.text = value.ToString();
            }
        }

        private int _score;
        private float _totalError;
        private int _warmup;
        private float _fillAmount;
        private ClickState _currentClickState;
        public bool Paused { get; set; }

        private void Awake()
        {
            _warmup = -2;
            _fillAmount = 0;
            _currentClickState = ClickState.WaitingForPreClick;

            Score = 0;
            TotalError = 0;

            highscoreText.text = Highscore.ToString();
            subMenu.SetActive(false);
        }

        private void Update()
        {
            if (Paused) return;

            if (Input.anyKeyDown) OnClick();

            _fillAmount += Time.deltaTime;
            circle.fillAmount = _fillAmount % 1;

            if (_fillAmount < 1) return;
            _fillAmount -= 1;
            PlayBeep();

            if (_warmup++ < 0) return;
            Debug.Log($"Filled!: {_currentClickState}");
            ExpendClick();
        }

        private void ExpendClick()
        {
            switch (_currentClickState)
            {
                case ClickState.WaitingForPreClick:
                    _currentClickState = ClickState.WaitingForClick;
                    break;
                case ClickState.WaitingForClick:
                    GameOver();
                    break;
                case ClickState.Clicked:
                    _currentClickState = ClickState.WaitingForPreClick;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GameOver()
        {
            Paused = true;
            circle.fillAmount = 1;

            if (Score > Highscore)
            {
                Highscore = Score;
            }

            subMenu.gameObject.SetActive(true);
        }

        private void OnClick()
        {
            Resources.UnloadUnusedAssets();
            if (_warmup < 0) return;
            if (_warmup == 0 && _fillAmount < 0.5f) return;
            Debug.Log($"Click!: {_currentClickState}");

            switch (_currentClickState)
            {
                case ClickState.WaitingForPreClick:
                    _currentClickState = ClickState.Clicked;
                    CheckClick(true);
                    break;
                case ClickState.WaitingForClick:
                    _currentClickState = ClickState.WaitingForPreClick;
                    CheckClick(false);
                    break;
                case ClickState.Clicked:
                    GameOver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CheckClick(bool pre)
        {
            var error = pre ? 1 - _fillAmount : _fillAmount;
            TotalError += error;
            Score++;

            Debug.Log($"Error: {error}    Total: {TotalError}\nScore: {Score}");

            if (_totalError > 1)
            {
                GameOver();
            }
        }

        private void PlayBeep()
        {
            var length = Mathf.FloorToInt(Mathf.Lerp(0.3f, 0.75f, TotalError) * Bps);
            var frequency = 440 * Mathf.Pow(2, -TotalError * 5);
            var clip = AudioClip.Create("beep", length, 1, Bps, false);
            clip.SetData(
                GenerateAudio(
                    length,
                    Bps / 5,
                    frequency
                ),
                0
            );
            audioSource.PlayOneShot(clip, 0.35f);
        }

        private float[] GenerateAudio(int totalSize, int falloffSize, float hz)
        {
            var preFalloff = totalSize - falloffSize;

            var data = new float[totalSize];

            var frequency = hz / Bps;

            for (var i = 0; i < preFalloff; i++)
            {
                var t = 2f * i * Mathf.PI * frequency;
                var h = Mathf.Sin(t);
                data[i] = h;
            }

            for (var i = 0; i < falloffSize; i++)
            {
                var t = 2f * (i + preFalloff) * Mathf.PI * frequency;
                var h = Mathf.Sin(t);
                var scale = 1 - i / (float)falloffSize;
                data[i + preFalloff] = h * scale;
            }

            return data;
        }
    }
}
