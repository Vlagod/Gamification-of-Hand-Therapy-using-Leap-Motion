using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class BalloonsSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public int maxObjects = 10;
    public float spawnRectX = 0f;
    public float spawnRectY = 0f;
    public float spawnRectWidth = 10f;
    public float spawnRectHeight = 10f;

    public float balloonsAutoDestroyPoint = 0f;
    private int numObjectsSpawned = 0;
    [SerializeField]
    private List<Balloon> spawnedBalloons = new List<Balloon>();

    void Start()
    {
        Balloon.BalloonPop.AddListener(BalloonPop);
        StartCoroutine(SpawnObjects());
    }

    private void OnDisable()
    {
        Balloon.BalloonPop.RemoveListener(BalloonPop);
    }

    IEnumerator SpawnObjects() {
        while (true)
        {
            while (spawnedBalloons.Count < maxObjects)
            {
                // Randomly generate position within spawn rectangle bounds
                Vector3 spawnPosition = new Vector2(Random.Range(spawnRectX, spawnRectX + spawnRectWidth),
                    Random.Range(spawnRectY, spawnRectY + spawnRectHeight));


                // Instantiate object at spawn position
                GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity, transform);
                spawnedBalloons.Add(newObject.GetComponent<Balloon>());


                var newObjectPos = newObject.transform.localPosition;
                newObjectPos.z = 0;
                newObject.transform.localPosition = newObjectPos;
                // Increment number of objects spawned
                numObjectsSpawned++;

                yield return null;
            }

            foreach (var balloon in spawnedBalloons)
            {
                if (balloon.transform.position.y > transform.position.y + balloonsAutoDestroyPoint)
                {
                    Destroy(balloon.gameObject);
                    
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void BalloonPop(Balloon balloon)
    {
        Debug.Log("balloon pop");
        spawnedBalloons.Remove(balloon);
        // spawnedBalloons.RemoveAll(item => item == null);
    }
    
    void OnDrawGizmosSelected() {
        // Draw a wireframe rectangle gizmo to show the spawn area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(spawnRectX + (spawnRectWidth / 2f), spawnRectY + (spawnRectHeight / 2f)),
            new Vector3(spawnRectWidth, spawnRectHeight, 0f));
        
        
        
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + balloonsAutoDestroyPoint));
    }
}
