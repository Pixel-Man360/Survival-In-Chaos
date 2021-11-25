using System;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
   
    public static event Action<int> OnPlayerHit;

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Object"))
        {
            IDamager obj = other.gameObject.GetComponent<IDamager>();
            if(obj != null)
            {
                OnPlayerHit?.Invoke(obj.GetDamageValue());
            }
        }
    }
}
