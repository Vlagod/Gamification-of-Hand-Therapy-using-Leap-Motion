using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public List<GameObject> ObstaclesGameObjects;
    public GameObject start;
    public Vector3 leftSpawnPos;
    public Vector3 middleSpawnPos;
    public Vector3 rightSpawnPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {

            yield return new WaitForSeconds(6f);
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        
        var randomObject = ObstaclesGameObjects[Random.Range(0, ObstaclesGameObjects.Count)];
        if (Random.Range(0, 2) == 0)
        { 
            randomObject = start;
        }
            
        var randomSpawnPos = Random.Range(0, 3);
        switch (randomSpawnPos)
        {
            case 0:
                Instantiate(randomObject, leftSpawnPos, Quaternion.identity, transform);
                break;
            case 1:
                Instantiate(randomObject, middleSpawnPos, Quaternion.identity, transform);
                break;
            case 2:
                Instantiate(randomObject, rightSpawnPos, Quaternion.identity, transform);
                break;
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 startPos = new Vector3(1, 1, 1); // Замените на вашу конкретную стартовую координату
        Vector3 endPos = new Vector3(2, 2, 2); // Замените на вашу конкретную конечную координату
        Gizmos.DrawLine(startPos, endPos);
        DrawGizmoDot(leftSpawnPos);
        DrawGizmoDot(middleSpawnPos);
        DrawGizmoDot(rightSpawnPos);
    }

    void DrawGizmoDot(Vector3 cord)
    {
        Gizmos.color = Color.red;
        Vector3 position = cord; // Замените на вашу конкретную координату
        float radius = 0.1f; // Радиус сферы
        Gizmos.DrawSphere(position, radius);
    }
}
