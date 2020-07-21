using TMPro;
using UnityEngine;

namespace Game1
{
    public class Misses : MonoBehaviour
    {
        private static Misses _misses;
        private static int _count;
        private TextMeshProUGUI _text;
        private float _fadeTimer = 0;

        private void Awake()
        {
            _misses = this;
            _count = 0;
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = "";
        }

        private void Update()
        {
            if(_fadeTimer <= 0) return;
            _fadeTimer -= Time.deltaTime;
            if(_fadeTimer > 0) return;


        }

        public static void Miss()
        {

        }

        public static void Reset()
        {
            _count
        }
    }
}
