using System;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public LeapServiceProvider leapServiceProvider;
    public float speed = 1f;
    
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
        if(ShipGameManager.Instance.isPaused) return;
        Frame currentFrame = leapServiceProvider.CurrentFrame;
        if (currentFrame != null && currentFrame.Hands.Count > 0)
        {
            _currentHand = currentFrame.Hands[0];
            _currentArm = _currentHand.Arm;

            if (_previousWristRotationAngle == 0)
            {
                _previousWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
            }
            else
            {
                currentWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
                if (currentWristRotationAngle < 60 && _previousWristRotationAngle > 300)
                {
                    rotationDifference = 360 - _previousWristRotationAngle + currentWristRotationAngle;
                }else if (currentWristRotationAngle > 300 && _previousWristRotationAngle < 60)
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
            newPos.x -= 1 * speed * rotationDifference * 1.5f;
            transform.position = newPos;
        }

       
    }

    public void OnShipCollision()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
        {
            ShipGameManager.Instance.OnShipCollision();
            other.gameObject.SetActive(false);
           
        }else if (other.CompareTag("bonus"))
        {
            ShipGameManager.Instance.AddScore();
        }
        Destroy(other.gameObject);
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
                _previousWristRotationAngle = Vector2.Angle(_currentHand.PalmPosition, _currentArm.WristPosition);
            }
            else
            {
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
                    }
                }

                _previousWristRotationAngle = currentWristRotationAngle;
            }

        }
    }

    void MoveShip()
    {
        float moveDelta = 1f;
        var newPos = transform.position;
        newPos.x += moveDelta * speed * (_cumulativeRotation / 360);
        transform.position = newPos;
    }

}


