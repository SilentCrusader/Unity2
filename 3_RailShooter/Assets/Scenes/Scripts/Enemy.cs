using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private void Start()
    {
        BoxCollider addBoxCollider = gameObject.AddComponent<BoxCollider>();
        addBoxCollider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {        
        Destroy(this.gameObject);
    }
}
