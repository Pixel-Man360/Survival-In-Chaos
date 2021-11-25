﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private Transform _canonNozzle;
    [SerializeField] private Transform _target;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private int _throwDirection = -1;
    [SerializeField] private float _angleOffsetMin;
    [SerializeField] private float _angleOffsetMax;
    public float throwBreak;
    
    private float? rotation = 90f;

    private float? angle = 45f;
    private bool _canThrow = true;
    private bool _waveStart = false;
   
    private bool _canRotate = true;


    void OnEnable()
    {
        GameManager.OnWaveStart += WaveStarted;
    }

    void OnDisable()
    {
        GameManager.OnWaveStart -= WaveStarted;
    }

    void WaveStarted(bool permission) => _waveStart = permission;
    
    void Update()
    {

        if(_canThrow && _waveStart)
        {

          if (rotation != null)
           {
              
               if(throwBreak <= 0.5f)
               {
                   throwBreak = 0.5f;
               } 

               Throw(); 
               angle = CalculateAngle();
               StartCoroutine(ResetRotation());
           } 
        }

        if(!_canThrow && _canRotate)
        {
            rotation = RotateLauncher();
            
        }

    }

    IEnumerator ResetThrowing()
    {
        _canThrow = false;
        yield return new WaitForSeconds(throwBreak);
        _canThrow = true;
    }

    IEnumerator ResetRotation()
    {
        _canRotate = false;
        yield return new WaitForSeconds(1f);
        _canRotate = true;
    }

    void Throw()
    {
        SoundManager.instance.PlaySound("pop up");
        Balls obj = OrangeBallsPool.instance.GetObject();
        obj.gameObject.transform.position = _launchPoint.transform.position;
        obj.gameObject.SetActive(true);
        obj.gameObject.GetComponent<Rigidbody2D>().velocity = _speed * _throwDirection * _launchPoint.transform.right;
        StartCoroutine(ResetThrowing());
    }

    float? RotateLauncher()
    {
        Quaternion newRotation = Quaternion.Euler(0,0, 360 - (float)angle);

        if (angle != null)
           {
                _canonNozzle.transform.rotation = Quaternion.RotateTowards(_canonNozzle.transform.rotation, newRotation, 30f * Time.deltaTime);

           }

        return angle;
    }



    float? CalculateAngle()
    {
        Vector3 targetDirection = (_target.position - transform.position).normalized;
        
        float y = targetDirection.y;
        targetDirection.y = 0;

        float x = targetDirection.magnitude;

        float SpeedSqrt = _speed * _speed;
        float underTheNextSqrt = SpeedSqrt * SpeedSqrt - _gravity * (_gravity * x * x + 2 * y * SpeedSqrt);

        if(underTheNextSqrt >= 0)
        {
            float root = Mathf.Sqrt(underTheNextSqrt);
            float angle = SpeedSqrt + root;

            float finalAngle = Mathf.Atan2(angle, _gravity) * Mathf.Rad2Deg;
            finalAngle = Mathf.Round(finalAngle * 10.0f) * 0.1f;

            return finalAngle - Random.Range(_angleOffsetMin, _angleOffsetMax);
        }

        else
           return null;

    }
}
