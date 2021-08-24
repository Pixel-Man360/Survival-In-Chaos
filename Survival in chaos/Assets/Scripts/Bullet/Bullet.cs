using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed; 
   
    void FixedUpdate()
    {
        _rb.velocity = transform.right * Time.fixedDeltaTime * _bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerBulletPuller.instance.ReturnToPool(this);
    }
}
