using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Game2
{
    public class Game2Manager : MonoBehaviour
    {
        private enum ClickState
        {
            WaitingForPreClick,
            WaitingForClick,
            Clicked
        }

        [SerializeField] private Image circle;

        private float _fillAmount;
        private ClickState _currentClickState;

        private void Awake()
        {
            _fillAmount = -3;
            _currentClickState = ClickState.WaitingForClick;
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
            switch ()
            {
                
            }
        }
    }
}
