using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
    public Action onDead;
    [field: SerializeField] public float MaxHealth { get; private set; } = 3f;
    [SerializeField] private float _damageTreshold = 0.2f;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        onDead.Invoke();       
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var impactVelocity = collision.relativeVelocity.magnitude;

        if (impactVelocity > _damageTreshold)
        {
            TakeDamage(impactVelocity);
        }
    }
}
