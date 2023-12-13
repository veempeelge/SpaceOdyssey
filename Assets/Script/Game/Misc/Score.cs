using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    [SerializeField] private TMP_Text score;
    [SerializeField] private TextMeshProUGUI highScore;
    GameManager GameManager;

    private int _currentScore;

    public int CurrentScore => _currentScore;

    // Start is called before the first frame update
    void Start()
    {
        HighScoreSet();
    }

    // Update is called once per frame
    void Update()
    {
       score.SetText(_currentScore.ToString("000000"));
        HighScore();
    }

    private void FixedUpdate()
    {
        var speedMultiplier = GameManager.speedmultiplier;
        AddScore(1*speedMultiplier);
    }

    public void AddScore(int amount)
    {
        _currentScore += amount;
       
    }

    void HighScore()
    {
        if (_currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            HighScoreSet();
        }
       
    }

    void HighScoreSet()
    {
        highScore.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
