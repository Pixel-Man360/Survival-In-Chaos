using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Balls : MonoBehaviour, IDamager
{
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Collider2D _col;
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected Light2D _light;
    [SerializeField] protected float _fallSpeed;
    [SerializeField] protected ParticleSystem _burst;
    [SerializeField] protected int _ballDamage = 20;

    [SerializeField] protected int _point = 100;
    protected float _backTimeIfNotDestroyed = 0f;
    protected bool _isWallHit = false;

    public static event Action OnBallsHit;

    public static event Action<int> OnPointsGained;

    void OnEnable()
    {
        _col.enabled = true;
        _sprite.enabled = true;
        _light.enabled = true;
        _isWallHit = false;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        HandleCollisions(other);
    }


    protected virtual void HandleCollisions(Collision2D other){}

    void FixedUpdate() 
    {
        if(_rb.velocity.y < 0)
           _rb.gravityScale = _fallSpeed;
        
        else 
          _rb.gravityScale = 1f;
    }

    void Update()
    {
        // if(!_isWallHit)
        //  return;

        // else
        // {
        //     _backTimeIfNotDestroyed += Time.deltaTime;

        //     if(_backTimeIfNotDestroyed >= 10f)
        //     {
        //         SoundManager.instance.PlaySound("explosion");
        //         OnBallsHit?.Invoke();
        //         _rb.velocity = Vector2.zero;
        //         _burst.Play();
        //         _col.enabled = false;
        //         _sprite.enabled = false;
        //         _light.enabled = false;
        //         _backTimeIfNotDestroyed = 0f;
        //         StartCoroutine(ReturnObj());
        //         _isWallHit = false;
        //     }
        // }
        
    }

    

    protected void BallsHitEvent() => OnBallsHit?.Invoke();

    protected void PointsGainedEvent() => OnPointsGained?.Invoke(_point);
    

    public int GetDamageValue() => _ballDamage;



   
   
}
