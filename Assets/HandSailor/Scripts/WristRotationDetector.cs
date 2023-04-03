using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class WristRotationDetector : MonoBehaviour
{
    public LeapServiceProvider leapServiceProvider;
    // public HandType handType = HandType.Left;
    private Frame currentFrame;
    private Frame previousFrame;

    [SerializeField] private float threshold = 0.05f;
    private Quaternion previousRotation;

    void Update()
    {
        if (leapServiceProvider == null)
        {
            leapServiceProvider = FindObjectOfType<LeapServiceProvider>();
            if (leapServiceProvider == null)
            {
                Debug.LogError("LeapServiceProvider not found in the scene.");
                return;
            }
        }

        previousFrame = currentFrame;
        currentFrame = leapServiceProvider.CurrentFrame;

        if (currentFrame != null)
        {
            Hand hand = currentFrame.Hands[0];

            if (hand == null)
            {
                Debug.Log("Hand not found.");
                return;
            }

            Arm arm = hand.Arm;
            Quaternion currentRotation = arm.Basis.rotation;

            if (previousFrame == null)
            {
                previousRotation = currentRotation;
                return;
            }

            Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);
            float rotationAngle;
            Vector3 rotationAxis;
            deltaRotation.ToAngleAxis(out rotationAngle, out rotationAxis);

            // Store current hand rotation for next frame comparison
            previousRotation = currentRotation;

            // Detect wrist rotation
            Debug.Log("Wrist rotation angle: " + rotationAngle + ", Rotation axis: " + rotationAxis);

            // Determine direction of rotation
            if(deltaRotation.y < threshold) return;
            if (rotationAxis.y < 0 )
            {
                Debug.Log("Clockwise rotation");
            }
            else
            {
                Debug.Log("Counter-clockwise rotation");
            }
        }
    }
}
