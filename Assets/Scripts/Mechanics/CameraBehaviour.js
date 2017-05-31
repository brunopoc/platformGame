#pragma strict
var targetPlayer: Transform;

function Start () {
	
}

function Update () {
	this.gameObject.transform.position.x = targetPlayer.position.x;
}
