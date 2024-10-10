﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void LoadCoOpMode()
    {
        SceneManager.LoadScene("CoOpMode");
    }
}
