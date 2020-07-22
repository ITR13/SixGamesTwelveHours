using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Game5;
using UnityEngine;
using UnityEngine.UI;

public class Playbar : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Image[] buttonImages;

    private int _prevPlayed = 0;

    private void Start()
    {
        for (var i = 0; i < buttons.Length; i++)
        {
            var index = i;
            var button = buttons[i];
            var image = buttonImages[i];

            button.onClick.AddListener(
                () =>
                {
                    Manager.ResetTo(index);
                    buttonImages[_prevPlayed].color = new Color(0.25f, 025f, 0.25f, 1);
                }
            );
            image.color = new Color(0.25f, 025f, 0.25f, 1);
        }
    }

    public void Play(int index)
    {
        index %= buttons.Length;
        buttonImages[_prevPlayed].color = new Color(0.25f, 025f, 0.25f, 1);
        buttonImages[index].color = Color.yellow;
        _prevPlayed = index;
    }
}
