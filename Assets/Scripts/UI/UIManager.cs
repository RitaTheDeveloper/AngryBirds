using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIGameResults _uIGameResults;
    [SerializeField] private UIGameMenu _uIGameMenu;
    [SerializeField] private UIMainMenu _uIMainMenu;

    private void OnEnable()
    {
        _gameManager.OnStartLevel += _uIGameMenu.DisplayLives;
        _gameManager.OnWin += LevelCompleted;
        _gameManager.OnLose += _uIGameResults.LevelFailed;
        _gameManager.onUsedShot += _uIGameMenu.DisplayLives;
    }
    private void OnDisable()
    {
        _gameManager.OnStartLevel -= _uIGameMenu.DisplayLives;
        _gameManager.OnWin -= _uIGameResults.LevelCompleted;
        _gameManager.OnLose -= _uIGameResults.LevelFailed;
        _gameManager.onUsedShot -= _uIGameMenu.DisplayLives;
    }

    private void Start()
    {
        _uIMainMenu.gameObject.SetActive(true);
        _uIMainMenu.Init(this);
    }

    private void LevelCompleted()
    {
        _uIGameResults.LevelCompleted();
    }

    public void StartGame()
    {
        _uIGameMenu.DisplayLives(_gameManager.MaxNumberOfShots);
        _gameManager.Init();
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
