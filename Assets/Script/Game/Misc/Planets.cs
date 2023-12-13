using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    Vector3 _currentPosition;
    [SerializeField] GameObject planet;
    float _planetSpeed = 2f;

    GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = planet.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPosition.z <= -3.1f)
        {
            Destroy(planet);
        }
    }
    private void FixedUpdate()
    {
        var speedMultiplier = GameManager.speedmultiplier;
        _currentPosition.z -= _planetSpeed * speedMultiplier;
        planet.transform.position = _currentPosition;
    }
}
