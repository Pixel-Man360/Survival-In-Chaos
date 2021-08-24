using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData 
{
    public string gunName;
    public GameObject gunObject;
    public SpriteRenderer gunGfx;
    public int bulletsPerRound;
    public float fireRate;
}
