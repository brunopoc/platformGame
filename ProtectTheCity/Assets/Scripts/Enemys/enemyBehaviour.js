#pragma strict
var way1: Transform;
var way2: Transform;
var way3: Transform;
var way4: Transform;

var enemy1:Transform;

function Start () {
if(stateMachine.stateCheck != "pause"){
	Phase1();
	}
}

function Update () {
	
}

function Phase1(){

	yield WaitForSeconds(5);
	Instantiate(enemy1, way1.position, Quaternion.identity);
	yield WaitForSeconds(5);
	Instantiate(enemy1, way3.position, Quaternion.identity);
	yield WaitForSeconds(4);
	Instantiate(enemy1, way2.position, Quaternion.identity);
	yield WaitForSeconds(2);
	Instantiate(enemy1, way1.position, Quaternion.identity);
	yield WaitForSeconds(8);
	Instantiate(enemy1, way4.position, Quaternion.identity);
	yield WaitForSeconds(12);
	Instantiate(enemy1, way2.position, Quaternion.identity);
	yield WaitForSeconds(5);
	Instantiate(enemy1, way4.position, Quaternion.identity);
	Instantiate(enemy1, way2.position, Quaternion.identity);
	yield WaitForSeconds(8);
	Instantiate(enemy1, way1.position, Quaternion.identity);
	Instantiate(enemy1, way3.position, Quaternion.identity);

}
