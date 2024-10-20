using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private Health _enemyHealth;

    private void OnEnable()
    {
        _enemyHealth.onDead += DeathEffect;
    }
    private void OnDisable()
    {
        _enemyHealth.onDead -= DeathEffect;
    }

    private void DeathEffect()
    {
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
    }
}
