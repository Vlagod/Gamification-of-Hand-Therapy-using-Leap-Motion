using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Leap;
using Leap.Unity;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlaneController : MonoBehaviour
{
    
    public float speed = 9f;
    public static PlaneController Instance;
    private Vector3 palmStartPos;
    private Vector3 startRotation;

    public UnityEvent onPlaneCollision = new UnityEvent();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        startRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        PlaneTilt();
        PlaneMovement();
    }
    public LeapServiceProvider leapServiceProvider;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Entered trigger with " + other.gameObject.name);
        onPlaneCollision.Invoke();
        GetComponent<Animator>().SetTrigger("Collision");
    }

    public void PlaneTilt()
    {
        Frame frame = leapServiceProvider.CurrentFrame;
        
        foreach (Hand hand in frame.Hands)
        {
            if (hand.IsRight)
            {
                Vector3 normal = hand.PalmNormal;
                if (palmStartPos == Vector3.zero)
                {
                    palmStartPos = normal;
                }

                Vector3 projectedNormal = Vector3.ProjectOnPlane(normal, Vector3.up);
                float yaw = -Vector3.SignedAngle(Vector3.forward, projectedNormal, Vector3.up);
                float rotateSpeed = 1f;
                

                var rotateAngleY = (new Vector3(0, yaw, 0) * Time.deltaTime * rotateSpeed).y;
                var currentLocalEulerY = transform.localEulerAngles.y;

                
                
                //Calculating these values once to avoid repetition in the conditions
                float angleDifference = Mathf.Abs(currentLocalEulerY - yaw);

                if (rotateAngleY > 0 && angleDifference < 20) // right rotation boundary
                {
                    Debug.Log("Rotate right Limit Reached");
                }
                else if (rotateAngleY < 0 && angleDifference > 280) // left rotation boundary
                {
                    Debug.Log("Rotate left Limit Reached");
                }
                else
                {
                    RotatePlane(new Vector3(0, rotateAngleY, 0)); // Rotate the object to follow the palm's yaw
                }
            }

        }  
    }

    void RotatePlane(Vector3 rotateAngle)
    {
        transform.Rotate(rotateAngle, Space.Self); 
    }
    void PlaneMovement()
    {
        if (transform.rotation.eulerAngles.y > startRotation.y)
        {
            if (transform.position.x < -90)
            {
                return;
            }
            
            var shiftX = transform.position.x - ((transform.rotation.eulerAngles.y - startRotation.y) ) * Time.deltaTime * speed;
            // Debug.Log(transform.rotation.eulerAngles.y - startRotation.y);
            // Debug.Log("shiftX: " + shiftX);
            transform.position = new Vector3(shiftX, transform.position.y , transform.position.z);
        }else if(transform.rotation.eulerAngles.y < startRotation.y)

        {
            if (transform.position.x > 80)
            {
                return;
            }
            var shiftX = transform.position.x + ((startRotation.y - transform.rotation.eulerAngles.y)) * Time.deltaTime * speed;
            // Debug.Log(transform.rotation.eulerAngles.y - startRotation.y);
            // Debug.Log("shiftX: " + shiftX);
            transform.position = new Vector3(shiftX, transform.position.y , transform.position.z); 
        }
            
            
        // transform.position = pos;
    }
}
