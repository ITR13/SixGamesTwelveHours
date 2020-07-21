using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Game2
{
    public class Game2Manager : MonoBehaviour
    {
        [SerializeField] private Image circle;

        private float _fillAmount;

        private void Awake()
        {
            _fillAmount = -3;
        }

        private void Awake()
        {
            _fillAmount += Time.deltaTime;
            circle.fillAmount = _fillAmount % 1;

            if (_fillAmount < 1) return;

            _fillAmount -= 1;
            ExpendClick();
        }

        private void ExpendClick()
        {

        }
    }
}
