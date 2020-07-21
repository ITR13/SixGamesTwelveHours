using TMPro;
using UnityEngine;

namespace Game1
{
    public class Misses : MonoBehaviour
    {
        private static Misses _instance;
        private static int _count;
        private static float _fadeTimer = 0;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _instance = this;
            _count = 0;
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = "";
            _fadeTimer = 0;
        }

        private void Update()
        {
            if(_fadeTimer <= 0) return;
            _fadeTimer -= Time.deltaTime;
            if(_fadeTimer > 0) return;

            _text.text = "";
        }

        public static void Miss()
        {
            _count++;
            _fadeTimer = 0;
            var text = "X";
            for (var i = 1; i < _count; i++)
            {
                text += " X";
            }

            _instance._text.text = text;

            if (_count < 3) return;
            _fadeTime = 3;

        }

        public static void Reset()
        {
            _count
        }
    }
}
