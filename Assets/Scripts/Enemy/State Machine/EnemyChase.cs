using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class EnemyChase : EnemyState
{
    public float moveSpeedMultiplier = 1.3f;

    [SerializeField, Tooltip("The distance to be from target before attacking")]
    private float _attackStopRange = .4f;

    public override void Awake(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Enter(EnemyStateMachine stateMachine)
    {
        stateMachine.movement.moveSpeedMultiplier = moveSpeedMultiplier;
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        Vector2[] path = Pathfinder.Instance.GetPath(stateMachine.transform.position, stateMachine.playerTransform.position);
        if(path.Length > 2)
            stateMachine.movement.TargetPos = path[2];


        if (!stateMachine.CanSeePlayer())
            stateMachine.TransitionToState(stateMachine.patrolState);
        else if (stateMachine.movement.DistanceToTarget <= _attackStopRange)
            stateMachine.TransitionToState(stateMachine.attackState);
    }
}
