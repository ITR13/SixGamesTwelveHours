using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game5
{
    public class Row : MonoBehaviour
    {
        private enum ButtonState
        {
            Off,
            Sine,
            SawTooth,
            Triangle,

            MAX,
        }


        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button[] buttons;
        [SerializeField] private Image[] buttonImages;

        private readonly Color[] colors = new[]
        {
            Color.gray,
            Color.red,
            Color.blue,
            Color.green,
        };

        private AudioClip _audioClip;
        private ButtonState[] _buttonStates;
        private float _frequency;
        private float _volume;

        private void Awake()
        {
            _audioClip = AudioClip.Create(
                _frequency.ToString(CultureInfo.InvariantCulture),
                Manager.ClipLength,
                1,
                Manager.BitsPerSecond,
                false
            );
            _buttonStates = new ButtonState[buttons.Length];

            for (var i = 0; i < buttons.Length; i++)
            {
                var index = i;
                var button = buttons[i];
                var image = buttonImages[i];

                button.onClick.AddListener(
                    () =>
                    {
                        _buttonStates[index] =
                            (ButtonState)(
                                ((int)_buttonStates[index] + 1) %
                                (int)ButtonState.MAX
                            );
                        image.color = colors[(int)_buttonStates[index]];

                    }
                );
                image.color = colors[(int)_buttonStates[index]];
            }
        }

        public void Play(int index)
        {
            index %= buttons.Length;
            switch (_buttonStates[index])
            {
                case ButtonState.Off:
                    break;
                case ButtonState.Sine:
                    PlayBeep(t => Mathf.Sin(t * Mathf.PI * 2));
                    break;
                case ButtonState.SawTooth:
                    PlayBeep(t => Mathf.Repeat(t, 1) * 0.5f);
                    break;
                case ButtonState.Triangle:
                    PlayBeep(t => Mathf.PingPong(t * 2, 1));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayBeep(Func<float, float> wave)
        {
            _audioClip.SetData(
                GenerateAudio(
                    Manager.ClipLength,
                    Manager.FalloffLength,
                    _frequency,
                    wave
                ),
                0
            );
            audioSource.PlayOneShot(_audioClip, 0.15f + Mathf.Log(_frequency / 440));
        }

        private float[] GenerateAudio(
            int totalSize,
            int falloffSize,
            float hz,
            Func<float, float> wave
        )
        {
            var preFalloff = totalSize - falloffSize;

            var data = new float[totalSize];

            var frequency = hz / Manager.BitsPerSecond;

            for (var i = 0; i < preFalloff; i++)
            {
                var t = i * frequency;
                var h = wave(t);
                data[i] = h;
            }

            for (var i = 0; i < falloffSize; i++)
            {
                var t = (i + preFalloff) * frequency;
                var h = wave(t);
                var scale = 1 - i / (float)falloffSize;
                data[i + preFalloff] = h * scale;
            }

            return data;
        }

        public void Set(string note, float frequency)
        {
            gameObject.name = note;
            _frequency = frequency;
            GetComponentInChildren<TextMeshProUGUI>().text = note;
        }
    }
}
