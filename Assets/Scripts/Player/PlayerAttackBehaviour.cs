using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehaviour : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler _inputHandler;
    private PlayerMovement _playerMovement;

    [SerializeField]
    private int _meleeDamage = 1;

    [SerializeField]
    private float _attackSize = 1f;
    [SerializeField]
    private float _attackOffset = 0f;

    [SerializeField]
    private float _attackCooldown = .5f;
    private float _timeLastAttacked = 0;

    [SerializeField]
    private GameObject _attackEffect;


    private void OnEnable()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _inputHandler.OnAttack += HandleAttack;
    }

    private void OnDisable()
    {
        _inputHandler.OnAttack -= HandleAttack;
    }



    private void HandleAttack()
    {
        if (_timeLastAttacked + _attackCooldown > Time.time)
            return;

        if( _attackEffect)
        {
            Transform effect = Instantiate(_attackEffect, transform).transform;
            effect.localPosition = _playerMovement.FacingDirection + _attackOffset * _playerMovement.FacingDirection;
        }
        

        Collider2D[] colliders = Physics2D.OverlapCircleAll((Vector2)transform.position + _playerMovement.FacingDirection + _attackOffset * _playerMovement.FacingDirection, _attackSize);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out HurtBox h))
            {
                if (h.team != Team.Player)
                    h.Hit(_meleeDamage);
            }
        }

        _timeLastAttacked = Time.time;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + GetComponent<PlayerMovement>().FacingDirection + _attackOffset * GetComponent<PlayerMovement>().FacingDirection, _attackSize);
    }
}
