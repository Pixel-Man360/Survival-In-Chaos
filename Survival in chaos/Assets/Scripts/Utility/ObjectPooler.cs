using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T prefab;

    public static ObjectPooler<T> instance {get; private set;}

    [HideInInspector]
    public Queue<T> objects = new Queue<T>();

    void Awake()
    {
        if(instance == null)
           instance = this;

        AddObjects(1);
        AddObjects(1);
        AddObjects(1);
        AddObjects(1);
        AddObjects(1);
    }

    void OnDestroy()
    {
        instance = null;
    }

    public T GetObject()
    {
        if(objects.Count == 0)
            AddObjects(1);

        return objects.Dequeue();
    }

    void AddObjects(int num)
    {
      var newObject = GameObject.Instantiate(prefab);
      newObject.gameObject.SetActive(false);
      objects.Enqueue(newObject);
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }


}
