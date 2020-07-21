using TMPro;
using UnityEngine;

namespace Game1
{
    public class Misses : MonoBehaviour
    {
        private static Misses _misses;
        private static int _count;
        private TextMeshProUGUI _text;
        private float fadeTimer = 0;

        private void Awake()
        {
            _misses = this;
            _count = 0;
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = "";
        }

        private void Update()
        {

        }

        public static void Miss()
        {

        }

        public static void Reset()
        {
            _text
        }
    }
}
