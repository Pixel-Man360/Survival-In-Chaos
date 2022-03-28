using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    [Header("Dependencies:")]
    [SerializeField] private BallSpawnData[] _ballObj;
    [SerializeField] private Transform _launchPoint;
    [SerializeField] private Transform _canonNozzle;
    [SerializeField] private Transform _target;
    [SerializeField] private ParticleSystem _muzzleFlash;

    [Header("Stats:")]
    [SerializeField] private float _speed;
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

    private double _accumulatedWeights;
    private System.Random _rand = new System.Random();

    
    void Awake() 
    {
        CalculateWeights();
    }

    void OnEnable()
    {
        GameManager.OnWaveStart += WaveStarted;
    }

    void OnDisable()
    {
        GameManager.OnWaveStart -= WaveStarted;
    }

    void WaveStarted(bool permission, int waveNum)
    {
        _waveStart = permission;

        if(_waveStart)
        return;

        _ballObj[0].chance -= 10;
        _ballObj[1].chance += 10;

        _ballObj[0].chance = Mathf.Clamp(_ballObj[0].chance, 50f, 100f);
        _ballObj[1].chance = Mathf.Clamp(_ballObj[1].chance, 0f, 50f);

        CalculateWeights();
    }
    
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
        _muzzleFlash.Play();
        CameraShake.instance.ShakeThatCam(4f, 0.2f);
        GameObject obj = ObjectPool.instance.GetObject(_ballObj[GetRandomBallIndex()].ballPrefab);
        obj.transform.position = _launchPoint.transform.position;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().velocity = _speed * _throwDirection * _launchPoint.transform.right;
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


    private void CalculateWeights()
    {
        _accumulatedWeights = 0f;

        foreach (BallSpawnData data in _ballObj)
        {
            _accumulatedWeights += data.chance;
            data.weight = _accumulatedWeights;
        }
    }

    private int GetRandomBallIndex()
    {
        double r = _rand.NextDouble() * _accumulatedWeights;

        for(int i = 0; i < _ballObj.Length; i++)
            if(_ballObj[i].weight >= r) 
              return i;
        
        
        return 0;

    }
}
