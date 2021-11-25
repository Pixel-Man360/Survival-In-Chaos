using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeBalls : Balls, IReturnObject
{
    protected override void HandleCollisions(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            SoundManager.instance.PlaySound("explosion");
            _rb.velocity = Vector2.zero;
            _col.enabled = false;
            _sprite.enabled = false;
            _light.enabled = false;
            _burst.Play();
            base.BallsHitEvent();
            base.PointsGainedEvent();
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
            base.BallsHitEvent();
            StartCoroutine(ReturnObj());
        }

        else if(other.gameObject.CompareTag("Walls"))
        {
            _isWallHit = true;
        }
    }

    public IEnumerator ReturnObj()
    {
        yield return new WaitForSeconds(0.5f);
        OrangeBallsPool.instance.ReturnToPool(this);
    }
}
