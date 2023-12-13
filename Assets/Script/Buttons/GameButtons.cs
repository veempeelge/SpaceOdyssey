using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameButtons : MonoBehaviour
{
    [SerializeField] Button pauseButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button resumeButton;
   [SerializeField] GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(PauseButton);
        quitButton.onClick.AddListener(QuitButton);
        resumeButton.onClick.AddListener(ResumeButton);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PauseButton()
    {
        Debug.Log("Pause");
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    void QuitButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

    }

    void ResumeButton()
    {
        // resume game
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
