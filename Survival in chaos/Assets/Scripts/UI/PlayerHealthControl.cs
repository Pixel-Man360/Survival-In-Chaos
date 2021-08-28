using UnityEngine;
using UnityEngine.UI;
public class PlayerHealthControl : MonoBehaviour
{
   [SerializeField] private Slider _healthBar;
   [SerializeField] private Image _healthBarFillImage;

   [SerializeField] private GameObject _gameOverPanel;
   private int _health = 3;
   
   void OnEnable()
   {
       PlayerHealth.OnPlayerHit += ChangeHealth;
   }

   void OnDisable()
   {
       PlayerHealth.OnPlayerHit -= ChangeHealth;
   }

   void ChangeHealth()
   {
       _health--;

       _healthBar.value = _health;

       if(_health == 1)
       {
           _healthBarFillImage.color = Color.red;
       }

       else if(_health <= 0)
       {
           Time.timeScale = 0;
           _gameOverPanel.SetActive(true);
       }
       else
       {
           _healthBarFillImage.color = Color.green;
       }

   }


}
