using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Team
{
    Player,
    Enemy
}


public class HurtBox : MonoBehaviour
{
    public Team team;

    public UnityEvent<int> OnHit;

    public void Hit(int damage)
    {
        OnHit?.Invoke(damage);
    }
}
