using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilSceneHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnDrawGizmos()
    {
        float axisLength = 1.0f; // Длина каждой оси
    
        // X-ось (Красная)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * axisLength);
    
        // Y-ось (Зеленая)
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * axisLength);
    
        // Z-ось (Синяя)
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * axisLength);
    }

}
