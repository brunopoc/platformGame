#pragma strict

static var stateCheck : String;
var animaPause : Animator;

function Start () {
	
}

function Update () {
	switch(stateCheck){
	case 'pause':
		animaPause.SetBool("pause", true);
	break;
	case 'play':
		animaPause.SetBool("pause", false);
	break;


	}
}

function Pause () {
 stateCheck = "pause";
}

function Play () {
 stateCheck = "play";
}