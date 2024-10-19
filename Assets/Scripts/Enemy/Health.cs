using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    GameManager _gameManager;
    [field: SerializeField] public float MaxHealth { get; private set; } = 3f;
    [SerializeField] private float _damageTreshold = 0.2f;

    private float _currentHealth;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

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
        _gameManager.RemoveEnemy(GetComponent<EnemyController>());
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
