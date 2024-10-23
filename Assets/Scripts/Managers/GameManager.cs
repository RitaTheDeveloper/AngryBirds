using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Action OnWin, OnLose;
    public Action<int> onUsedShot;

    [field:Header("Stats: ")]
    [field:SerializeField] public int MaxNumberOfShots { get; private set; } = 3;

    [Header("Scripts: ")]
    [SerializeField] private SlingshotHandler _slingshotHandler;
    private int _shotCounter;
    private List<EnemyController> _enemies = new List<EnemyController>();

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
        _shotCounter = 0;
        _enemies = new List<EnemyController>();
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        for (int i = 0; i < enemies.Length ; i++)
        {
            _enemies.Add(enemies[i]);                      
        }
        _slingshotHandler.Init(this);
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

    private void CheckForLastShot()
    {
        if (_shotCounter == MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWaitTime());
        }
    }

    private IEnumerator CheckAfterWaitTime()
    {
        yield return new WaitForSeconds(4f);

        if (_enemies.Count == 0)
        {
            WinGame();
        }
        else
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
        OnWin.Invoke();
        Debug.Log("победа!");
    }

    private void LoseGame()
    {
        OnLose.Invoke();
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
