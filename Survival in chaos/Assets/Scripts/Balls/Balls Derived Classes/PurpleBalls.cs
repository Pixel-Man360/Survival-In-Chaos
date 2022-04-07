using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleBalls : Balls, IReturnObject
{
    bool firstHit = false;
    bool secondHit = false;
    bool thirdHit = false;
    
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
            StartCoroutine(BallHitWall(this.gameObject, 5f));
        }
    }

    protected override void BallHitPlayer()
    {
        _ballDamage = 50;
        base.BallHitPlayer();
        StartCoroutine(ReturnObj());
    }

    protected override void BallHitBullet()
    {
        // CheckBlastRadiusForPlayer();

        // base.BallHitBullet();
        // StartCoroutine(ReturnObj());

        //Tripple Shot Kill
    }

    public IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }
}
