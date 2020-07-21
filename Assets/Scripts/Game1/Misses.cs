using TMPro;
using UnityEngine;

namespace Game1
{
    public class Misses : MonoBehaviour
    {
        private static Misses _misses;
        private int _count;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _misses = this;
            _count = 0;
            _text = GetComponent<TextMeshProUGUI>();
            _text.text = "";
        }

        public static void 
    }
}
