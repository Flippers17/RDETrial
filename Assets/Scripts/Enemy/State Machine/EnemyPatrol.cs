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
        stateMachine.animator.SetBool("Patroling", true);

        int numberOfPatrolPoints = Random.Range(_minimumPatrolPoints, _maximumPatrolPoints + 1);

        Vector2[] patrolPoints = new Vector2[numberOfPatrolPoints];
        List<Vector2> patrolPath = new List<Vector2>();
        patrolPoints[0] = stateMachine.transform.position;

        for(int i = 1; i < numberOfPatrolPoints; i++)
        {
            if(Pathfinder.Instance.Grid.GetNearestUnoccupiedSpace((Vector2)stateMachine.transform.position + Random.insideUnitCircle * _patrolRadius, out Vector2 p))
                patrolPoints[i] = p;
            else
                patrolPoints[i] = stateMachine.transform.position;
        }

        for(int i = 0; i < numberOfPatrolPoints; i++)
        {
            Vector2[] path = Pathfinder.Instance.GetPath(patrolPoints[i], patrolPoints[(i + 1) % numberOfPatrolPoints]);

            for(int j = 0; j < path.Length; j++)
            {
                patrolPath.Add(path[j]);
            }
        }


        stateMachine.movement.SetPath(patrolPath.ToArray(), true, 0);
        stateMachine.movement.moveSpeedMultiplier = _moveSpeedMultiplier;
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        stateMachine.animator.SetBool("Patroling", false);
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        if(stateMachine.CanSeePlayer())
            stateMachine.TransitionToState(stateMachine.chaseState);
    }
}
