using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTransition : MonoBehaviour
{
    public static ParticleTransition instance;
    [SerializeField] ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    public void StartGame()
    {
        particle.startSpeed = -400.6f;
        particle.startSize = 84.6f;
        particle.emissionRate = 30;
        particle.loop = false;

    }
}
