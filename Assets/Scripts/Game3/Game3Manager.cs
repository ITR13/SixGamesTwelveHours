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
            None,
            Attack,
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
            while (true)
            {
                yield return SelectAttack();
                yield return ExecuteAttack();
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

        private IEnumerable SelectAttack()
        {
            var selectedAttack = Attacks.None;
            RpgTextScript.Instance.SetButtons(
                new []{"Attack", "Charge", "Defend", "Run"},
                new Action[]
                {
                    () => selectedAttack = Attacks.Attack,
                    () => selectedAttack = Attacks.Charge,
                    () => selectedAttack = Attacks.Defend,
                    () => selectedAttack = Attacks.RunAway
                },
                null
            );
            while (selectedAttack == Attacks.None) yield return null;

            if (selectedAttack != Attacks.Attack) yield break;

            RpgTextScript.Instance.SetButtons(
                new[] { "Attack", "Charge", "Defend", "Run" },
                new Action[]
                {
                    () => selectedAttack = Attacks.Attack,
                    () => selectedAttack = Attacks.Charge,
                    () => selectedAttack = Attacks.Defend,
                    () => selectedAttack = Attacks.RunAway
                },
                null
            );
            while (selectedAttack == Attacks.Attack) yield return null;

        }
    }
}
