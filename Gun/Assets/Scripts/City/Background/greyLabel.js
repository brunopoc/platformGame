#pragma strict

var blackObject : GameObject;
var movement : float;
var direction : Vector3;

function Start () {
	direction.y = 0;
	direction.z = 0;
}

function Update () {
	if(player_behavior.canMove == true){
    direction.x = -Input.GetAxis("Horizontal");
	blackObject.transform.Translate(direction*movement);
	}
}
