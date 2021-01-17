using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject _overlay = null;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    private void OnStartGame()
    {
        _overlay.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
