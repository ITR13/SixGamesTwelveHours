using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game3
{
    public class RpgFighter : MonoBehaviour
    {
        [SerializeField] private Image sprite;
        [SerializeField] private Image healthBar;
        [SerializeField] private Image energyBar;

        [SerializeField] private int totalEnergy;
        private int _currentEnergy;

        public float Health
        {
            get => healthBar.fillAmount;
            set
            {
                Debug.Log($"Health: {value}");
                healthBar.fillAmount = value;
            }
        }

        public int Energy
        {
            get => _currentEnergy;
            set
            {
                Debug.Log($"Energy: {value}");
                if (value < 0)
                {
                    _currentEnergy = 0;
                }
                else if (value >= totalEnergy)
                {
                    _currentEnergy = totalEnergy;
                }
                else
                {
                    _currentEnergy = value;
                }

                energyBar.fillAmount = _currentEnergy / (float) totalEnergy;
            }
        }

        public float Size
        {
            get => sprite.transform.localScale.x;
            set => sprite.transform.localScale = Vector3.one * value;
        }
    }
}
