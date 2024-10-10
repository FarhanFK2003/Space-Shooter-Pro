using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isPlayerOne)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetBool("TurnLeft", true);
                anim.SetBool("TurnRight", false);
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", true);
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                anim.SetBool("TurnLeft", true);
                anim.SetBool("TurnRight", false);
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", true);
            }
            else if (Input.GetKeyUp(KeyCode.Keypad6))
            {
                anim.SetBool("TurnLeft", false);
                anim.SetBool("TurnRight", false);
            }
        }
    }
}
