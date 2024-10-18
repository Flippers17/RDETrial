using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{

    public abstract void Awake(EnemyStateMachine stateMachine);
    public abstract void Enter(EnemyStateMachine stateMachine);
    public abstract void Update(EnemyStateMachine stateMachine);
    public abstract void Exit(EnemyStateMachine stateMachine);
}
