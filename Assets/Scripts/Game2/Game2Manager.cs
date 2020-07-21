using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace Game2
{
    public class Game2Manager : MonoBehaviour
    {
        [SerializeField] private Image circle;

        private float _gameTime;

        private void Awake()
        {
            _gameTime = -3;
        }

        private void Awake()
        {
            _gameTime += Time.deltaTime;
            circle.fillAmount = _gameTime % 1;

            if (_gameTime < 1) return;

            
        }
    }
}
