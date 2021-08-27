using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _fallSpeed;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
           ItemsPuller.instance.ReturnToPool(this);
    }

    void FixedUpdate() 
    {
        if(_rb.velocity.y < 0)
           _rb.gravityScale = _fallSpeed;
        
        else 
          _rb.gravityScale = 1f;
    }
   
}
