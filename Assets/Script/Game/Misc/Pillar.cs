using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pillar : MonoBehaviour
{
    [SerializeField] GameObject pillar;
    Vector3 position;
    float _speed = .7f;

    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        position = pillar.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       if (position.z <= -35f)
        {
            Destroy(pillar);
        }
    }

    private void FixedUpdate()
    {
        var speedMultiplier = GameManager.speedmultiplier;
        position.z -= _speed * speedMultiplier;
        pillar.transform.position = position;

    }

    
}
