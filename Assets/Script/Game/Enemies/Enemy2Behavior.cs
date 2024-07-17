using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behavior : MonoBehaviour
{


    [SerializeField] GameObject enemyPrefabs;
    [SerializeField] GameObject controllerEnemy2;
    [SerializeField] Collider collider;
    Vector3 _controllerPosition;
    Vector3 _position;
    float _speed = .7f;
    Score _score;
    private Vector3 _enemyPosition;
    [SerializeField] ParticleSystem explosion;
    private float[] _positionX = new float[] { -2f, 2f };
    [SerializeField] Animator animator;
    float targetX;

    GameManager GameManager;
    void Start()
    {
        _position = enemyPrefabs.transform.position;
        _controllerPosition = controllerEnemy2.transform.position;
        InvokeRepeating("move", 1.3f, 5f);
        _score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (_position.z <= -3.1f)
        {
            Destroy(enemyPrefabs);
        }
    }

    private void FixedUpdate()
    {
        var speedMultiplier = GameManager.speedmultiplier;
        _position.z -= _speed * speedMultiplier;
        enemyPrefabs.transform.position = _position;
        _position.x = Mathf.Lerp(_position.x, targetX, 5f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Shield")
        {
            _speed = 0f;
           // Destroy(other.gameObject);
            _score.AddScore(100);
            explosion.Play();
           // Debug.Log("Hit2");
            animator.SetTrigger("dead");
            Sound._instance.PlayDestroyed();
            collider.enabled = false;

            if (other.gameObject.tag == "Bullet")
                Destroy(other.gameObject);

        }
    }

    void move()
    {
       //Debug.Log(_position.x);
        if (_position.x < -1 || _position.x > 1)
        {
            targetX = 0;
        }
        else if (_position.x > -1 && _position.x < 1)
        {
            targetX = _positionX[Random.Range(0, _positionX.Length)];
        }
      
    }
}
