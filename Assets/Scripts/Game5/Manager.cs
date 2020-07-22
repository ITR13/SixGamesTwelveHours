using UnityEngine;

namespace Game5
{
    public class Manager : MonoBehaviour
    {
        public const int BitsPerSecond = 44100;
        public const float BeatsPerSecond = 2;

        public readonly int ClipLength = Mathf.FloorToInt(
            44100 / BeatsPerSecond
        );
        

        [SerializeField] private Row[] rows;

    }
}
