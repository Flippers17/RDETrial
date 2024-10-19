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
    public Vector2 TargetPos {
        get { return _targetPos; }
        
        set 
        {
            _followingPath = false;
            _targetPos = value; 
        } 
    }

    [HideInInspector]
    public float moveSpeedMultiplier = 1f;

    private bool _followingPath = false;
    private bool _loopPath = false;
    private Vector2[] _currentPath = new Vector2[0];
    int _currentPathIndex = 0;


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
        if(_followingPath)
        {
            HandlePathFollowing();
        }
        
        if (IsWithinStoppingDistance())
            return;


        Vector2 pos = transform.position;
        transform.Translate((_targetPos - pos).normalized * (_moveSpeed * moveSpeedMultiplier * Time.deltaTime));
    }

    private void HandlePathFollowing()
    {
        if (_currentPathIndex >= _currentPath.Length)
        {
            _followingPath = false;
            return;
        }

        _targetPos = _currentPath[_currentPathIndex];

        if (IsWithinStoppingDistance())
            _currentPathIndex = _loopPath ? (_currentPathIndex + 1) % _currentPath.Length : _currentPathIndex + 1;
    }


    private bool IsWithinStoppingDistance()
    {
        Vector2 pos = transform.position;

        return (pos - _targetPos).sqrMagnitude < _stopDistance * _stopDistance;
    }


    public void SetPath(Vector2[] path, bool loopPath)
    {
        _currentPath = path;
        _currentPathIndex = 0;
        _loopPath = loopPath;
        _followingPath = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (_followingPath)
        {
            Gizmos.color = Color.green;

            for(int i = 0; i < _currentPath.Length; i++)
            {
                Gizmos.DrawWireSphere(_currentPath[i], .3f);
            }
        }
    }
}
