using UnityEngine;
using System;
public class Health
{
    float _maxHealth;
    float _currentHealth;

    public event EventHandler OnDamange;
    public event EventHandler onDeath;
    public Health   (float health)
    {
        _maxHealth = health;
        _currentHealth = health;
    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        OnDamange?.Invoke(this, EventArgs.Empty);
        if (_currentHealth <= 0)
        {
            //this died
            onDeath?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
