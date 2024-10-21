using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler _inputHandler;
    [SerializeField]
    private Rigidbody2D _rb;
    
    [Space(15), SerializeField]
    private float _maxSpeed = 6f;

    private Vector2 _facingDirection = new Vector2(1, 0);
    public Vector2 FacingDirection { get => _facingDirection; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputHandler.MoveInput.sqrMagnitude > 0)
        {
            _facingDirection = _inputHandler.MoveInput.normalized;
            _rb.velocity = _facingDirection * _maxSpeed;   
        }
        else
            _rb.velocity = Vector2.zero;

        
    }
}
