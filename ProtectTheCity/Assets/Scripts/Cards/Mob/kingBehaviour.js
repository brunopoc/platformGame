#pragma strict

var defKing:int; //Defesa que o Rei tem antes de perder vida
var energKing:int; //Energia disponível para descer as cartas

static var detectCard:boolean;

function Start () {
		this.gameObject.GetComponent.<lifeBehaviour>().life = 100 + defKing;
}

function Update () {

	
}

function OnTriggerEnter2D(coll: Collider2D){
	if(coll.gameObject.tag == "cardToKing"){
    detectCard = true;
    }
}