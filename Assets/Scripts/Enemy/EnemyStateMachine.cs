using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    private EnemyIdle _idleState = new EnemyIdle();
    private EnemyPatrol _patrolState = new EnemyPatrol();
    private EnemyChase _chaseState = new EnemyChase();
    private EnemyAttack _attackState = new EnemyAttack();

    private EnemyState _currentState;

    // Start is called before the first frame update
    void Awake()
    {
        _idleState.Awake(this);
        _patrolState.Awake(this);   
        _chaseState.Awake(this);
        _attackState.Awake(this);

        _currentState = _idleState;
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.Update(this);
    }

    public void TransitionToState(EnemyState state)
    {
        _currentState.Exit(this);
        _currentState = state;
        _currentState.Enter(this);
    }
}
