using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField]
    private HealthUIPort _healthUIPort;

    [SerializeField]
    private Image _healthFill;

    private void OnEnable()
    {
        _healthUIPort.OnUpdateHealth += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _healthUIPort.OnUpdateHealth -= UpdateHealthBar;
    }



    private void UpdateHealthBar(int health, int maxHealth)
    {
        _healthFill.fillAmount = (float)health/maxHealth;
    }
}
