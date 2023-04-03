using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Balloon : MonoBehaviour
{
    public static UnityEvent<Balloon> BalloonPop = new UnityEvent<Balloon> ();
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position;
            
        newPos.y += speed * Time.deltaTime;
        transform.position = newPos;
    }

    private void OnDestroy()
    {
        BalloonPop.Invoke(this);
    }

    
}
