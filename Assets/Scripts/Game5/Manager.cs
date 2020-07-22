using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game5
{
    public class Manager : MonoBehaviour
    {
        public const int BitsPerSecond = 44100;
        public const float BeatsPerSecond = 3;

        public static readonly int ClipLength = Mathf.FloorToInt(
            44100 * 0.95f / BeatsPerSecond
        );

        public static readonly int FalloffLength = Mathf.FloorToInt(
            44100 * 0.1f / BeatsPerSecond
        );

        [SerializeField] private Row rowPrefab;
        [SerializeField] private List<Tuple<string, float>> notes;
        
        private Row[] _rows;

        private float _time;
        private int _beat;

        private void Awake()
        {
            _rows = new Row[notes.Count];
        }

        private void Update()
        {
            _time += Time.deltaTime / BeatsPerSecond;
            if(_time < _beat) return;
            foreach (var row in rows)
            {
                row.Play(_beat);
            }

            _beat++;
        }

        private void Reset()
        {
            var parent = transform.GetChild(0);

            while (parent.childCount > 0)
            {

            }


            for (var i = 0; i < _rows.Length; i++)
            {
                _rows[i] = Instantiate(rowPrefab, parent);
                _rows[i].Set(notes[i].Item1, notes[i].Item2);
            }
        }
    }
}
