using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinect : MonoBehaviour
{
    [SerializeField] GameObject kinectBody;

    Vector3 kinectBodyPosition;
    Vector3 rightposition;
    Vector3 leftposition;
    Vector3 middleposition;
    Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        kinectBodyPosition = kinectBody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = kinectBody.transform.position;
        if (kinectBodyPosition.x < currentPosition.x - 5f)
        {
            DebugTools.instance.moveRight();
            Debug.Log("MoveRight");
        }

        if (kinectBodyPosition.x > currentPosition.x + 5f)
        {
            DebugTools.instance.moveLeft();
            Debug.Log("MoveRight");
        }
    }

}
