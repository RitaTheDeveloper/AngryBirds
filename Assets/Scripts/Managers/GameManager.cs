using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field:Header("Stats: ")]
    [field:SerializeField] public int MaxNumberOfShots { get; private set; } = 3;

    [Header("Scripts: ")]
    [SerializeField] private SlingshotHandler _slingshotHandler;
    private int _shotCounter;
    private List<EnemyController> _enemies = new List<EnemyController>();

    private void Start()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (var go in enemies)
        {
            _enemies.Add(go);
        }

        _slingshotHandler.Init(this);
    }

    private void OnEnable()
    {
        Actions.OnEnemyKilled += RemoveEnemy;
    }

    private void OnDisable()
    {
        Actions.OnEnemyKilled -= RemoveEnemy;
    }

    public void UseShot()
    {
        _shotCounter++;
        CheckForLastShot();
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
        yield return new WaitForSeconds(2f);

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
        Debug.Log("победа!");
    }

    private void LoseGame()
    {
        Debug.Log("поражение!");
    }

    private void CheckForAllDeadEnemies()
    {
        if (_enemies.Count == 0)
        {
            WinGame();
        }
    }

}
