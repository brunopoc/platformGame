using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(player_behaviour))]
public class player_input : MonoBehaviour
{

    player_behaviour Player;

    void Start()
    {
        Player = GetComponent<player_behaviour>();
    }

    void Update()
    {
        Vector2 directionInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Player.SetDirectionalInput(directionInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player.OnJumpInputDown();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Player.OnJumpInputUp();
        }
    }

}
