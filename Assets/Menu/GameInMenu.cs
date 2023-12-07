using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInMenu : MonoBehaviour
{
    
    [SerializeField] private Image gameImage;
    [SerializeField] private GameSettings gameSettings;
    // Start is called before the first frame update
   

    public void Init(GameSettings settings)
    {
        this.gameSettings = settings;
        gameImage.sprite = gameSettings.gameImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameSelected()
    {
        MainMenuManager.Instance.StartGame(gameSettings);
    }
}
