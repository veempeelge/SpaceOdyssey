using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject pillar;
    float _startDelay = 2f;
    float _repeatRate = 5f;
    float _enemySpawnRate = 6f;
    int _randomPillars;
    private Vector3 _spawnPos;
    private Vector3 _enemySpawnPos;
    private Vector3 _powerUpSpawnPost;
    private float[] xPoses = new float[] { -2f, 0f, 2f };
    private float[] xPosesEnemy = new float[] { -2f, 0f, 2f };
    private float[] xPosesPowerup = new float[] { -2f, 0f, 2f };



    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] GameObject[] powerUps;

    Score _score;

    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 6f, _enemySpawnRate);
        InvokeRepeating(nameof(SpawnPillars), _startDelay, _repeatRate);
        InvokeRepeating(nameof(SpeedPowerUp), 0f, 20f);
        _spawnPos = transform.position; 
        _score = FindObjectOfType<Score>();

    }

    private void Update()
    {
       
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_score.CurrentScore % 500 == 0)
        {
            Debug.Log("Lv = " + _repeatRate);
            if (_enemySpawnRate > 0)
            {
                _enemySpawnRate -= .1f;
                CancelInvoke(nameof(SpawnEnemy));
                InvokeRepeating(nameof(SpawnEnemy), 2f, _enemySpawnRate);
            }
            else return;


            if (_repeatRate > 0)
            {
                _repeatRate -= .1f;
                CancelInvoke(nameof(SpawnPillars));
                InvokeRepeating(nameof(SpawnPillars), 2f, _repeatRate);
            }
            else return;
        }

        //if (_score._currentScore >= 1000)
        //{
        //    Debug.Log("Lv2");
        //    _repeatRate = 6;
        //}
    }

   void SpawnPillars()
    {
      _spawnPos = new Vector3(xPoses[Random.Range(0,xPoses.Length)], 0f, 120f);
      Instantiate(pillar,_spawnPos,Quaternion.identity);
    }

    void SpawnEnemy()
    {
     _enemySpawnPos = new Vector3(xPosesEnemy[Random.Range(0, xPosesEnemy.Length)], -1f, 120f);
     Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], _enemySpawnPos, Quaternion.identity);
    } 

    void SpeedPowerUp()
    {
        _powerUpSpawnPost = new Vector3(xPosesPowerup[Random.Range(0, xPosesPowerup.Length)], -1f, 120f);
        Instantiate(powerUps[Random.Range(0, powerUps.Length)], _powerUpSpawnPost, Quaternion.identity);
    }


}
