using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game5
{
    public class Row : MonoBehaviour
    {
        private const int Bps = 44100;

        private enum ButtonState
        {
            Off,
            Sine,
            SawTooth
        }


        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Button[] buttons;
        [SerializeField] private Image[] buttonImages;
        [SerializeField] private float frequency;

        private AudioClip _audioClip;
        private ButtonState[] _buttonStates;

        private void Awake()
        {
            _audioClip = AudioClip.Create(
                frequency.ToString(CultureInfo.InvariantCulture),
                Bps * 9 / 10,
                1,
                Bps,
                false
            );
            _buttonStates = new ButtonState[buttons.Length];
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
                    PlayBeep(t => Mathf.Repeat(t, 1));
                    break;
                case ButtonState.Triangle:
                    PlayBeep(t => Mathf.PingPong(t * 2, 1));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayBeep(Func<float, float> wave)
        {
            _audioClip.SetData(
                GenerateAudio(
                    Bps * 9 / 10,
                    Bps / 5,
                    frequency,
                    wave
                ),
                0
            );
            audioSource.PlayOneShot(_audioClip, 0.35f);
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

            var frequency = hz / Bps;

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
                var scale = 1 - i / (float) falloffSize;
                data[i + preFalloff] = h * scale;
            }

            return data;
        }
    }
}
