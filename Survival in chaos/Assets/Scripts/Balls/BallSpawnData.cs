using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallSpawnData
{
   public GameObject ballPrefab;
   [Range(0f, 100f)] public float chance = 100f;

   [HideInInspector] public double weight;
}
