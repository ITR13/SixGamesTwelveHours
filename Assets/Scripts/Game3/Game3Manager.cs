using System;
using System.Collections;
using UnityEditor;
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
        private int bananas, prettyGoods;

        private Attack _selectedAttack;

        private void Awake()
        {
            billyAttack = 0.02f;
            bananas = 0;
            prettyGoods = 1;
        }

        private void Start()
        {
            RpgTextScript.Instance.SetText("", () => {});
            StartCoroutine(IntroOutro());
        }

        private IEnumerator IntroOutro()
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

        private IEnumerator MainLoop()
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

        private IEnumerator SelectAttack()
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

        private IEnumerator ExecuteAttack()
        {
            switch (_selectedAttack)
            {
                case Attack.Milk:
                    yield return MilkAnim();
                    billyAttack *= 1.75f;
                    break;
                case Attack.PrettyGood:
                    if(player.Energy < 3)
                    yield return PrettyGoodAnim();
                    player.Health += 0.1f * prettyGoods;
                    prettyGoods++;
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
                    player.Energy += 1 + bananas;
                    break;
                case Attack.Defend:
                    yield return DefendAnim();
                    billyAttack *= 0.4f;
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

        private IEnumerator RunAwayAnim()
        {
            yield break;
        }

        private IEnumerator DefendAnim()
        {
            yield break;
        }

        private IEnumerator ChargeAnim()
        {
            yield break;
        }

        private IEnumerator KarlsonVibeAnim()
        {
            yield break;
        }

        private IEnumerator BananaAnim()
        {
            yield break;
        }

        private IEnumerator PrettyGoodAnim()
        {
            yield break;
        }

        private IEnumerator MilkAnim()
        {
            yield break;
        }

        private IEnumerator BillyAttack()
        {
            player.Health -= billyAttack;
            var ack = false;
            RpgTextScript.Instance.SetText("Billy bumped into you", () => ack = true);
            while (!ack) yield return null;
            yield break;
        }
    }
}
