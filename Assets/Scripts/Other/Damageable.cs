using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [Header("Damageable References")]
    public float currentHealth;
    public float maxHealth = 100f;

    public virtual void TakeDamage(float _damage)
    {
        currentHealth -= _damage;
        if(currentHealth <= 0) Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
}