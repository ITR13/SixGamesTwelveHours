using System;
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
            Beep,
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
                frequency.ToString(),
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
                var t = 2f * i * Mathf.PI * frequency;
                var h = wave(t);
                data[i] = h;
            }

            for (var i = 0; i < falloffSize; i++)
            {
                var t = 2f * (i + preFalloff) * Mathf.PI * frequency;
                var h = wave(t);
                var scale = 1 - i / (float) falloffSize;
                data[i + preFalloff] = h * scale;
            }

            return data;
        }
    }
}
