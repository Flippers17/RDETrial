using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpecialAttackState : EnemyState
{
    [SerializeField, Tooltip("Time it takes to perform an attack. The enemy cannot move for this many seconds after it enters this state.")]
    private float _attackTime = .5f;
    private float _timeWhenLastAttacked = 0f;


    public override void Awake(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Enter(EnemyStateMachine stateMachine)
    {
        stateMachine.movement.moveSpeedMultiplier = 0;
        stateMachine.movement.TargetPos = stateMachine.playerTransform.position;
        TriggerAttack(stateMachine);
        stateMachine.animator.SetBool("Attacking", true);
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        stateMachine.animator.SetBool("Attacking", false);
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        stateMachine.movement.TargetPos = stateMachine.playerTransform.position;

        if ((_timeWhenLastAttacked + _attackTime) < Time.time)
        {
            stateMachine.TransitionToState(stateMachine.chaseState);
            return;
        }            
    }


    private void TriggerAttack(EnemyStateMachine stateMachine)
    {
        stateMachine.attackBehaviour.DoSpecialAttack();
        _timeWhenLastAttacked = Time.time;
    }
}
