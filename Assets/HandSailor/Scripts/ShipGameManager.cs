using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipGameManager : MonoBehaviour
{
    public float baseTimeInSeconds = 60f;
    private float timeRemaining;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreTextGameEnd;
    public GameObject gameEndWindow;
    public TextMeshProUGUI countdownText;
    
    public TextMeshProUGUI gameStartCountdownText;

    public GameObject restartGameButton;

    public ShipController shipController;

    public TextMeshProUGUI get1Text;
    public TextMeshProUGUI getMinus1Text;
    public static ShipGameManager Instance;
    private float score = 0;
    public bool isPaused = false;
    
    

    private void Awake()
    {
        Instance = this;
        gameEndWindow.SetActive(false);
        baseTimeInSeconds = PlayerPrefs.GetFloat("ShipGameTime", 2) * 30;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameStartCountdownText.gameObject.SetActive(false);
        // restartGameButton.SetActive(false);
        PauseTheGame();
        // shipController.OnShipCollision.AddListener(OnShipCollision);
        timeRemaining = baseTimeInSeconds;
        // StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        scoreText.text = score.ToString("0");
        countdownText.text = timeRemaining.ToString("0");
        if (timeRemaining <= 0)
        {
            countdownText.text = "0";
            Time.timeScale = 0;
            GameEnd();
        }
        
    }

    public void OnShipCollision()
    {
        var penalty = 1f;
        score-= penalty;
        LostScoreEffect();
    }
    
    public void AddScore()
    {
        var increment = 1f;
        score += increment;
        GetScoreEffect();
    }

    public void GetScoreEffect()
    {
        get1Text.gameObject.SetActive(true);
        var color = get1Text.color;
        color.a = 1f;
        get1Text.color = color;
        
        StartCoroutine(HideGet1(get1Text));
    }
    public void LostScoreEffect()
    {
        getMinus1Text.gameObject.SetActive(true);
        var color = getMinus1Text.color;
        color.a = 1f;
        getMinus1Text.color = color;
        
        StartCoroutine(HideGet1(getMinus1Text));
    }

    private IEnumerator HideGet1(TextMeshProUGUI text)
    {
        while (text.color.a > 0)
        {
            var color = text.color;
            color.a -= 0.05f;
            text.color = color;
            yield return new WaitForSeconds(0.1f);
        }
       
    }


    void GameEnd()
    {
        PauseTheGame();
        // restartGameButton.SetActive(true);
        gameEndWindow.SetActive(true);
        scoreTextGameEnd.text = score.ToString("0");
        
    }
    
    public void PauseTheGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        // Можно отображать меню паузы или что-то в этом роде здесь.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        // Можно скрыть меню паузы или что-то в этом роде здесь.
    }
    
    public void RestartGame()
    {
        // Загрузка текущей активной сцены заново
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void StartGame()
    {
        
        StartCoroutine(StartGameCorountine());
    }

    IEnumerator StartGameCorountine()
    {
        baseTimeInSeconds = PlayerPrefs.GetFloat("ShipGameTime", 2) * 30;
        gameStartCountdownText.gameObject.SetActive(true);
        gameStartCountdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        gameStartCountdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        gameStartCountdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        gameStartCountdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        gameStartCountdownText.gameObject.SetActive(false);
        ResumeGame();
    }
}
