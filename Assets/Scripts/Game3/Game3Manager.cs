using System;
using System.Collections;
using UnityEngine;

namespace Game3
{
    public class Game3Manager : MonoBehaviour
    {
        private enum Attack
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

        [SerializeField] private RpgFighter player, enemy;

        private float billyAttack;
        private int bananas;

        private Attack _selectedAttack;

        private void Awake()
        {
            billyAttack = 0.02f;
            bananas = 0;
        }

        private void Start()
        {
            RpgTextScript.Instance.SetText("", () => {});
        }

        private IEnumerable IntroOutro()
        {
            // Intro
            var ack = false;
            RpgTextScript.Instance.SetText(
                "A wild billy approaches",
                () => ack = true
            );
            while (!ack) yield return null;

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
            _selectedAttack = Attack.None;
            RpgTextScript.Instance.SetButtons(
                new []{"Attack", "Charge", "Defend", "Run"},
                new Action[]
                {
                    () => _selectedAttack = Attack.Attack,
                    () => _selectedAttack = Attack.Charge,
                    () => _selectedAttack = Attack.Defend,
                    () => _selectedAttack = Attack.RunAway
                },
                null
            );
            while (_selectedAttack == Attack.None) yield return null;

            if (_selectedAttack != Attack.Attack) yield break;

            RpgTextScript.Instance.SetButtons(
                new[] { "Milk [0]", "Pretty Good [3]", "Banana [4]", "Vibe [10]" },
                new Action[]
                {
                    () => _selectedAttack = Attack.Attack,
                    () => _selectedAttack = Attack.Charge,
                    () => _selectedAttack = Attack.Defend,
                    () => _selectedAttack = Attack.RunAway
                },
                null
            );
            while (_selectedAttack == Attack.Attack) yield return null;
        }

        private IEnumerable ExecuteAttack()
        {
            switch (_selectedAttack)
            {
                case Attack.Milk:
                    yield return MilkAnim();
                    billyAttack *= 1.75f;
                    break;
                case Attack.PrettyGood:
                    yield return PrettyGoodAnim();
                    player.Health += 0.3f;
                    break;
                case Attack.Banana:
                    yield return BananaAnim();
                    bananas++;
                    break;
                case Attack.KarlsonVibe:
                    yield return KarlsonVibeAnim();
                    enemy.Health -= 0.2f;
                    break;
                case Attack.Charge:
                    yield return ChargeAnim();
                    break;
                case Attack.Defend:
                    yield return DefendAnim();
                    break;
                case Attack.RunAway:
                    yield return RunAwayAnim();
                    break;
                case Attack.None:
                case Attack.Attack:
                default:
                    break;
            }
            yield break;
        }
    }
}
