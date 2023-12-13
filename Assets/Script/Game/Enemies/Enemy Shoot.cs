using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] GameObject enemybullet;
    Vector3 _position;
    float speed = 2f;


    // Start is called before the first frame update
    void Start()
    {

        _position = enemybullet.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_position.z <= -3.1f)
        {
            Destroy(enemybullet);
        }
    }

    private void FixedUpdate()
    {

        _position.z -= speed;
        enemybullet.transform.position = _position;

    }

   
}
