using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Health _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<Health>();
    }

    private void OnEnable()
    {
        if(_enemyHealth)
            _enemyHealth.onDead += Die;

    }

    private void OnDisable()
    {
        if (_enemyHealth)
            _enemyHealth.onDead -= Die;
    }

    private void Die()
    {
        Actions.OnEnemyKilled(this);
    }
}
