using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyChase : EnemyState
{
    public float moveSpeedMultiplier = 1.3f;

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
        stateMachine.movement.TargetPos = stateMachine.playerTransform.position;
    }
}
