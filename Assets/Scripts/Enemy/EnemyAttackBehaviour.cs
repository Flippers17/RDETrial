using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : MonoBehaviour
{
    private EnemyMovement _movement;

    [SerializeField]
    private float _attackOffset;
    [SerializeField]
    private float _attackSize = 1f;
    [SerializeField]
    private int _attackDamage = 1;


    [SerializeField]
    private GameObject _specialAttackPrefab;
    [SerializeField]
    private float _specialAttackCooldown = 10f;
    private float _timeWhenLastSpecial = 0;


    private void OnEnable()
    {
        _movement = GetComponent<EnemyMovement>();
    }


    public void DoMeleeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + _movement.FacingDirection + _attackOffset * _movement.FacingDirection, _attackSize);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out HurtBox h))
            {
                if(h.team != Team.Enemy)
                    h.Hit(_attackDamage);
            }
        }
    }


    public void DoSpecialAttack()
    {
        Transform t = Instantiate(_specialAttackPrefab, (Vector2)transform.position + _movement.FacingDirection, Quaternion.identity).transform;
        t.right = (Vector2)t.position - _movement.TargetPos;


        _timeWhenLastSpecial = Time.time;
    }


    public bool CanDoSpecial()
    {
        return _timeWhenLastSpecial + _specialAttackCooldown < Time.time;
    }
}
