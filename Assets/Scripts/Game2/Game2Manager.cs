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
        private const int Bps = 10;

        private enum ClickState
        {
            WaitingForPreClick,
            WaitingForClick,
            Clicked
        }

        [SerializeField] private Image circle;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private AudioSource audioSource;

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

        private void Awake()
        {
            _warmup = -2;
            _fillAmount = 0;
            _currentClickState = ClickState.WaitingForPreClick;

            Score = 0;
            TotalError = 0;
        }

        private void Update()
        {
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
            Time.timeScale = 0;
            Debug.Log("Game Over");
        }

        private void OnClick()
        {
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
            var length = Mathf.FloorToInt(2 * 1000);
            var clip = AudioClip.Create("beep", length, 1, Bps, false);
            clip.SetData(
                GenerateAudio(
                    length,
                    200,
                    440
                ),
                0
            );
            audioSource.PlayOneShot(clip);
        }

        private float[] GenerateAudio(int ms, int falloutMs, float hz)
        {
            var totalSize = Bps * ms / 1000;
            var falloffSize = falloutMs * Bps / 1000;
            var preFalloff = totalSize - falloffSize;

            var data = new float[totalSize];

            hz *= Bps;

            for (var i = 0; i < preFalloff; i++)
            {
                var t = 2 * i * Mathf.PI / hz;
                var h = Mathf.Sin(t);
                data[i] = h;
            }

            for (var i = 0; i < falloffSize; i++)
            {
                var t = 2 * i * Mathf.PI / hz;
                var h = Mathf.Sin(t);
                var scale = 1 - i / (float)falloffSize;
                data[i + preFalloff] = h * scale;
            }


            return data;
        }
    }
}
