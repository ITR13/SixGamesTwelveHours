using System.Collections;
using UnityEngine;

namespace Game3
{
    public class Game3Manager : MonoBehaviour
    {
        [SerializeField] private RpgFighter player, enemy;

        private void Start()
        {
            RpgTextScript.Instance.SetText("", () => {});
        }

        private enum FightState
        {
            Selecting,
            Attacking,
            BillyAttacks,
            Dead,
        }

        private enum SelectState
        {
            MainSelect,
            AttackSelect,
        }

        private enum AttackState
        {
            Animation,
            Health,
            Text,
        }

        private IEnumerable MainLoop()
        {
            while (true)
            {

            }
        }
    }
}
