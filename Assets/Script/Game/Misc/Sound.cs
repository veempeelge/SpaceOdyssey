using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound _instance;
    [SerializeField] AudioClip shoot, destroyed, enemyshoot, move, hit, shield, boost, hp;
    [SerializeField] AudioSource soundFx;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public void PlayShoot()
    {
     soundFx.PlayOneShot(shoot);   
    }

    public void PlayDestroyed()
    {
        soundFx.PlayOneShot(destroyed);
    }
    public void PlayEnemyShoot()
    {
        soundFx.PlayOneShot(enemyshoot);
        
    }

    public void PlayMove()
    {
        soundFx.PlayOneShot(move);

    }
    public void PlayHit()
    {
        soundFx.PlayOneShot(hit);
    }

    public void PlayBoost()
    {
        soundFx.PlayOneShot(boost);
    }

    public void PlayShield()
    {
        soundFx.PlayOneShot(shield);

    }

    public void PlayHP()
    {
        soundFx.PlayOneShot(hp);
    }
}
