using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDestroy : MonoBehaviour
{

    [SerializeField] GameObject planeController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Destroy()
    {
        Destroy(planeController);
    }

}
