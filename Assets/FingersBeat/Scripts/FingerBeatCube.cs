using System.Collections;
using System.Collections.Generic;
using Leap.Unity.Interaction;
using UnityEngine;

public class FingerBeatCube : MonoBehaviour
{
    public float speed = 10f;  // The speed at which the object moves

    private Rigidbody rigidbody;  // The Rigidbody component of the object

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move the object along the x-axis
        
        Vector3 movement = new Vector3(0f, 0f, speed * Time.fixedDeltaTime * -1);
        transform.position += transform.forward * speed * Time.fixedDeltaTime * -1;
        // Apply the movement to the object's Rigidbody
        rigidbody.MovePosition(transform.position + movement);
    }

    // void OnCollisionStay(Collision collision)
    // {
    //     // Get the normal vector of the collision contact
    //     Vector3 contactNormal = collision.GetContact(0).normal;
    //
    //     // Calculate the rotation needed to align the object's up vector with the contact normal
    //     Quaternion rotation = Quaternion.FromToRotation(transform.up, contactNormal) * transform.rotation;
    //
    //     // Apply the rotation to the object's Rigidbody
    //     rigidbody.MoveRotation(rotation);
    // }
    //
    
}
