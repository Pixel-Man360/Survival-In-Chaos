using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    [SerializeField] private Gun _gun;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private ParticleSystem _muzzleFlash;

    private bool _canShoot = true;
    private float _angle;
    private int _totalBullets;
    private int _bulletsCount;
    private bool _isReloadPressed = false;
    private bool _isButtonHighlighted = false;

    public static event Action OnShoot;
    public static event Action<int,int> OnBulletReduce;

    void Start()
    {
        _totalBullets = _gun.gunData.bulletsPerRound;
        _bulletsCount = _totalBullets;
    }

    void OnEnable()
    {
        Reload.OnReloadPressed += ReloadPressed;
        Reload.OnButtonHighlighted += Highlight;
    }

    void OnDisable()
    {
        Reload.OnReloadPressed -= ReloadPressed;
        Reload.OnButtonHighlighted -= Highlight;
        
    }

    void Highlight(bool isHighlighted) => _isButtonHighlighted = isHighlighted;

  

    void ReloadPressed()
    {
       _isReloadPressed = true;
       if(_bulletsCount != _totalBullets && _isReloadPressed)
       {
           StartCoroutine(ReloadTheGun());
       }

       else if(_bulletsCount == _totalBullets)
       {
           _isReloadPressed = false;
       }
    } 
    void Update()
    {

        if(Input.GetButton("Fire1") && _canShoot && !_isReloadPressed && !_isButtonHighlighted)
        {
           _muzzleFlash.Play();
           OnShoot?.Invoke();
           Aim();
           ReloadCheck();
           Shoot();
           Recoil();
           
        }
    }

    void ReloadCheck()
    {
       _bulletsCount--;

       if(_bulletsCount == 0)
       {
            StartCoroutine(ReloadTheGun());
       }

       else
       {
           OnBulletReduce?.Invoke(_totalBullets,_bulletsCount);
           StartCoroutine(ResetFire());
       }
    }

    void Aim()
    {
        Vector3 mousePosition = Utility.GetMousePosition();

        Vector3 direction = (mousePosition - _gun.gunData.gunObject.transform.position).normalized;

        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _gun.gunData.gunObject.transform.eulerAngles = new Vector3(0,0, _angle);
        
        Vector3 localScale = Vector3.one;

        if(_angle > 90 || _angle < -90)
        {
            localScale.y = -1f;
        }

        else 
        {
            localScale.y = +1f;
        }

        _gun.gunData.gunObject.transform.localScale = localScale;

        
    }


    void Shoot()
    {
        Bullet bullet =  PlayerBulletPuller.instance.GetObject();
        bullet.gameObject.transform.position = _shootPoint.transform.position;
        bullet.gameObject.transform.rotation = _gun.gunData.gunObject.transform.rotation;
        bullet.gameObject.SetActive(true);
    
    }

    void Recoil()
    {
        if(_angle > 90 || _angle < -90)
          {
              StartCoroutine(AddRecoil(-5f));
          }
        
        else
           {
             StartCoroutine(AddRecoil(5f));
           }

    }

    IEnumerator AddRecoil(float amount)
    {
        _gun.gameObject.transform.localEulerAngles = new Vector3(0, 0, _gun.gameObject.transform.localEulerAngles.z + amount);
        yield return new WaitForSeconds(0.2f);
        _gun.gameObject.transform.localEulerAngles = new Vector3(0, 0, _gun.gameObject.transform.localEulerAngles.z - amount);
    }

    IEnumerator ResetFire()
    {
        _canShoot = false;;
        yield return new WaitForSeconds(_gun.gunData.fireRate);
        _canShoot = true;
        
    }

    IEnumerator ReloadTheGun()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_reloadTime);
        _canShoot = true;
        _bulletsCount = _totalBullets;
        OnBulletReduce?.Invoke(_totalBullets,_bulletsCount);
        _isReloadPressed = false;
    }
}
