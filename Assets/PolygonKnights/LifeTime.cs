using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float destroyTime;

    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
