using UnityEngine;

namespace Game5
{
    public class Manager : MonoBehaviour
    {
        public const int BitsPerSecond = 44100;
        public const float BeatsPerSecond = 1;

        public static readonly int ClipLength = Mathf.FloorToInt(
            44100 * 0.95f / BeatsPerSecond
        );

        public static readonly int FalloffLength = Mathf.FloorToInt(
            44100 * 0.1f / BeatsPerSecond
        );
        
        [SerializeField] private Row[] rows;

        private float _time;
        private int _beat;

        private void Update()
        {
            _time += Time.deltaTime / BeatsPerSecond;
            if(_time < _beat) return;
            foreach (var row in rows)
            {
                row.Play(_beat);
            }

            _beat++;
        }
    }
}
