using System;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthControl : MonoBehaviour
{
   [SerializeField] private Slider _healthBar;
   [SerializeField] private Image _healthBarFillImage;

   [SerializeField] private GameObject _gameOverPanel;
   private int _health = 100;

   public static event Action OnPlayerDead;
   
   void OnEnable()
   {
       PlayerHealth.OnPlayerHit += ChangeHealth;
   }

   void OnDisable()
   {
       PlayerHealth.OnPlayerHit -= ChangeHealth;
   }

   void ChangeHealth(int damage)
   {
       _health -= damage;
       _healthBar.value = _health;

       Debug.Log("Health: " + _health);

       if(_health <= 20)
       {
           _healthBarFillImage.color = Color.red;
            if(_health <= 0)
            {
                Time.timeScale = 0;
                OnPlayerDead?.Invoke();
                _gameOverPanel.SetActive(true);
            }
       }

       
       else
       {
           _healthBarFillImage.color = Color.green;
       }

   }


}
