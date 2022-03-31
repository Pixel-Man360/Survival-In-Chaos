using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBalls : Balls, IReturnObject
{
    [Header("Blue Balls Dependencies:")]
    [SerializeField] private PowerUp[] _powerUp;

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
            StartCoroutine(BallHitWall(this.gameObject, 4f));
        }
    }

    void GetPowerUp()
    {
        _powerUp[UnityEngine.Random.Range(0, _powerUp.Length)].GivePowerUp(this.transform);
    }

    protected override void BallHitPlayer()
    {
        base.BallHitPlayer();
        StartCoroutine(ReturnObj());
    }

    protected override void BallHitBullet()
    {
        base.BallHitBullet();
        StartCoroutine(ReturnAndGetPowerUp());
        
    }

    public IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }

    IEnumerator ReturnAndGetPowerUp()
    {
        yield return new WaitForSeconds(0.1f);
        GetPowerUp();
        yield return new WaitForSeconds(0.5f);
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }
}
