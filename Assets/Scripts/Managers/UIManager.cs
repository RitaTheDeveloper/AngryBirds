using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Scripts: ")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIGameResults _uIGameResults;
    [SerializeField] private UIGameMenu _uIGameMenu;

    private void OnEnable()
    {
        _gameManager.OnWin += _uIGameResults.LevelCompleted;
        _gameManager.OnLose += _uIGameResults.LevelFailed;
        _gameManager.onUsedShot += _uIGameMenu.DisplayLives;
    }
    private void OnDisable()
    {
        _gameManager.OnWin -= _uIGameResults.LevelCompleted;
        _gameManager.OnLose -= _uIGameResults.LevelFailed;
        _gameManager.onUsedShot -= _uIGameMenu.DisplayLives;
    }

    private void Start()
    {
        _uIGameMenu.DisplayLives(_gameManager.MaxNumberOfShots);
    }

}
