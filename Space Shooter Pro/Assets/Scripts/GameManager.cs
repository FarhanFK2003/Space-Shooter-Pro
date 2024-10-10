using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isGameOver = true;

    public bool isCoOpMode = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.R) && !isCoOpMode)
            {
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.R) && isCoOpMode)
            {
                SceneManager.LoadScene(2);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    public void GameOver()
    {
        isGameOver = true;
    }
}
