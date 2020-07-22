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
                    Manager.ResetTo(index)
                }
            );
            image.color = colors[(int)_buttonStates[index]];
        }
    }

    public void Play(int index)
    {
        index %= buttons.Length;
        
    }
}
