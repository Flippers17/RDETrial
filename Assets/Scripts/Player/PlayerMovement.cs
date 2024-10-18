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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = _inputHandler.MoveInput.normalized * _maxSpeed;   
    }
}
