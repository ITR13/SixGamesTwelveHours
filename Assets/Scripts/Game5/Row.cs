using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Row : MonoBehaviour
{
    private AudioSource audioSource;
    private Button[] buttons;


    private void PlayBeep()
    {
        var length = Mathf.FloorToInt(Mathf.Lerp(0.3f, 0.75f, TotalError) * Bps);
        var frequency = 440 * Mathf.Pow(2, -TotalError * TotalError * 4f);
        var clip = AudioClip.Create("beep", length, 1, Bps, false);
        clip.SetData(
            GenerateAudio(
                length,
                Bps / 5,
                frequency
            ),
            0
        );
        audioSource.PlayOneShot(clip, 0.35f);
    }

    private float[] GenerateAudio(int totalSize, int falloffSize, float hz)
    {
        var preFalloff = totalSize - falloffSize;

        var data = new float[totalSize];

        var frequency = hz / Bps;

        for (var i = 0; i < preFalloff; i++)
        {
            var t = 2f * i * Mathf.PI * frequency;
            var h = Mathf.Sin(t);
            data[i] = h;
        }

        for (var i = 0; i < falloffSize; i++)
        {
            var t = 2f * (i + preFalloff) * Mathf.PI * frequency;
            var h = Mathf.Sin(t);
            var scale = 1 - i / (float)falloffSize;
            data[i + preFalloff] = h * scale;
        }

        return data;
    }
}
