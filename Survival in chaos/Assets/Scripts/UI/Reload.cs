using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Reload : MonoBehaviour
{
    [SerializeField] private Button _reloadButton;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _sliderColor;
    
    
    public static event Action OnReloadPressed;
    public static event Action<bool> OnButtonHighlighted;
    
    void OnEnable()
    {
        PlayerShooting.OnBulletReduce += ChangeBulletSlider;
    }

    void OnDisable()
    {
        PlayerShooting.OnBulletReduce -= ChangeBulletSlider;
    }

    public void ReloadGun()
    {
        OnReloadPressed?.Invoke();  
    }

    void ChangeBulletSlider(int maxVal,int value)
    {

        _slider.maxValue = maxVal;
        _slider.value = value;

        if(value <= 2)
          _sliderColor.color = Color.red;
        
        else
          _sliderColor.color = Color.green;

    }

    public void ButtonHighlighted()
    {
        OnButtonHighlighted?.Invoke(true);
        Debug.Log("Hovering....");
    }

    public void ButtunNotHighlighted()
    {
        OnButtonHighlighted?.Invoke(false);
        Debug.Log("Won't Reload now.");
    }

     

    

}
