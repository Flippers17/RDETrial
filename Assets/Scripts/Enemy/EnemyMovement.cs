using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 3f;
    [SerializeField, Tooltip("The distance from it is from it's target when it stops")]
    private float _stopDistance = .5f;


    [SerializeField]
    private bool _debug;


    private Vector2 _targetPos;
    public Vector2 TargetPos {
        get { return _targetPos; }
        
        set 
        {
            _followingPath = false;
            _targetPos = value; 
        } 
    }

    public float DistanceToTarget
    {
        get
        {
            return ((Vector2)transform.position - _targetPos).magnitude;
        }
    }

    private Vector2 _facingDirection = new Vector2(1, 0);
    public Vector2 FacingDirection { get => _facingDirection; }


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

            if (_currentPathIndex == _currentPath.Length -1 && IsWithinStoppingDistance())
                return;
        }
        else if (IsWithinStoppingDistance())
            return;
        


        Vector2 pos = transform.position;
        if (moveSpeedMultiplier > 0f)
            _facingDirection = (_targetPos - pos).normalized;

        transform.Translate(_facingDirection * (_moveSpeed * moveSpeedMultiplier * Time.deltaTime));
    }

    private void HandlePathFollowing()
    {
        if (_currentPathIndex >= _currentPath.Length)
        {
            _followingPath = false;
            return;
        }

        if (IsWithinStoppingDistance())
            _currentPathIndex = _loopPath ? (_currentPathIndex + 1) % _currentPath.Length : _currentPathIndex + 1;

        _targetPos = _currentPath[_currentPathIndex];
    }


    private bool IsWithinStoppingDistance()
    {
        Vector2 pos = transform.position;

        return (pos - _targetPos).sqrMagnitude < _stopDistance * _stopDistance;
    }


    public void SetPath(Vector2[] path, bool loopPath, int startIndex)
    {
        _currentPathIndex = startIndex;
        _currentPath = path;
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

    private void OnDrawGizmos()
    {

        if(!_debug)
            return;

        if (_followingPath)
        {
            Gizmos.color = Color.green;

            for (int i = 0; i < _currentPath.Length - 1; i++)
            {
                Gizmos.DrawWireSphere(_currentPath[i], .3f);
                Gizmos.DrawLine(_currentPath[i], _currentPath[i + 1]);
            }

            Gizmos.DrawWireSphere(_currentPath[^1], .3f);
        }
    }
}
