using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    Vector3 position;
    float speed = 1f;
    private Score _score;


    // Start is called before the first frame update
    void Start()
    {
        _score = FindObjectOfType<Score>();
        position = bullet.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (position.z >= 70f)
        {
            Destroy(bullet);
        }
    }

    private void FixedUpdate()
    {

        position.z += speed;
        bullet.transform.position = position;

    }
        private void OnCollisionEnter(Collision collision)
    {
      //  if (collision.gameObject.tag == "Enemy")
        {
      
            Destroy(collision.gameObject);
            Destroy(this);
            _score.AddScore(100);
           // Debug.Log("Destroy");
        }
    }
}