using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubMenu : MonoBehaviour
{
    [SerializeField] private Button Retry;
    [SerializeField] private Button MainMenu;

    private void Awake()
    {
        Retry.onClick.AddListener(
            () => SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            )
        );
        MainMenu.onClick.AddListener();
    }
}
