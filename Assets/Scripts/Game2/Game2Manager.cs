using System;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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

        private void Update()
        {
            _fillAmount += Time.deltaTime;
            circle.fillAmount = _fillAmount % 1;

            if (_fillAmount < 1) return;

            _fillAmount -= 1;
            ExpendClick();
        }

        private void ExpendClick()
        {
            switch (_currentClickState)
            {
                case ClickState.WaitingForPreClick:
                    _currentClickState = ClickState.WaitingForClick;
                    break;
                case ClickState.WaitingForClick:
                    GameOver();
                    break;
                case ClickState.Clicked:
                    _currentClickState = ClickState.WaitingForPreClick;
                    CheckClick(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GameOver()
        {

        }

        private void OnClick()
        {
            switch (_currentClickState)
            {
                case ClickState.WaitingForPreClick:
                    _currentClickState = ClickState.WaitingForClick;
                    break;
                case ClickState.WaitingForClick:
                    _currentClickState = ClickState.WaitingForPreClick;
                    CheckClick(false);
                    break;
                case ClickState.Clicked:
                    GameOver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
