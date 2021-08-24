using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    
    [SerializeField] private float _fallingSpeed = 1.5f;
    [SerializeField] private float _xForce;
    [SerializeField] private float _yForce;
    [SerializeField] private float _playerYPos = -37.5f;
    [SerializeField] private float _playerXPosMin = -30.7f;
    [SerializeField] private float _playerXPosMax = 7.5f;

    private Rigidbody2D _rb;
    [SerializeField] private float _angle;
    private Vector2 _targetPosition;
    private Vector2 _itemPosition;
    private bool _canCalculateMovement;
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _targetPosition = new Vector2(Random.Range(_playerXPosMin, _playerXPosMax), _playerYPos);
        _itemPosition = transform.position;
        _rb.gravityScale = 0;
    }

    void OnEnable() 
    {
        //_canCalculateMovement = true;
    }

    void FixedUpdate()
    {
        Movement();

       // Gravity();
    }

    void Movement()
    {   
        _rb.gravityScale = 1;

        

        float vx = _itemPosition.x * Mathf.Cos(_angle);
        float vy = _itemPosition.y * Mathf.Sin(_angle) - (Physics2D.gravity.y * (_fallingSpeed - 1) * Time.fixedDeltaTime);

       _rb.velocity = new Vector2(vx * _xForce , vy * _yForce);
      
        

    }

    void Gravity()
    {
        if(_rb.velocity.y < 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, (Physics2D.gravity.y * (_fallingSpeed - 1) * Time.fixedDeltaTime));
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
      //  _canCalculateMovement = false;
    }

   
}
