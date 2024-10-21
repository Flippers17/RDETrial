using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    public EnemyIdle idleState = new EnemyIdle();
    public EnemyPatrol patrolState = new EnemyPatrol();
    public EnemyChase chaseState = new EnemyChase();
    public EnemyAttack attackState = new EnemyAttack();
    public EnemySpecialAttackState specialAttackState = new EnemySpecialAttackState();

    private EnemyState _currentState;

    public EnemyMovement movement;
    public EnemyAttackBehaviour attackBehaviour;

    [SerializeField]
    private LayerMask _visionObstructionLayers;
    [SerializeField]
    private float _visionRange = 5f;

    [HideInInspector]
    public Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        idleState.Awake(this);
        patrolState.Awake(this);   
        chaseState.Awake(this);
        attackState.Awake(this);
        specialAttackState.Awake(this);

        _currentState = patrolState;
        _currentState.Enter(this);

        EnemyHealthBarsManager.Instance.AddHealthBar(GetComponent<Health>());
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

    public bool CanSeePlayer()
    {
        Vector2 playerDir = playerTransform.position - transform.position;
        return !Physics.Raycast(transform.position, playerDir.normalized, _visionRange, _visionObstructionLayers) && playerDir.sqrMagnitude < (_visionRange * _visionRange);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, _visionRange);

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, patrolState._patrolRadius);
    }
}
