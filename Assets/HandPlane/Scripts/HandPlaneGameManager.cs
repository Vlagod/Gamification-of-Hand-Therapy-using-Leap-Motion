using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandPlaneGameManager : MonoBehaviour
{
    public float baseTimeInSeconds = 60f;
    private float timeRemaining;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI countdownText;
    
    public TextMeshProUGUI gameStartCountdownText;

    public GameObject restartGameButton;

    private float score = 0;
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        gameStartCountdownText.gameObject.SetActive(false);
        restartGameButton.SetActive(false);
        PauseTheGame();
        PlaneController.Instance.onPlaneCollision.AddListener(OnPlaneCollision);
        timeRemaining = baseTimeInSeconds;
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        score += Time.deltaTime;
        scoreText.text = "Score: " + score.ToString("0");
        countdownText.text =timeRemaining.ToString("0");
        if (timeRemaining <= 0)
        {
            countdownText.text = "0";
            Time.timeScale = 0;
            GameEnd();
        }
        
    }

    void OnPlaneCollision()
    {
        var penalty = 5f;
        score-= penalty;
    }

    
    void GameEnd()
    {
        PauseTheGame();
        restartGameButton.SetActive(true);
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
