using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        deathFX.SetActive(true);
        StartDeathSequence();
        Invoke("ReloadLevel", loadLevelDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");        
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);        
    }
}
