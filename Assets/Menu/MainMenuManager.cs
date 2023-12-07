using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

   
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }



    public void StartGame(GameSettings gameSettings)
    {
        // Здесь мы должны загрузить сцену с игрой и передать настройки в неё.
        SceneManager.LoadScene(gameSettings.gameSceneName);
        
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
