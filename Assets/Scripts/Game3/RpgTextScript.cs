using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game3
{
    public class RpgTextScript : MonoBehaviour
    {
        public static RpgTextScript Instance { get; private set; };

        [SerializeField] private Button[] buttons;
        [SerializeField] private Button backButton;
        [SerializeField] private Text text;
        [SerializeField] private Button textButton;

        [SerializeField] private GameObject textGo;
        [SerializeField] private GameObject buttonParent;

        private void Awake()
        {
            Instance = this;
        }

        public void SetText(string textToShow, Action go)
        {
            textGo.SetActive(true);
            buttonParent.SetActive(false);
            text.text = textToShow;
            
            textButton.onClick.RemoveAllListeners();
            textButton.onClick.AddListener(new UnityAction(go));
        }

        public void SetButtons(
            string[] text,
            Action[] actions,
            Action backAction
        )
        {
            for (var i = 0; i < 4; i++)
            {

            }


        }
    }
}
