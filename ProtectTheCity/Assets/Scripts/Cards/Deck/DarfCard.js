#pragma strict

private var field : int;

var way1: Transform;
var way2: Transform;
var way3: Transform;
var way4: Transform;

function Start () {
	field = 0;
}

function Update () {

		if(stateMachine.stateCheck != "pause"){
			   
				    if(Input.GetMouseButtonUp(0) && mouseBehaviour.kingField == true && mouseBehaviour.moveCard != null && field != 0){
				    baseCardBehaviour.controlStartPosition = false;
						    switch(field){
								    case 1:
								     Instantiate(Resources.Load("Prefabs/Deck/Mob/darf_Mob") as GameObject, way1.position, Quaternion.identity);
								     Destroy(mouseBehaviour.moveCard);
								     mouseBehaviour.moveCard = null;
								    break;
								    case 2:
								     Instantiate(Resources.Load("Prefabs/Deck/Mob/darf_Mob") as GameObject, way2.position, Quaternion.identity);
								     Destroy(mouseBehaviour.moveCard);
								     mouseBehaviour.moveCard = null;
								    break;
								    case 3:
								     Instantiate(Resources.Load("Prefabs/Deck/Mob/darf_Mob") as GameObject, way3.position, Quaternion.identity);
								     Destroy(mouseBehaviour.moveCard);
								     mouseBehaviour.moveCard = null;
								    break;
								    case 4:
								     Instantiate(Resources.Load("Prefabs/Deck/Mob/darf_Mob") as GameObject, way4.position, Quaternion.identity);
								     Destroy(mouseBehaviour.moveCard);
								     mouseBehaviour.moveCard = null;
								    break;
						    }
				    }

				    if(Input.GetMouseButtonUp(0)){
				     field = 0;
				    }

		 } else {

		 }
}

function OnTriggerEnter2D(coll: Collider2D){
    if(coll.gameObject.tag == "field1"){
        field = 1;
        Debug.Log('onField1');
    }
    if(coll.gameObject.tag == "field2"){
        field = 2;
        Debug.Log('onField2');
    }
    if(coll.gameObject.tag == "field3"){
        field = 3;
        Debug.Log('onField3');
    }
    if(coll.gameObject.tag == "field4"){
        field = 4;
        Debug.Log('onField4');
    }
}