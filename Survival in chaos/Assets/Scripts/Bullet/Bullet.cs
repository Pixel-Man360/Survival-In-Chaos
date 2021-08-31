using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed; 
    private bool _canMove;
    
  
    void FixedUpdate()
    {
        _rb.velocity = transform.right * Time.fixedDeltaTime * _bulletSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Walls"))
        {
            PlayerBulletPuller.instance.ReturnToPool(this);
        }
    }

  
}
