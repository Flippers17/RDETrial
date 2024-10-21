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
    
    [SerializeField, Tooltip("The time it takes before the enemy loses the player after losing sight of the player. If it loses sight of the player, it will still follow it for this many seconds.")]
    private float _timeToLosePlayer = .4f;
    private float _timeWhenLostSight = 0;
    private bool _seeingPlayer = true;

    public override void Awake(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Enter(EnemyStateMachine stateMachine)
    {
        stateMachine.movement.moveSpeedMultiplier = moveSpeedMultiplier;
        _seeingPlayer = true;
    }

    public override void Exit(EnemyStateMachine stateMachine)
    {
        //throw new System.NotImplementedException();
    }

    public override void Update(EnemyStateMachine stateMachine)
    {
        Vector2[] path = Pathfinder.Instance.GetPath(stateMachine.transform.position, stateMachine.playerTransform.position);

        stateMachine.movement.SetPath(path, false, 1);

        if (!stateMachine.CanSeePlayer())
        {
            if (_seeingPlayer)
            {
                _timeWhenLostSight = Time.time;
                _seeingPlayer = false;
            }
            else if (_timeWhenLostSight + _timeToLosePlayer < Time.time)
                stateMachine.TransitionToState(stateMachine.patrolState);
        }
        else if(stateMachine.attackBehaviour.CanDoSpecial())
            stateMachine.TransitionToState(stateMachine.specialAttackState);
        else if (((Vector2)stateMachine.transform.position - path[^1]).sqrMagnitude <= _attackStopRange * _attackStopRange)
            stateMachine.TransitionToState(stateMachine.attackState);
        else
            _seeingPlayer = true;
    }
}
