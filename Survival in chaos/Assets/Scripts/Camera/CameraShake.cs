﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraShake : MonoBehaviour
{
    
    [SerializeField] private float _shakeIntensity;
    [SerializeField] private float _shakeTime;
    [SerializeField] private CinemachineVirtualCamera _cam;
    private float _startIntensity;
    private float _totalShakeTime;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private float _timer;
    void Awake()
    {
        _timer = 0f;
        _cam = GetComponent<CinemachineVirtualCamera>();
        _perlin =  _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    }

    void OnEnable()
    {
      PlayerShooting.OnShakeNeeded += ShakeThatCam;
      Item.OnItemHit += ShakeThatCam;
    }

    void OnDisable()
    {
      PlayerShooting.OnShakeNeeded -= ShakeThatCam;
      Item.OnItemHit -= ShakeThatCam;
    }

    
    void Update()
    {
      if(_timer > 0)
      {
          _timer -= Time.deltaTime;
      }
        
      if(_timer <= 0)
      {
        _perlin.m_AmplitudeGain = 0f;
        Mathf.Lerp(_startIntensity, 0f, 1-(_timer/_totalShakeTime));
      }
    }

    public void ShakeThatCam()
    {
      _perlin.m_AmplitudeGain = _shakeIntensity;
      _timer = _shakeTime;
      _totalShakeTime = _shakeTime;
      _startIntensity = _shakeIntensity;
    }


}
