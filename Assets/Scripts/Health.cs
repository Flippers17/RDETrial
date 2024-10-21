using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int _health;
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    private GameObject _deathEffect;

    public UnityEvent OnTakeDamage;
    public UnityEvent<int, int> OnHealthUpdated;
    public UnityEvent OnDie;


    public void TakeDamage(int dmg)
    {
        OnTakeDamage?.Invoke();
        _health -= dmg;
        OnHealthUpdated?.Invoke(_health, _maxHealth);

        if (_health <= 0)
            Die();
    }

    public void Heal(int healing)
    {
        _health += healing;
        _health = Mathf.Min(_health, _maxHealth);
        OnHealthUpdated.Invoke(_health, _maxHealth);
    }


    public void Die()
    {
        if (_deathEffect != null)
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        
        OnDie?.Invoke();
    }
}
