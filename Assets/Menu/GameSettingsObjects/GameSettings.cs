using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GameSettings", menuName = "HandTherapy/Game Settings", order = 1)]
public class GameSettings : ScriptableObject
{
    public Sprite gameImage;
    public string gameSceneName;
    
    public string gameName;
    public string gameDescription;
}
