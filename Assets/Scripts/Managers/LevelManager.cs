using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelPrefabs;
    [SerializeField] private Transform _container;
    [SerializeField] private GameManager _gameManager;
    private int _currentLevel;
    private void Awake()
    {
        _currentLevel = 0;
        CreateLevel(_currentLevel);
    }

    public void LoadNextLevel()
    {
        if (_currentLevel + 1 < _levelPrefabs.Length)
        {
            _currentLevel++;
        }
        else
        {
            _currentLevel = 0;
        }

        CreateLevel(_currentLevel);
        _gameManager.Init();

    }

    private void CreateLevel(int index)
    {
        DestroyLevel();
        Instantiate(_levelPrefabs[index], _container);
    }

    private void DestroyLevel()
    {
        foreach (Transform level in _container)
        {
            Destroy(level.gameObject);
        }
    }

    public void RestartLevel()
    {
        CreateLevel(_currentLevel);
        _gameManager.Init();
    }

}
