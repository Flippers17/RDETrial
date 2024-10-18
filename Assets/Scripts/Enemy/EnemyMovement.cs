using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3f;
    [SerializeField, Tooltip("The distance from it is from it's target when it stops")]
    private float _stopDistance = .5f;

    private Vector2 _targetPos;
    public Vector2 TargetPos { get { return _targetPos; } set { _targetPos = value; } }

    [HideInInspector]
    public float moveSpeedMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        Vector2 pos = transform.position;

        if ((pos - _targetPos).sqrMagnitude < _stopDistance * _stopDistance)
            return;

        transform.Translate((_targetPos - pos).normalized * (_moveSpeed * moveSpeedMultiplier * Time.deltaTime));
    }
}
