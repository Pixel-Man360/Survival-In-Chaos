using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBalls : Balls, IReturnObject
{
    [Header("Redball dependencies")]
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _playerLayer;
    [Header("Redball stats")]

    [Range(1f, 6f)]
    [SerializeField] private float _explosionDamageRange;

    // void OnDrawGizmos() 
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(this.transform.position, _explosionDamageRange);

    //     if(_target != null)
    //     {  
    //         float dotProduct =  Vector3.Dot(transform.up, Vector3.Normalize(transform.position - _target.position));
    //         float distanceVal = Vector3.Distance(transform.position, _target.position);   

    //         print("Distance: " + distanceVal);
    //         print("Dot Product: " + dotProduct);
    //         Gizmos.color = Color.white;
    //         Gizmos.DrawLine(transform.position, _target.position);

    //         if(dotProduct >= 0.85)
    //         {
    //             if(distanceVal >= 6.5)
    //             {
    //                 print("Damage 10%");
    //             }

    //             else 
    //             {
    //                 print("Damage 30%");
    //             }
    //         }

    //         else 
    //         {
    //             if(distanceVal >= 5.7)
    //             {
    //                 print("Damage 10%");
    //             }

    //             else 
    //             {
    //                 print("Damage 30%");
    //             }
    //         }

    //     }
    // }

    void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // void Update()
    // {
    //      float dotProduct =  Vector3.Dot(transform.up, Vector3.Normalize(transform.position - _target.position));
    //      float distanceVal = Vector3.Distance(transform.position, _target.position);

    //     if(dotProduct >= 0.85)
    //     {
    //         if(distanceVal <= 6.5)
    //         {
    //             BallHitBullet();
    //             Debug.Log("10%");
    //         }

    //     }

    //     else
    //     {
    //         if(distanceVal <= 5.8)
    //         {
    //             BallHitBullet();
    //         }
    //     }
    // }


    protected override void HandleCollisions(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BallHitBullet();
        }

        else if (other.gameObject.CompareTag("Player"))
        {
            BallHitPlayer();
        }

        else if (other.gameObject.CompareTag("Walls"))
        {
            BallHitWall(this.gameObject);
        }
    }

    protected override void ExplosionShake()
    {
        CameraShake.instance.ShakeThatCam(4f, 0.25f);
    }

    protected override void BallHitPlayer()
    {
        _ballDamage = 50;
        base.BallHitPlayer();
        StartCoroutine(ReturnObj());
    }

    protected override void BallHitBullet()
    {
        CheckBlastRadiusForPlayer();

        base.BallHitBullet();
        StartCoroutine(ReturnObj());
    }

    void CheckBlastRadiusForPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _explosionDamageRange, _playerLayer);

        Debug.Log(collider);
        if (collider != null && collider.gameObject.CompareTag("Player"))
        {
            float dotProduct = Vector3.Dot(transform.up, Vector3.Normalize(transform.position - _target.position));
            float distanceVal = Vector3.Distance(transform.position, _target.position);

            if (dotProduct >= 0.85)
            {
                _ballDamage = (distanceVal >= 6.4) ? 10 : 30;
            }

            else
            {
                _ballDamage = (distanceVal >= 5.8) ? 10 : 30;
            }
        }
    }



    public IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }
}
