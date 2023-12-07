using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerBeatManager : MonoBehaviour
{
    public FingerLine fingerLine1;
    public FingerLine fingerLine2;
    public FingerLine fingerLine3;
    public FingerLine fingerLine4;
    public FingerLine fingerLine5;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fingerLine1.tilesToTap.Count == 0)
            fingerLine1.SpawnTileToTap();

        if (Input.GetKey(KeyCode.Alpha1))
        {
         fingerLine1.FingerIsDown();   
        }
        else
        {
            fingerLine1.FingerIsUp();
        }
    }
}
