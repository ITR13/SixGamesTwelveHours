using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Game3
{
    public class RpgTextScript : MonoBehaviour
    {
        private static RpgTextScript _instance;

        [SerializeField] private Button[] buttons;
        [SerializeField] private Button backButton;
        [SerializeField] private Text text;
        [SerializeField] private Button textButton;

        [SerializeField] private GameObject textGo;
        [SerializeField] private GameObject buttonParent;

        private void Awake()
        {
            _instance = this;
        }

        public static void SetText(string text, Action go)
        {
            _instance
        }
    }
}
