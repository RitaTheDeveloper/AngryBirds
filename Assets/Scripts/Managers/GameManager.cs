using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Action OnWin, OnLose, OnStart;
    public Action<int> onUsedShot, OnStartLevel;

    [field:Header("Stats: ")]
    [field:SerializeField] public int MaxNumberOfShots { get; private set; } = 3;

    [Header("Scripts: ")]
    [SerializeField] private SlingshotHandler _slingshotHandler;
    private int _shotCounter;
    private List<EnemyController> _enemies = new List<EnemyController>();
    private bool _levelCompleted;
    private bool _isPlaying;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Actions.OnEnemyKilled += RemoveEnemy;
    }

    private void OnDisable()
    {
        Actions.OnEnemyKilled -= RemoveEnemy;
    }

    public void Init()
    {        
        _isPlaying = true;
        _levelCompleted = false;
        _shotCounter = 0;
        _enemies = new List<EnemyController>();
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        for (int i = 0; i < enemies.Length ; i++)
        {
            _enemies.Add(enemies[i]);                      
        }
        _slingshotHandler.enabled = true;
        _slingshotHandler.Init(this);
        OnStartLevel.Invoke(MaxNumberOfShots);
    }

    public void UseShot()
    {
        _shotCounter++;
        CheckForLastShot();
        onUsedShot.Invoke(MaxNumberOfShots - _shotCounter);
    }

    public bool HasAvailableShots()
    {
        if (_shotCounter < MaxNumberOfShots)
            return true;
        else
            return false;
    }

    private bool CheckForLastShot()
    {
        if (_shotCounter == MaxNumberOfShots)
        {
            _slingshotHandler.enabled = false;
            if (_isPlaying && !_levelCompleted)
                StartCoroutine(CheckAfterWaitTime());
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator CheckAfterWaitTime()
    {
        yield return new WaitForSeconds(4f);

        if (_enemies.Count == 0)
        {
            WinGame();
        }
        else if(!_levelCompleted && _isPlaying)
        {
            LoseGame();
        }

    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemies.Remove(enemy);
        CheckForAllDeadEnemies();
    }

    private void WinGame()
    {
        _isPlaying = false;
        _levelCompleted = true;        
        _slingshotHandler.enabled = false;
        Debug.Log("победа!");
        OnWin.Invoke();
    }

    private void LoseGame()
    {
        if(_isPlaying && !_levelCompleted)
        {
            OnLose.Invoke();
        }
        _isPlaying = false;
        _slingshotHandler.enabled = false;
        Debug.Log("поражение!");
        ClearEnemies();        
    }

    private void CheckForAllDeadEnemies()
    {
        if (_enemies.Count == 0)
        {
            WinGame();
        }
    }

    public void ClearEnemies()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
            _enemies.Remove(enemies[i].GetComponent<EnemyController>());                       
        }
    }

}
