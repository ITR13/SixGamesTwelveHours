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
            set => healthBar.fillAmount = value;
        }

        public int Energy
        {
            get => _currentEnergy;
            set
            {
                if (value < 0)
                {
                    _currentEnergy = 0;
                }else if (value >= totalEnergy)
                {
                    _currentEnergy = totalEnergy;
                }
            }
        }
    }
}
