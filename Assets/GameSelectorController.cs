using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectorController : MonoBehaviour
{
    public float scrollStep = 0.1f; // Величина прокрутки
    public ScrollRect scrollRect;
   
    public int totalGamesInMenu = 5;
    private int currentIndex = 0;
    
    public Transform gamesHolder;
    public GameInMenu gameInMenuPrefab;
    public List<GameSettings> gamesSettingsInMenu;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateGamesInMenu();
    }
    
    void InstantiateGamesInMenu()
    {
        foreach (var gameSetting in gamesSettingsInMenu)
        {
            var gameInMenu = Instantiate(gameInMenuPrefab, gamesHolder);
            gameInMenu.Init(gameSetting);
        }
    }

    void ScrollNext()
    {
        currentIndex++;
        if (currentIndex >= gamesHolder.childCount)
        {
            currentIndex = 0;
        }
        ScrollToCurrentIndex();
    }

    void ScrollPrevious()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = gamesHolder.childCount - 1;
        }
        ScrollToCurrentIndex();
    }
    
    void ScrollToCurrentIndex()
    {
        float targetPosition = (float)currentIndex / (gamesHolder.childCount - 1); // Рассчитываем целевую позицию прокрутки
        scrollRect.horizontalNormalizedPosition = targetPosition; // Прокручиваем к целевой позиции
    }
}
