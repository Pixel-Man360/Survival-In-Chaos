using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBalls : Balls, IReturnObject
{
    private int _hitCount = 0;

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

    protected override void BallHitPlayer()
    {
        base.BallHitPlayer();
        StartCoroutine(ReturnObj());
    }

    protected override void BallHitBullet()
    {
        _hitCount++;

        if (_hitCount >= 3)
        {
            _hitCount = 0;
            base.BallHitBullet();
            StartCoroutine(ReturnObj());
        }
    }

    public IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }
}
