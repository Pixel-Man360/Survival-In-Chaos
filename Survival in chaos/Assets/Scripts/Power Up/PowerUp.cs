using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            ReturnPowerUp();
        }

        else if(other.gameObject.CompareTag("Walls"))
        {
            StartCoroutine(PowerUpHitsWall());
        }
    }


    public void GivePowerUp(Transform spawnPos)
    {
        GameObject obj = ObjectPool.instance.GetObject(this.gameObject);
        obj.gameObject.transform.position = spawnPos.position;
        obj.SetActive(true);
    }

    void ReturnPowerUp()
    {
        ObjectPool.instance.ReturnToPool(this.gameObject);
    }

    IEnumerator PowerUpHitsWall()
    {
        
        yield return new WaitForSeconds(10f);

        //SoundManager.instance.PlaySound("explosion");
       

        //yield return new WaitForSeconds(0.5f);
        ReturnPowerUp();
    }
}
