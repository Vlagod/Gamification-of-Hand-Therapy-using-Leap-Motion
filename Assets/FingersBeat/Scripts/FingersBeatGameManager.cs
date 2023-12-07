using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingersBeatGameManager : MonoBehaviour
{
    [SerializeField] private List<LaneBehaviour> lanes = new List<LaneBehaviour>();

    [SerializeField] private TapFieldBehaviour tapFieldPrefab;

    [SerializeField]
    private List<Color32> colorsActive;
    [SerializeField]
    private List<Color32> colors;

    [SerializeField] private float spawnSpeed = 15f;
    private float timeToNextSpawn = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToNextSpawn -= Time.deltaTime;
        if (timeToNextSpawn <= 0)
        {
            SpawnTapField();
            timeToNextSpawn = spawnSpeed;
            
        }
    }


    void SpawnTapField()
    {
        var fingerIndex = Random.Range(0, lanes.Count);
        var targetLane = lanes[fingerIndex];
        TapFieldBehaviour spawnedTapField =
            Instantiate(tapFieldPrefab, targetLane.transform.position, Quaternion.identity, targetLane.transform);

        var randomSize = Random.Range(1, 5);
        spawnedTapField.Init(randomSize, fingerIndex);
    }
    
}
