using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button plusPlaneButton;
    [SerializeField] GameObject plusPlanePanel;
     int planesCount;
    // [SerializeField] Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure TimeManager instance is not null
        if (TimeManager.instance == null)
        {
            Debug.LogError("TimeManager instance is null");
            return;
        }

        planesCount = TimeManager.instance.energy;
      // Debug.Log(planesCount);

        if (startButton != null)
        {
            startButton.onClick.AddListener(StartGameButton);
        }
        else
        {
            Debug.LogError("startButton is not assigned");
        }
        // if (mainMenu != null)
        // {
        //     mainMenu.onClick.AddListener(MainMenu);
        // }
        // else
        // {
        //     Debug.LogError("mainMenu button is not assigned");
        // }
        plusPlaneButton.onClick.AddListener(PlusPlaneButton);
        plusPlanePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StartGameButton()
    {
            //Debug.Log("Start" + planesCount);
            //SceneManager.LoadScene(1);
            TimeManager.instance.SpendEnergy();
    }

    void PlusPlaneButton()
    {
        plusPlanePanel.SetActive(true);
    }


    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
