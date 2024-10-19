using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPatrol : EnemyState
{
    [SerializeField]
    private int _minimumPatrolPoints = 2;
    [SerializeField] 
    private int _maximumPatrolPoints = 3;

    [SerializeField, Range(1, 30)]
    public float _patrolRadius = 3f;

    [SerializeField]
    private float _moveSpeedMultiplier = .8f;

    public override void Awake(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Enter(EnemyStateMachine stateMachine)
    {
        int numberOfPatrolPoints = Random.Range(_minimumPatrolPoints, _maximumPatrolPoints + 1);

        Vector2[] patrolPoints = new Vector2[numberOfPatrolPoints];

        for(int i = 0; i < numberOfPatrolPoints; i++)
        {
            patrolPoints[i] = (Vector2)stateMachine.transform.position + Random.insideUnitCircle * _patrolRadius;
        }

        stateMachine.movement.SetPath(patrolPoints, true);
        stateMachine.movement.moveSpeedMultiplier = _moveSpeedMultiplier;
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        if(stateMachine.CanSeePlayer())
            stateMachine.TransitionToState(stateMachine.chaseState);
    }
}
