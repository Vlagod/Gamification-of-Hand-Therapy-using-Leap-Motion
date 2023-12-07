using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public List<GameObject> objectsToMove;
    public float speed = 9f;
    public float posToSwapObjects = -425f;
    public float posToResetObjects = 600f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveClouds();
    }

    void MoveClouds()
    {
        foreach (var obj in objectsToMove)
        {
            obj.transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * speed;
            if (obj.transform.position.z < posToSwapObjects)
            {
                obj.transform.position = Vector3.forward * posToResetObjects;
            }
        }
        
    }
}
