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
            0.9622642f,
            0.7592501f,
            0
        );

        public static SavePoint CurrentSavePoint { get; private set; }

        [SerializeField] private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer.color = OffColor;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (CurrentSavePoint != null)
            {
                CurrentSavePoint.spriteRenderer.color = OffColor;
            }
            spriteRenderer.color = OnColor;
            CurrentSavePoint = this;
        }
    }
}
