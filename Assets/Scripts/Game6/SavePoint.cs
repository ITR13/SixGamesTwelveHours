using System.Linq.Expressions;
using UnityEngine;

namespace Game6
{
    public class SavePoint : MonoBehaviour
    {
        private static readonly Color OffColor = new Color(
            0,
            0.7592501f,
            0.9622642f
        );
        private static readonly Color OnColor = new Color(
            0,
            0.7592501f,
            0.9622642f
        );


        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {

        }
    }
}
