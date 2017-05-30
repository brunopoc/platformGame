#pragma strict
var life:int;


function Start () {
	
}

function Update () {
		if(stateMachine.stateCheck != "pause"){
				if(life <= 0){
				Destroy(this.gameObject);
				}	
		}
}
