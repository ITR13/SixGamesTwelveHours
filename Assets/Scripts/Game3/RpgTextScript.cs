using System;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI[] buttonTexts;
        [SerializeField] private Button backButton;
        [SerializeField] private TextMeshProUGUI text;
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
            string[] textToShow,
            Action[] actions,
            Action backAction
        )
        {
            textGo.SetActive(false);
            buttonParent.SetActive(true);

            for (var i = 0; i < 4; i++)
            {
                buttonTexts[i].text = textToShow[i];
            }


        }
    }
}
