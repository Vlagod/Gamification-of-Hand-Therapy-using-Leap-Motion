using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectBehaviour : MonoBehaviour
{
    public float speed = 27f;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Animator>(out var animator))
        {
            animator.SetBool("flying", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0, 0, 1) * Time.deltaTime * speed;
    }
}
