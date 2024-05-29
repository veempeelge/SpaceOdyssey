using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button restart;
   // [SerializeField] Button mainMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGameButton);
        restart.onClick.AddListener(RestartGame);
       // mainMenu.onClick.AddListener(MainMenu);

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void StartGameButton()
    {
        Debug.Log("Start");
        SceneManager.LoadScene(1);
       
    }

    void  RestartGame()
    {
        SceneManager.LoadScene(1);
        
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
