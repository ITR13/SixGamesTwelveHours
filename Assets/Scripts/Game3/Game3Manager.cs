using System;
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
            Victory,
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
            var state = FightState.Selecting;
            while (true)
            {
                switch (state)
                {
                    case FightState.Selecting:
                        break;
                    case FightState.Attacking:
                        break;
                    case FightState.BillyAttacks:
                        break;
                    case FightState.Victory:
                        Debug.Log("You won!");
                        break;
                    case FightState.Dead:
                        Debug.Log("You lost!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
