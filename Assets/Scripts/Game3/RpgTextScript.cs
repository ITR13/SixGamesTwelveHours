using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game3
{
    public class RpgTextScript : MonoBehaviour
    {
        private RpgTextScript _instance;

        [SerializeField] private Button[] buttons;
        [SerializeField] private Button backButton;
        [SerializeField] private Text text;

        [SerializeField] private GameObject textGo;
        [SerializeField] private GameObject buttonParent;

        private void Awake()
        {
            _instance = this;
        }

        public static void SetText(string text, Action go)
        {

        }
    }
}
