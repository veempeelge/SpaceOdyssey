using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefabs;
    Vector3 position;
    float _speed = .7f;
    Score _score;
    [SerializeField] Collider collider;


    [SerializeField] private GameObject enemyBulletPrefabs;
    [SerializeField] Transform enemyBulletSpawn;
    [SerializeField] ParticleSystem explosion;
    private Vector3 _enemyPosition;

    [SerializeField] Animator enemyAnimator;

    [SerializeField] GameObject PlaneEnemyNoShader;

    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("EnemyShoot", 1f, 1.5f);
        position = enemyPrefabs.transform.position;
        _score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        _enemyPosition = enemyBulletSpawn.transform.position;
        if (position.z <= -3.1f)
        {
            Destroy(enemyPrefabs);
        }
    }

    private void FixedUpdate()
    {
        var speedMultiplier = GameManager.speedmultiplier;
        position.z -= _speed*speedMultiplier;
        enemyPrefabs.transform.position = position;

    }

    void EnemyShoot()
    {
        Sound._instance.PlayEnemyShoot();
        Instantiate(enemyBulletPrefabs, _enemyPosition, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" || other.tag == "Shield")
        {
            CancelInvoke(nameof(EnemyShoot));
            _speed = 0f;
            enemyAnimator.SetTrigger("Dead");
            explosion.Play();
            //Destroy(other.gameObject);
            _score.AddScore(100);
            Sound._instance.PlayDestroyed();
           // Debug.Log("Hit");
            collider.enabled = false;

            if (other.tag == "Bullet")
                Destroy(other.gameObject);
        }
    }
}
