#pragma strict
var onSlotItem:boolean;

function Start () {
	
}

function Update () {
if(stateMachine.stateCheck != "pause"){
	if(Input.GetMouseButtonUp(0) && mouseBehaviour.kingField == true && mouseBehaviour.mouseOnPosition == true && mouseBehaviour.moveCard != null && onSlotItem == true){
	  Debug.Log('você colocou o card no Slot! Parabéns!');
    }
   } else {

   }
}
function OnTriggerEnter2D(coll: Collider2D){
    if(coll.gameObject.tag == "itemCard"){
        onSlotItem = true ;
        Debug.Log('onSlot');
    }
}
function OnTriggerExit2D(coll: Collider2D){
    if(coll.gameObject.tag == "itemCard"){
        onSlotItem = false ;
        Debug.Log('offSlot');
    }
}