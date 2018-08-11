using UnityEngine;
using System.Collections;

public class greyLabel : MonoBehaviour {

    float movement;
    GameObject blackObject;
    Vector3 direction;
    public dialog dialog;
    public player_behavior player_behavior;

    void Start (){
        direction.y = 0;
        direction.z = 0;
        dialog = GameObject.Find("Player").GetComponent<dialog>();
        player_behavior = GameObject.Find("Player").GetComponent<player_behavior>();
    }

    void Update (){
	    if(player_behavior.canMove == true){
        direction.x = -Input.GetAxis("Horizontal");
	    blackObject.transform.Translate(direction*movement);
	    }
    }
}