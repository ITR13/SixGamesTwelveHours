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

        private enum Attacks
        {
            Milk, 
            PrettyGood,
            Banana,
            KarlsonVibe,
            Charge,
            Defend,
            RunAway,
        }

        private IEnumerable WaitFor(out Action action)
        {
            var waiting = true;
            action = () => waiting = false;
            while (waiting) yield return null;
        }

        private IEnumerable IntroOutro()
        {
            // Intro
            RpgTextScript.Instance.SetText("A wild billy approaches");
            yield return MainLoop();
            // Outro
            // Menu
        }

        private IEnumerable MainLoop()
        {
            var state = FightState.Selecting;
            while (true)
            {
                yield return SelectAttack(out var attack);
                yield return ExecuteAttack(attack);
                if (enemy.Health <= 0)
                {
                    Debug.Log("You won!");
                    break;
                }
                yield return BillyAttack();

                if (player.Health <= 0)
                {
                    Debug.Log("You lost!");
                    break;
                }
            }
        }

        private IEnumerable SelectAttack(out Attacks attack)
        {
            yield return null;
        }
    }
}
