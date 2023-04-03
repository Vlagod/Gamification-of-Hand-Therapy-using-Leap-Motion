using System;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public LeapServiceProvider leapServiceProvider;
    public float speed = 5f;
    private Hand _currentHand;
    private Arm _currentArm;
    [SerializeField] private float _previousWristRotationAngle;
    [SerializeField] private float currentWristRotationAngle;
    [SerializeField] private int _rotationCounter;
    private float _rotationThreshold = 20;
    [SerializeField] private float _cumulativeRotation;


    [SerializeField] private float rotationDifference;

    void Start()
    {
        _rotationCounter = 0;
        _cumulativeRotation = 0f;
    }

    void Update()
    {
        Frame currentFrame = leapServiceProvider.CurrentFrame;
        if (currentFrame != null && currentFrame.Hands.Count > 0)
        {
            _currentHand = currentFrame.Hands[0];
            _currentArm = _currentHand.Arm;

            if (_previousWristRotationAngle == 0)
            {
                // _previousWristRotationAngle = _currentArm.Rotation.eulerAngles.z;
                _previousWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
            }
            else
            {
                // currentWristRotationAngle = _currentArm.Rotation.eulerAngles.z;
                currentWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
                if (currentWristRotationAngle < 20 && _previousWristRotationAngle > 340)
                {
                    rotationDifference = 360 - _previousWristRotationAngle + currentWristRotationAngle;
                }else if (currentWristRotationAngle > 340 && _previousWristRotationAngle < 20)
                {
                    rotationDifference = _previousWristRotationAngle + 360 - currentWristRotationAngle;
                }
                else
                {
                    rotationDifference = currentWristRotationAngle - _previousWristRotationAngle;
                }

                _previousWristRotationAngle = currentWristRotationAngle;
            }
            
            var newPos = transform.position;
            newPos.x += 1 * speed * rotationDifference;
            transform.position = newPos;
        }

       
    }


    void HandRotation()
    {
        Frame currentFrame = leapServiceProvider.CurrentFrame;
        if (currentFrame != null && currentFrame.Hands.Count > 0)
        {
            _currentHand = currentFrame.Hands[0];
            _currentArm = _currentHand.Arm;

            if (_previousWristRotationAngle == 0)
            {
                // _previousWristRotationAngle = _currentArm.Rotation.eulerAngles.z;
                _previousWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
            }
            else
            {
                // currentWristRotationAngle = _currentArm.Rotation.eulerAngles.z;
                currentWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
                rotationDifference = currentWristRotationAngle - _previousWristRotationAngle;

                if (Mathf.Abs(rotationDifference) <= _rotationThreshold)
                {
                    if (Mathf.Sign(rotationDifference) != Mathf.Sign(_cumulativeRotation))
                    {
                        _cumulativeRotation = 0;
                    }

                    _cumulativeRotation += rotationDifference;

                    if (_cumulativeRotation >= 360f)
                    {
                        _rotationCounter++;
                        _cumulativeRotation %= 360f;
                        Debug.Log("Wrist Rotations: " + _rotationCounter);
                    }
                }

                _previousWristRotationAngle = currentWristRotationAngle;
            }

        }
    }
// [SerializeField] private Vector3 point;
    //
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawCube(point, new Vector3(1, 1, 1));
    // }

    void MoveShip()
    {
        float moveDelta = 1f;
        var newPos = transform.position;
        newPos.x += moveDelta * speed * (_cumulativeRotation / 360);
        transform.position = newPos;
    }

}

// public float rotationThreshold = 360.0f; // angle threshold for counting full turns
    // public int turnCount = 0; // counter for full turns
    // public bool clockwiseOnly = false; // if true, count only clockwise turns
    //
    // private LeapProvider _leapProvider;
    // private HandModel _handModel;
    // private Quaternion _previousWristRotation;
    //
    // [SerializeField] private float accumulatedAngle;
    //
    // void Start () {
    //     _leapProvider = FindObjectOfType<LeapProvider>() as LeapProvider;
    //     _handModel = GetComponent<HandModel>();
    //     _previousWristRotation = Quaternion.identity;
    // }
    //
    // void Update () {
    //     // get the hand from the Leap Motion device
    //     Hand hand = _leapProvider.CurrentFrame.Hands[0];
    //
    //     if (hand != null) {
    //         // get the wrist rotation
    //         Quaternion wristRotation = hand.Basis.rotation;
    //
    //         // calculate the rotation delta based on the change in wrist rotation
    //         Quaternion rotationDelta = wristRotation * Quaternion.Inverse(_previousWristRotation);
    //         rotationDelta.ToAngleAxis(out float angle, out Vector3 axis);
    //
    //         // // calculate the accumulated angle of rotation around the wrist's axis
    //         float dotProduct =  Vector3.Dot(axis, hand.Arm.Direction);
    //         if (dotProduct < 0.0f) {
    //             angle = -angle;
    //         }
    //         // if (rotationDelta. < Quaternion.identity)
    //         // {
    //         //     angle = -angle;
    //         // }
    //
    //         accumulatedAngle += angle;
    //         // accumulatedAngle = Mathf.Repeat(accumulatedAngle + angle, 360.0f);
    //
    //         // check if a full turn has been completed
    //         if (accumulatedAngle >= rotationThreshold) {
    //             turnCount++;
    //             accumulatedAngle -= rotationThreshold;
    //         }
    //
    //         _previousWristRotation = wristRotation;
    //     }
    // }


