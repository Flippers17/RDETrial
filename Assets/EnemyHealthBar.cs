using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Vector2 _offset;

    [SerializeField]
    private Image _healthFill;

    public Transform targetTransform;

    public UnityAction<EnemyHealthBar> OnDie;

    public void SetUp(Health enemyHealth)
    {
        enemyHealth.OnHealthUpdated.AddListener(UpdateHealth);
        enemyHealth.OnDie.AddListener(Die);
        targetTransform = enemyHealth.transform;
    }


    private void UpdateHealth(int  health, int maxHealth)
    {
        _healthFill.fillAmount = (float)health/maxHealth;
    }


    private void Die()
    {
        OnDie?.Invoke(this);
    }
}
