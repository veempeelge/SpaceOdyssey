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
            Debug.Log("Particle system found.");
            // Ensure VelocityOverLifetime module is enabled
            var velocityOverLifetime = particleSystem.velocityOverLifetime;
            if (velocityOverLifetime.enabled)
            {
                Debug.Log($"Current Speed Modifier Multiplier: {velocityOverLifetime.speedModifierMultiplier}");

                // Modify the speedModifierMultiplier
                velocityOverLifetime.speedModifierMultiplier = 3f;

                // Log after changing
                Debug.Log($"New Speed Modifier Multiplier: {velocityOverLifetime.speedModifierMultiplier}");
            }
            else
            {
                Debug.LogWarning("VelocityOverLifetime module is not enabled.");
            }
        }
        else
        {
            Debug.LogWarning("Particle system reference not set.");
        }
    }
}
