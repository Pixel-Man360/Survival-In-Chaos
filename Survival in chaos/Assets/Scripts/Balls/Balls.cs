using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Balls : MonoBehaviour, IDamager
{
    [Header("Dependencies:")]
    [SerializeField] protected Rigidbody2D _rb;
    [SerializeField] protected Collider2D _col;
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected TrailRenderer _trail;
    [SerializeField] protected Light2D _light;
    [SerializeField] protected ParticleSystem _burst;

    [Header("Stats:")]
    [SerializeField] protected float _fallSpeed;
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
        _trail.enabled = true;
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
            
        // }
        
    }


    protected virtual void BallHitPlayer()
    {
        SoundManager.instance.PlaySound("explosion");
        _rb.velocity = Vector2.zero;
        _col.enabled = false;
        _sprite.enabled = false;
        _trail.enabled = false;
        _light.enabled = false;
        _burst.Play();
       // OnBallsHit?.Invoke();
        ExplosionShake();
    }

    protected virtual void BallHitBullet()
    {
        SoundManager.instance.PlaySound("explosion");
        _rb.velocity = Vector2.zero;
        _col.enabled = false;
        _sprite.enabled = false;
        _trail.enabled = false;
        _light.enabled = false;
        _burst.Play();
      
        //OnBallsHit?.Invoke();
        ExplosionShake();
        OnPointsGained?.Invoke(_point);
    }


    protected IEnumerator BallHitWall(GameObject obj, float time)
    {
        
        yield return new WaitForSeconds(time);

        SoundManager.instance.PlaySound("explosion");
       // OnBallsHit?.Invoke();
        ExplosionShake();
        _rb.velocity = Vector2.zero;
        _col.enabled = false;
        _sprite.enabled = false;
        _trail.enabled = false;
        _light.enabled = false;
        _burst.Play();

        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(obj);
    }

    protected virtual void ExplosionShake()
    {
        CameraShake.instance.ShakeThatCam(3f, 0.15f);
    }


    public int GetDamageValue() => _ballDamage;

    

}
