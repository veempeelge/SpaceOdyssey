using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] Button restart;
    [SerializeField] Button mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(Retry);
        mainMenu.onClick.AddListener(BackToMainMenu);

    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Retry()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
