using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using Unity.XR.CoreUtils;
using System;

public class DebugTools : MonoBehaviour
{
    public static DebugTools instance;
    ARFaceManager faceManager;
    GameObject _faceObject;
    [SerializeField] GameObject faceWarning;
   
        
    [SerializeField] private TMP_Text positionDebug;
    [SerializeField] private TMP_Text rotationXDebug;
    [SerializeField] private TMP_Text tiltDebug;
    [SerializeField] Animator animator;

    Vector3 _facePosition;
    Vector3 _rotation;
    float _faceRotationZ;

    Vector3 _currentPosition;
    Vector3 _targetPosition;
    bool _isMoving;

    private Rigidbody _faceRigidBody;

    [SerializeField] GameObject cube;

    float _timeLimit = 20f;
    float _currentTime;
    // Start is called before the first frame update
    void Start()
    {
      _currentTime = _timeLimit;
      faceManager = GetComponent<ARFaceManager>();
      _targetPosition = cube.transform.position;
      faceManager.facesChanged += FaceManager_facesChanged;
    }

    private void FaceManager_facesChanged(ARFacesChangedEventArgs obj)
    {
        if (obj.updated.Count > 0)
        {
            _faceObject = obj.added[0].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _faceObject = GameObject.FindGameObjectWithTag("Face");
        tiltDetect();
        usingMouse();
        cube.transform.position = Vector3.Lerp(cube.transform.position, _targetPosition, 5f*Time.deltaTime);
        _isMoving = (cube.transform.position - _targetPosition).sqrMagnitude > 0.1f;

        
    }

    private void FixedUpdate()
    {
        _currentTime += 1f;
    }

    void tiltDetect()
    {
        if (_faceObject == null) return;

        _faceRigidBody = _faceObject.GetComponent<Rigidbody>();

        _facePosition = _faceRigidBody.transform.position;
        positionDebug.SetText(_facePosition.ToString());

        _faceRotationZ = _faceRigidBody.transform.rotation.z;
        rotationXDebug.SetText(_faceRotationZ.ToString());

        if (_faceRotationZ > 0.1)
        {
           tiltDebug.SetText("Tilting Right");
           moveRight();
        }
        else if (_faceRotationZ < -0.1 )
        {
           tiltDebug.SetText("Tilting Left");
           moveLeft();
        }
    }

    void usingMouse()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
    }

   public void moveRight()
    {
        if (_currentTime < _timeLimit) return;
        if (_isMoving) return;

        var currentTarget = 2f;
      

        _targetPosition.x += 2f;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -2f, 2f);
        _currentTime = 0;

        if (cube.transform.position.x < 1f)
        animator.SetTrigger("TurnRight");

        Sound._instance.PlayMove();
    }

   public void moveLeft()
    {
        if (_currentTime < _timeLimit) return;
        if (_isMoving) return;
        var currentTarget = -2f;
      

        _targetPosition.x -= 2f;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -2f, 2f);
        _currentTime = 1;

        if (cube.transform.position.x > -1f)
        animator.SetTrigger("TurnLeft");

        Sound._instance.PlayMove();

    }
}
