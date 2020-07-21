using System;
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

        public float Energy
        {
            set => energyBar.fillAmount = 
        }
    }
}
