using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Health UI Port", menuName = "Health UI Port")]
public class HealthUIPort : ScriptableObject
{
    public UnityAction<int, int> OnUpdateHealth;

    public void UpdateHealth(int health, int maxHealth)
    {
        OnUpdateHealth?.Invoke(health, maxHealth);
    }
}
