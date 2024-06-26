using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HP : MonoBehaviour
{

    float _currentHP = 3;
    [SerializeField] Animator anim;

    [SerializeField] TMP_Text hpUi;
    [SerializeField] ParticleSystem HPParticle;
    Score _score;


    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
       

    }


   public void GotHit(int amount)
    {
        _currentHP -= amount;
        hpUi.SetText(_currentHP.ToString());

        if (_currentHP > 0)
        {
            anim.SetTrigger("GotHit");
        }

        if (_currentHP == 0)
        {
            Debug.Log("GameOver");
            anim.SetTrigger("PlayerDead");
            PlayerPrefs.SetInt("EndScore", PlayerPrefs.GetInt("CurrentScore"));
        }
    }

    public void GotHP(int amount)
    {
        _currentHP += amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HP")
        {
            GotHP(1);
            Debug.Log("GOTHP1");
            HPParticle.Play();
            Destroy(other);
            Sound._instance.PlayHP();
        }
    }
}
