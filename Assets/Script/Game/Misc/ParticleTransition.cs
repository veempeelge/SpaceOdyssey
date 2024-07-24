using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTransition : MonoBehaviour
{
    public static ParticleTransition instance;

    // Reference to the ParticleSystem component
    private ParticleSystem particleSystem;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    
        particleSystem = GetComponent<ParticleSystem>();

       
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {

    }

   
    public void StartGame()
    {
       
        if (particleSystem != null)
        {
            particleSystem.startSpeed = 538.13f;
            particleSystem.startSize = 4.9f;
            particleSystem.startLifetime = 6.58f;
            particleSystem.emissionRate = 10f;
        }
        else
        {
            Debug.LogWarning("Particle system reference not set.");
        }
    }
}
