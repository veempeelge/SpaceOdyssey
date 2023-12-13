using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPUp : MonoBehaviour
{
    [SerializeField] Collider col;
    GameObject player;

    [SerializeField] GameObject powerUp;
    float _speed = .7f;
    Vector3 _position;

    // Start is called before the first frame update
    void Start()
    {
        _position = powerUp.transform.position;
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
}
