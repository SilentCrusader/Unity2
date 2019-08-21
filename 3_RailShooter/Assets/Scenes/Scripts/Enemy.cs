using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 12;

    ScoreBoard scoreBoard;

    private void Start()
    {
        BoxCollider addBoxCollider = gameObject.AddComponent<BoxCollider>();
        addBoxCollider.isTrigger = false;
        scoreBoard = FindObjectOfType<ScoreBoard>();        
    }

    void OnParticleCollision(GameObject other)
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
        scoreBoard.scoreHit(scorePerHit);
    }
}
