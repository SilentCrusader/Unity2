using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{    
    void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;        
        
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(this.gameObject);
        }        
    }

    void Start()
    {
        Invoke("LoadNextLevel", 10f);
    }


    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);      
    }
}
