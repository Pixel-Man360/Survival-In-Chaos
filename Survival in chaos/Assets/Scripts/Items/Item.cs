using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Item : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _col;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Light2D _light;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private ParticleSystem _burst;

    private float _backTimeIfNotDestroyed = 0f;

    public static event Action OnItemHit;

    public static event Action<int> OnPointsGained;

    void OnEnable()
    {
        _col.enabled = true;
        _sprite.enabled = true;
        _light.enabled = true;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
           {
               SoundManager.instance.PlaySound("explosion");
               _rb.velocity = Vector2.zero;
               _col.enabled = false;
               _sprite.enabled = false;
               _light.enabled = false;
               _burst.Play();
               OnItemHit?.Invoke();
               OnPointsGained?.Invoke(100);
               StartCoroutine(ReturnObj());
               
           }

          else if(other.gameObject.CompareTag("Player"))
          {
               SoundManager.instance.PlaySound("explosion");
               _rb.velocity = Vector2.zero;
               _col.enabled = false;
               _sprite.enabled = false;
               _light.enabled = false;
               _burst.Play();
               OnItemHit?.Invoke();
               StartCoroutine(ReturnObj());
          }
    }

    void FixedUpdate() 
    {
        if(_rb.velocity.y < 0)
           _rb.gravityScale = _fallSpeed;
        
        else 
          _rb.gravityScale = 1f;
    }

    void Update()
    {
        _backTimeIfNotDestroyed += Time.deltaTime;

        if(_backTimeIfNotDestroyed >= 20f)
        {
            SoundManager.instance.PlaySound("explosion");
            OnItemHit?.Invoke();
            _rb.velocity = Vector2.zero;
            _burst.Play();
            _col.enabled = false;
            _sprite.enabled = false;
            _light.enabled = false;
            _backTimeIfNotDestroyed = 0f;
            StartCoroutine(ReturnObj());
        }
    }

    IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        ItemsPuller.instance.ReturnToPool(this);
    }
   
}
