using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[System.Serializable]
public class EnemyAttack : EnemyState
{
    [SerializeField, Tooltip("Time it takes to perform an attack. The enemy cannot move for this many seconds after it enters this state.")]
    private float _attackTime = .5f;
    private float _timeWhenLastAttacked = 0f;

    [SerializeField]
    private float _attackCooldown = 1f;


    [SerializeField, Tooltip("The distance from the target required to exit the attack state.")]
    private float _cancelDistance = 1.5f;


    public override void Awake(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Enter(EnemyStateMachine stateMachine)
    {
        stateMachine.movement.moveSpeedMultiplier = 0;
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        stateMachine.movement.TargetPos = stateMachine.playerTransform.position;

        if(stateMachine.movement.DistanceToTarget > _cancelDistance && (_timeWhenLastAttacked + _attackTime) < Time.time)
        {
            stateMachine.TransitionToState(stateMachine.chaseState);
            return;
        }

        if(_timeWhenLastAttacked + _attackTime + _attackCooldown < Time.time)
            TriggerAttack(stateMachine);
    }


    private void TriggerAttack(EnemyStateMachine stateMachine) 
    {
        Debug.Log("Attack!");
        _timeWhenLastAttacked = Time.time;
    }
}
