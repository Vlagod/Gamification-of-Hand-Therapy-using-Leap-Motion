using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerLine : MonoBehaviour
{
    public GameObject externalCircle;
    public GameObject fingerTapCircle;
    
    public GameObject tilesToTapHolder;
    public GameObject tileToTapPrefab;
    
    public List<GameObject> tilesToTap;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FingerIsDown()
    {
        fingerTapCircle.SetActive(true);
    }

    public void FingerIsUp()
    {
        fingerTapCircle.SetActive(false);
    }
    
    public void SpawnTileToTap()
    {
        var tile = Instantiate(tileToTapPrefab, tilesToTapHolder.transform);
        tilesToTap.Add(tile);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTiles();
    }

    void MoveTiles()
    {
        foreach (var tile in tilesToTap)
        {
            tile.transform.position += Vector3.down * Time.deltaTime;
        }
    }
}
