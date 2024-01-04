using System;
using System.Collections;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using UnityEngine;
using WristSniper.Scripts;

public class AimController : MonoBehaviour
{
  
    public LeapServiceProvider leapServiceProvider;
    public float speed = 1f;

    public float rightMovementScale = 1;
    public float leftMovementScale = 1;
    public float upMovementScale = 1;
    public float downMovementScale = 1;
    void Update () {
        Frame frame = leapServiceProvider.CurrentFrame;

        if (frame.Hands.Count > 0) {
            Vector3 wristPosition = frame.Hands[0].WristPosition;
            
            
            Hand hand = frame.Hands[0];
            
            
            
            
            Vector3 palmNormal = hand.PalmNormal;
            Vector3 palmDirection = hand.Direction;
            Quaternion wristRotation = Quaternion.LookRotation(palmDirection, -palmNormal);

            
            Quaternion upperArmRotation = hand.Arm.Basis.rotation;

            
            Vector3 wristForward = wristRotation * Vector3.forward;
            Vector3 upperArmForward = upperArmRotation * Vector3.forward;

            
            if (wristForward.x > 0)
            {
                wristForward *= rightMovementScale;
            }
            else
            {
                wristForward *= leftMovementScale;
            }

            if (upperArmForward.y > 0)
            {
                upperArmForward *= upMovementScale;
            }
            else
            {
                upperArmForward *= downMovementScale; 
            }
            Vector3 movementDirection = wristForward + upperArmForward;
            movementDirection.Normalize();
            movementDirection.z = 0;
          
            transform.position += movementDirection * Time.deltaTime * speed;
            
            
            
           
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Frame frame = leapServiceProvider.CurrentFrame;

        if (frame.Hands.Count > 0)
        {

            Hand hand = frame.Hands[0];
            if (hand.GrabStrength > 0.95f)
            {
                Destroy(other.gameObject);
                WristSniperGameManager.AddPoint.Invoke();
               
            }
        }
    }


}
