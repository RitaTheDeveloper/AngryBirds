using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private Health _enemyHealth;

    private void OnEnable()
    {
        if (_enemyHealth)
            _enemyHealth.onDead += DeathEffect;
    }
    private void OnDisable()
    {
        if (_enemyHealth)
            _enemyHealth.onDead -= DeathEffect;
    }

    private void DeathEffect()
    {
        AudioManager.instance.PlaySound("PigDied");
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
    }
}
