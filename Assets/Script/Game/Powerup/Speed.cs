using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] Collider col;
    GameObject player;
    private Collider playerCollider;

    [SerializeField] GameObject powerUp;
    float _speed = .7f;


    Vector3 _position;
    private float _powerUpDuration = 5f;

    GameManager GameManager;

    Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerCollider = player.GetComponent<Collider>();
        _position = powerUp.transform.position;
        plane = player.GetComponent<Plane>();
    }   

    // Update is called once per frame
    void Update()
    {
       
        powerUp.transform.position = _position;

    }

    private void FixedUpdate()
    {
        _position.z -= _speed;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" )
        {
           
            Debug.Log("hitSpeed2");
            playerCollider.enabled = false;
            GameManager.speedmultiplier = 3;
            plane.Invis();
            Invoke(nameof(StopPowerUP), _powerUpDuration);
            Sound._instance.PlayBoost();
        }
    }

    void StopPowerUP()
    {
        playerCollider.enabled = true;
        plane.StopInvis();
        GameManager.speedmultiplier = 1;
        Destroy(gameObject);

    }
}
