using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{

    public static Transition Instance;
    [SerializeField] Animator animator;
    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        Invoke(nameof(DisableGameObject), .4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableGameObject()
    {
        gameObject.SetActive(true);
    }

    public void EndAnimation()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("EndScene");
    }
}
