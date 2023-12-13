    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetSpawn : MonoBehaviour
{

    [SerializeField] GameObject[] planets;
    Vector3 _spawnPosition;
    float[] heightx = new float[] { 20f, -22f };
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPlanets",0f,.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlanets()
    {
        _spawnPosition = new Vector3(Random.Range(-30.1f, 30.6f), heightx[Random.Range(0, heightx.Length)], 170f);
        Instantiate(planets[Random.Range(0,planets.Length)],_spawnPosition,Quaternion.identity);
    }
}
