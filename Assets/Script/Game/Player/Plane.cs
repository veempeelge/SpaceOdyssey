using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plane : MonoBehaviour
{
    public static Plane _instance;
    [SerializeField] private HP hp;

    [SerializeField] GameObject bulletPrefabs;
    [SerializeField] Transform bulletSpawn;
    private Vector3 shooterPosition;
    [SerializeField] Animator animator;

    float _iframe = 20f;
    float _timer;
    float _fireRate = 10f;
    float _fireRateTimer;

    [SerializeField] Animator planeAnimator;

    [SerializeField] GameObject invisShield;
    [SerializeField] ParticleSystem particles;

    int speedMultiplier = GameManager.speedmultiplier;


    // Start is called before the first frame update
    void Start()
    {

        planeAnimator.SetTrigger("Enter");
        _timer = 2f;
        _fireRateTimer = .8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                if (_fireRate < _fireRateTimer)
                {
                    Shoot();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_fireRate < _fireRateTimer)
            {
                Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        shooterPosition = bulletSpawn.transform.position;
        _timer += 1f;
        _fireRateTimer += 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && _iframe < _timer)
        {
            hp.GotHit(1);
            Destroy(other.gameObject);
            _timer = 0;
            Sound._instance.PlayHit();
        }
    }

    void Shoot()
    {
        _fireRateTimer = 0;
        Instantiate(bulletPrefabs, shooterPosition, Quaternion.identity);
        Sound._instance.PlayShoot();
    }

    public void Invis()
    {
        invisShield.SetActive(true);
        particles.startSpeed *= speedMultiplier;
        particles.startColor = Color.cyan;
        particles.emissionRate *= speedMultiplier;
    }

    public void StopInvis()
    {
        invisShield.SetActive(false);
        particles.startSpeed *= 1/speedMultiplier;
        particles.startColor = Color.white;
        particles.emissionRate *= 1 / speedMultiplier;
    }

    public void Shield()
    {
        invisShield.SetActive(true);
    }

    public void StopShield()
    {
        invisShield.SetActive(false);
    }

    
}
