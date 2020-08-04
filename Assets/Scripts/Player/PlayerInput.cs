using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerBehaviour))]
public class PlayerInput : MonoBehaviour
{

    PlayerBehaviour Player;

    void Start()
    {
        Player = GetComponent<PlayerBehaviour>();
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Player.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Player.OnDashInputDown();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Player.OnDashInputUp();
        }
    }

}
