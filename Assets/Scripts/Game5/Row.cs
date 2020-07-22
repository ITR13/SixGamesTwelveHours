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

        private AudioClip audioClip;
        private ButtonState[] buttonStates;

        private void Awake()
        {
            audioClip = AudioClip.Create(
                frequency.ToString(),
                Bps * 9 / 10,
                1,
                Bps,
                false
            );
            buttonStates = new ButtonState[buttons.Length];
        }

        private void PlayBeep()
        {
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
