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
            var length = Mathf.FloorToInt(Mathf.Lerp(0.6f, 1f, TotalError) * Bps);
            var clip = AudioClip.Create("beep", length, 1, Bps, false);
            clip.SetData(
                GenerateAudio(
                    length,
                    Bps / 5,
                    440
                ),
                0
            );
            audioSource.clip = clip;
            SaveAudioClipToWav(clip, "./audio.wav");
            audioSource.Play();
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

            var byteArray = new byte[data.Length * 4];
            Buffer.BlockCopy(data, 0, byteArray, 0, byteArray.Length);
            File.WriteAllBytes("./audio", byteArray);

            return data;
        }

        static public void SaveAudioClipToWav(AudioClip audioClip, string filename)
        {
            FileStream fsWrite = File.Open(filename, FileMode.Create);

            BinaryWriter bw = new BinaryWriter(fsWrite);

            Byte[] header = { 82, 73, 70, 70, 22, 10, 4, 0, 87, 65, 86, 69, 102, 109, 116, 32 };
            bw.Write(header);

            Byte[] header2 = { 16, 0, 0, 0, 1, 0, 1, 0, 68, 172, 0, 0, 136, 88, 1, 0 };
            bw.Write(header2);

            Byte[] header3 = { 2, 0, 16, 0, 100, 97, 116, 97, 152, 9, 4, 0 };
            bw.Write(header3);

            float[] samples = new float[audioClip.samples];
            audioClip.GetData(samples, 0);
            int i = 0;

            while (i < audioClip.samples)
            {
                int sampleInt = (int)(32000.0 * samples[i++]);

                int msb = sampleInt / 256;
                int lsb = sampleInt - (msb * 256);

                bw.Write((Byte)lsb);
                bw.Write((Byte)msb);
            }

            fsWrite.Close();

        }
    }
}
