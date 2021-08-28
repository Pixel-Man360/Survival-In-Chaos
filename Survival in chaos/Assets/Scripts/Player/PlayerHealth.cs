using System;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
   
    public static event Action OnPlayerHit;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Object"))
        {
            OnPlayerHit?.Invoke();
        }
    }
}
