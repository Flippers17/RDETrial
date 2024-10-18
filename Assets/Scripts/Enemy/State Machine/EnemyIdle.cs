using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyIdle : EnemyState
{
    public override void Awake(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Enter(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
        stateMachine.movement.moveSpeedMultiplier = 0;
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
        if(stateMachine.CanSeePlayer())
        {
            stateMachine.TransitionToState(stateMachine.chaseState);
        }
    }
}
