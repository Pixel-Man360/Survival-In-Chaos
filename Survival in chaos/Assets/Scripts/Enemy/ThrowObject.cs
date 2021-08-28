using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private Transform _target;
    [SerializeField] private float _gravity = 9.8f;
    public float throwBreak;
    [SerializeField] private int _throwDirection = -1;
    [SerializeField] private float _angleOffsetMin;
    [SerializeField] private float _angleOffsetMax;

    private bool _canThrow = true;

    private bool _waveStart = false;

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
          float? rotation = RotateLauncher();

          if (rotation != null)
           {
               if(throwBreak <= 0.8f)
               {
                   throwBreak = 0.8f;
               } 
               Throw(); 
           } 
        }
        
    }

    IEnumerator ResetThrowing()
    {
        _canThrow = false;
        yield return new WaitForSeconds(throwBreak);
        _canThrow = true;
    }

    void Throw()
    {
        Item obj = ItemsPuller.instance.GetObject();
        obj.gameObject.transform.position = _launchPoint.transform.position;
        obj.gameObject.SetActive(true);
        obj.gameObject.GetComponent<Rigidbody2D>().velocity = _speed * _throwDirection * _launchPoint.transform.right;
        StartCoroutine(ResetThrowing());
    }

    float? RotateLauncher()
    {
        float? angle = CalculateAngle();
        if (angle != null)
            _launchPoint.transform.localEulerAngles = new Vector3(0, 0, 360f - (float)angle);

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
