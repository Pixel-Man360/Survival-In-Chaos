using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBalls : Balls, IReturnObject
{
    protected override void HandleCollisions(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            BallHitBullet();
        }

        else if(other.gameObject.CompareTag("Player"))
        {
            
            BallHitPlayer();
        }

        else if(other.gameObject.CompareTag("Walls"))
        {
            StartCoroutine(BallHitWall(this.gameObject, 10f));
        }
    }

    protected override void BallHitPlayer()
    {
        base.BallHitPlayer();
        StartCoroutine(ReturnObj());
    }

    protected override void BallHitBullet()
    {
        base.BallHitBullet();
        StartCoroutine(ReturnObj());
    }
    
    

    public IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }
}
